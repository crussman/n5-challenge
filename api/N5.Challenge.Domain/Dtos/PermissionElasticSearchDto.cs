namespace N5.Challenge.Domain.Dtos;

public record PermissionElasticSearchDto(
    int Id,
    string EmployeeFirstName,
    string EmployeeLastName,
    int PermissionTypeId,
    string PermissionTypeDescription,
    DateTime PermissionDate
);