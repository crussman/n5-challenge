using AutoMapper;

using Moq;

using N5.Challenge.Domain.Dtos;
using N5.Challenge.Domain.Entities;
using N5.Challenge.Domain.Repositories;
using N5.Challenge.Domain.Services;
using N5.Challenge.Domain.UnitOfWork;
using N5.Challenge.Domain.ValueObjects;
using N5.Challenge.Infrastructure.Services;

namespace N5.Challenge.Tests.Services
{
    public class PermissionServiceTests
    {
        private readonly Mock<IPermissionRepository> _permissionRepoMock = new();
        private readonly Mock<IPermissionTypeService> _permissionTypeServiceMock = new();
        private readonly Mock<IElasticsearchService> _elasticsearchServiceMock = new();
        private readonly Mock<IKafkaService> _kafkaServiceMock = new();
        private readonly Mock<IUnitOfWorkAsync> _unitOfWorkMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private PermissionService CreateService() => new(
            _permissionRepoMock.Object,
            _permissionTypeServiceMock.Object,
            _elasticsearchServiceMock.Object,
            _kafkaServiceMock.Object,
            _unitOfWorkMock.Object,
            _mapperMock.Object);

        [Fact]
        public async Task GetPermissionsAsync_ReturnsPermissions_ShouldSucceed()
        {
            // Arrange
            var permissions = new Permission[]
            {
                new() {
                    Id = 1,
                    EmployeeFirstName = "Joseph",
                    EmployeeLastName = "Joestar",
                    PermissionTypeId = 1,
                    PermissionDate = DateTime.UtcNow
                }
            };

            _permissionRepoMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(permissions);

            var service = CreateService();

            // Act
            var result = await service.GetPermissionsAsync(CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(permissions.First().EmployeeFirstName, result.First().EmployeeFirstName);
            Assert.Equal(permissions.First().EmployeeLastName, result.First().EmployeeLastName);
            Assert.Equal(permissions.First().PermissionTypeId, result.First().PermissionTypeId);
            Assert.Equal(permissions.First().PermissionDate, result.First().PermissionDate);

        }

        [Fact]
        public async Task CreatePermissionAsync_CreatesAndReturnsPermission_ShouldSucceed()
        {
            // Arrange
            var creationData = new PermissionCreationData("Zoro", "Roronoa", 1, DateTime.UtcNow);

            _permissionTypeServiceMock.Setup(s => s.ExistsAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var permission = new Permission
            {
                Id = 2,
                EmployeeFirstName = "Zoro",
                EmployeeLastName = "Roronoa",
                PermissionTypeId = 1,
                PermissionDate = creationData.PermissionDate
            };

            _mapperMock.Setup(m => m.Map<Permission>(creationData)).Returns(permission);

            _permissionRepoMock.Setup(r => r.AddAsync(permission, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(1));

            var service = CreateService();

            // Act
            var result = await service.CreatePermissionAsync(creationData, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(permission.EmployeeFirstName, result.EmployeeFirstName);
            Assert.Equal(permission.EmployeeLastName, result.EmployeeLastName);
            Assert.Equal(permission.PermissionTypeId, result.PermissionTypeId);
            Assert.Equal(permission.PermissionDate, result.PermissionDate);

        }

        [Fact]
        public async Task ModifyPermissionAsync_UpdatesAndReturnsPermission_ShouldSucceed()
        {
            // Arrange
            var modificationData = new PermissionModificationData(3, "Mia", "Wallace", 2, DateTime.UtcNow);

            var permission = new Permission
            {
                Id = 3,
                EmployeeFirstName = "Old",
                EmployeeLastName = "Name",
                PermissionTypeId = 2,
                PermissionDate = DateTime.UtcNow.AddDays(-1)
            };

            _permissionRepoMock.Setup(r => r.GetByIdAsync(3, It.IsAny<CancellationToken>()))
                .ReturnsAsync(permission);

            _permissionTypeServiceMock.Setup(s => s.ExistsAsync(2, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(1));

            var service = CreateService();

            // Act
            var result = await service.ModifyPermissionAsync(modificationData, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(permission.EmployeeFirstName, result.EmployeeFirstName);
            Assert.Equal(permission.EmployeeLastName, result.EmployeeLastName);
            Assert.Equal(permission.PermissionTypeId, result.PermissionTypeId);
            Assert.Equal(permission.PermissionDate, result.PermissionDate);
        }
    }
}