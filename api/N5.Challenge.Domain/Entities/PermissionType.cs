﻿namespace N5.Challenge.Domain.Entities;

public class PermissionType
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public ICollection<Permission> Permissions { get; set; } = [];
}