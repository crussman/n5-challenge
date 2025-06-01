namespace N5.Challenge.Application.Dtos;

public record PermissionDto(
    int Id,
    string EmployeeFirstName,
    string EmployeeLastName,
    int PermissionTypeId,
    string PermissionTypeDescription,
    DateTime PermissionDate);