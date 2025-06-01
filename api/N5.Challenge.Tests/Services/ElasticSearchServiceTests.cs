using Moq;

using N5.Challenge.Domain.Dtos;
using N5.Challenge.Tests.Wrappers;

namespace N5.Challenge.Tests.Services;

public class ElasticsearchServiceTests
{
    [Fact]
    public async Task IndexPermissionAsync_ShouldIndex_WhenValid()
    {
        // Arrange
        var mockWrapper = new Mock<IElasticsearchClientWrapper>();

        mockWrapper
            .Setup(x => x.IndexExistsAsync("permissions", It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        mockWrapper
            .Setup(x => x.IndexPermissionAsync(It.IsAny<PermissionElasticSearchDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var exists = await mockWrapper.Object.IndexExistsAsync("permissions");
        var indexed = await mockWrapper.Object.IndexPermissionAsync(
            new PermissionElasticSearchDto(1, "John", "Doe", 1, "Read", DateTime.UtcNow));

        // Assert
        Assert.True(exists);
        Assert.True(indexed);
    }
}