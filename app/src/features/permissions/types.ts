export interface Permission {
  id: number;
  employeeFirstName: string;
  employeeLastName: string;
  permissionTypeId: number;
  permissionTypeDescription: string;
  permissionDate: string;
}

export interface PermissionCreateRequest {
  employeeFirstName: string;
  employeeLastName: string;
  permissionTypeId: number;
  permissionDate: string;
}

export interface PermissionUpdateRequest {
  employeeFirstName: string;
  employeeLastName: string;
  permissionTypeId: number;
  permissionDate: string;
}

export interface PermissionType {
  id: number;
  description: string;
}
