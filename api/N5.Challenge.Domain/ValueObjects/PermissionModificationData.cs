namespace N5.Challenge.Domain.ValueObjects;

public record PermissionModificationData(
    int Id,
    string EmployeeFirstName,
    string EmployeeLastName,
    int PermissionTypeId,
    DateTime PermissionDate);