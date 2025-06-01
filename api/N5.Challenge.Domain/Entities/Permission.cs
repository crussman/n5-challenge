namespace N5.Challenge.Domain.Entities;

public class Permission
{
    public int Id { get; set; }
    public string EmployeeFirstName { get; set; } = null!;
    public string EmployeeLastName { get; set; } = null!;
    public int PermissionTypeId { get; set; }
    public DateTime PermissionDate { get; set; }

    public PermissionType PermissionType { get; set; } = null!;
}