using AutoMapper;

using N5.Challenge.Domain.Constants;
using N5.Challenge.Domain.Dtos;
using N5.Challenge.Domain.Entities;
using N5.Challenge.Domain.Repositories;
using N5.Challenge.Domain.Resources;
using N5.Challenge.Domain.Services;
using N5.Challenge.Domain.UnitOfWork;
using N5.Challenge.Domain.ValueObjects;
namespace N5.Challenge.Infrastructure.Services;

public class PermissionService(
    IPermissionRepository permissionRepository,
    IPermissionTypeService permissionTypeService,
    IElasticsearchService elasticsearchService,
    IKafkaService kafkaService,
    IUnitOfWorkAsync unitOfWork,
    IMapper mapper) : IPermissionService
{
    private readonly IPermissionRepository _permissionRepository = permissionRepository ?? throw new ArgumentNullException(nameof(permissionRepository));
    private readonly IPermissionTypeService _permissionTypeService = permissionTypeService ?? throw new ArgumentNullException(nameof(permissionTypeService));
    private readonly IElasticsearchService _elasticsearchService = elasticsearchService ?? throw new ArgumentNullException(nameof(elasticsearchService));
    private readonly IKafkaService _kafkaService = kafkaService ?? throw new ArgumentNullException(nameof(kafkaService));
    private readonly IUnitOfWorkAsync _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<Permission> CreatePermissionAsync(PermissionCreationData data, CancellationToken cancellationToken)
    {
        if (!await _permissionTypeService.ExistsAsync(data.PermissionTypeId, cancellationToken))
            throw new KeyNotFoundException(Messages.PermissionTypeNotFound);

        var permission = _mapper.Map<Permission>(data);

        await _permissionRepository.AddAsync(permission, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Since there is no requirement to do anything with the result of both the Kafka and ElasticSearch
        // operations, we can run these asynchronously without awaiting for their answers...
        _ = _elasticsearchService.IndexPermissionAsync(_mapper.Map<PermissionElasticSearchDto>(permission), cancellationToken);
        _ = _kafkaService.SendMessageAsync(Guid.NewGuid(), KafkaOperationNames.Request, cancellationToken);

        return permission;
    }

    public async Task<IEnumerable<Permission>> GetPermissionsAsync(CancellationToken cancellationToken)
    {
        // Since there is no requirement to do anything with the result of the Kafka operation,
        // we can run it asynchronously without awaiting for its answer...
        _ = _kafkaService.SendMessageAsync(Guid.NewGuid(), KafkaOperationNames.Get, cancellationToken);

        return await _permissionRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Permission> ModifyPermissionAsync(PermissionModificationData data, CancellationToken cancellationToken)
    {
        var permission = await _permissionRepository.GetByIdAsync(data.Id, cancellationToken)
            ?? throw new KeyNotFoundException(Messages.PermissionNotFound);

        if (!await _permissionTypeService.ExistsAsync(data.PermissionTypeId, cancellationToken))
            throw new KeyNotFoundException(Messages.PermissionTypeNotFound);

        permission.EmployeeFirstName = data.EmployeeFirstName;
        permission.EmployeeLastName = data.EmployeeLastName;
        permission.PermissionTypeId = data.PermissionTypeId;
        permission.PermissionDate = data.PermissionDate;

        _permissionRepository.Update(permission);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Since there is no requirement to do anything with the result of both the Kafka and ElasticSearch
        // operations, we can run these asynchronously without awaiting for their answers...
        _ = _elasticsearchService.IndexPermissionAsync(_mapper.Map<PermissionElasticSearchDto>(permission), cancellationToken);
        _ = _kafkaService.SendMessageAsync(Guid.NewGuid(), KafkaOperationNames.Modify, cancellationToken);

        return permission;
    }
}