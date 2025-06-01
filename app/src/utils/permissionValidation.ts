import { Dayjs } from "dayjs";

export interface PermissionFormErrors {
  employeeFirstName: string;
  employeeLastName: string;
  permissionType: string;
  permissionDate: string;
}

export const validatePermissionForm = (
  employeeFirstName: string,
  employeeLastName: string,
  permissionType: number,
  permissionDate: Dayjs | null
): { isValid: boolean; errors: PermissionFormErrors } => {
  let isValid = true;
  const errors: PermissionFormErrors = {
    employeeFirstName: "",
    employeeLastName: "",
    permissionType: "",
    permissionDate: "",
  };

  if (!employeeFirstName) {
    errors.employeeFirstName = "First Name is required";
    isValid = false;
  }

  if (!employeeLastName) {
    errors.employeeLastName = "Last Name is required";
    isValid = false;
  }

  if (!permissionType || permissionType === 0) {
    errors.permissionType = "Permission Type is required";
    isValid = false;
  }

  if (!permissionDate) {
    errors.permissionDate = "Permission Date is required";
    isValid = false;
  }

  return { isValid, errors };
};
