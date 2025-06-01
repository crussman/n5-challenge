namespace N5.Challenge.Domain.ValueObjects;

public record PermissionCreationData(
    string EmployeeFirstName,
    string EmployeeLastName,
    int PermissionTypeId,
    DateTime PermissionDate);
