using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Moq;

using N5.Challenge.Domain.Entities;
using N5.Challenge.Domain.Services;
using N5.Challenge.Domain.ValueObjects;
using N5.Challenge.Infrastructure;
using N5.Challenge.Infrastructure.Repositories;
using N5.Challenge.Infrastructure.Services;
using N5.Challenge.Infrastructure.UnitOfWork;

namespace N5.Challenge.Tests.Integrations
{
    public class PermissionServiceIntegrationTests
    {
        private static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Infrastructure.Mappings.EntityToValueObjectMappingProfile>();
                cfg.AddProfile<Application.Mappings.EntityToDtoMappingProfile>();
            });
            return config.CreateMapper();
        }

        private static AppDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new AppDbContext(options);

            // Seed PermissionTypes
            context.PermissionTypes.Add(new PermissionType { Id = 1, Description = "Read" });
            context.PermissionTypes.Add(new PermissionType { Id = 2, Description = "Write" });
            context.SaveChanges();

            return context;
        }

        private static PermissionService CreateService(AppDbContext context)
        {
            var permissionRepo = new PermissionRepository(context);
            var permissionTypeRepo = new PermissionTypeRepository(context);
            var permissionTypeService = new PermissionTypeService(permissionTypeRepo);

            var elasticMock = new Mock<IElasticsearchService>();
            var kafkaMock = new Mock<IKafkaService>();
            var unitOfWork = new UnitOfWorkAsync(context);
            var mapper = CreateMapper();

            return new PermissionService(
                permissionRepo,
                permissionTypeService,
                elasticMock.Object,
                kafkaMock.Object,
                unitOfWork,
                mapper
            );
        }

        [Fact]
        public async Task CreatePermissionAsync_ShouldCreatePermission()
        {
            // Arrange
            var context = CreateDbContext();
            var service = CreateService(context);

            var creationData = new PermissionCreationData("Integration", "Test", 1, DateTime.UtcNow);

            // Act
            var created = await service.CreatePermissionAsync(creationData, CancellationToken.None);

            // Assert
            Assert.NotNull(created);
            Assert.Equal(creationData.EmployeeFirstName, created.EmployeeFirstName);
            Assert.Equal(creationData.EmployeeLastName, created.EmployeeLastName);
            Assert.Equal(creationData.PermissionTypeId, created.PermissionTypeId);
            Assert.Equal(creationData.PermissionDate, created.PermissionDate);
        }

        [Fact]
        public async Task GetPermissionsAsync_ShouldReturnPermissions()
        {
            // Arrange
            var context = CreateDbContext();
            var service = CreateService(context);

            // Seed a permission
            var creationData = new PermissionCreationData("Integration", "Test", 2, DateTime.UtcNow);
            await service.CreatePermissionAsync(creationData, CancellationToken.None);

            // Act
            var all = (await service.GetPermissionsAsync(CancellationToken.None)).ToList();

            // Assert
            Assert.Single(all);
            Assert.Equal(creationData.EmployeeFirstName, all[0].EmployeeFirstName);
            Assert.Equal(creationData.EmployeeLastName, all[0].EmployeeLastName);
            Assert.Equal(creationData.PermissionTypeId, all[0].PermissionTypeId);
            Assert.Equal(creationData.PermissionDate, all[0].PermissionDate);
        }

        [Fact]
        public async Task ModifyPermissionAsync_ShouldModifyPermission()
        {
            // Arrange
            var context = CreateDbContext();
            var service = CreateService(context);

            // Seed a permission
            var creationData = new PermissionCreationData("Integration", "Test", 1, DateTime.UtcNow);
            var created = await service.CreatePermissionAsync(creationData, CancellationToken.None);

            var modificationData = new PermissionModificationData(
                created.Id, "Integration2", "Test2", 2, DateTime.UtcNow.AddDays(1));

            // Act
            var modified = await service.ModifyPermissionAsync(modificationData, CancellationToken.None);

            // Assert
            Assert.NotNull(modified);
            Assert.Equal(modificationData.EmployeeFirstName, modified.EmployeeFirstName);
            Assert.Equal(modificationData.EmployeeLastName, modified.EmployeeLastName);
            Assert.Equal(modificationData.PermissionTypeId, modified.PermissionTypeId);
            Assert.Equal(modificationData.PermissionDate, modified.PermissionDate);
        }
    }
}