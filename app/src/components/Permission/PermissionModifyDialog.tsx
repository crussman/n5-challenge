import React, { useState, useEffect } from "react";
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
} from "@mui/material";
import dayjs, { Dayjs } from "dayjs";
import {
  Permission,
  PermissionUpdateRequest,
} from "../../features/permissions/types";
import {
  PermissionFormErrors,
  validatePermissionForm,
} from "../../utils/permissionValidation";
import PermissionForm from "./PermissionForm";

interface Props {
  open: boolean;
  onClose: () => void;
  permission: Permission | null;
  onSave: (id: number, data: PermissionUpdateRequest) => void;
}

const PermissionModifyDialog: React.FC<Props> = ({
  open,
  onClose,
  permission,
  onSave,
}) => {
  const [employeeFirstName, setEmployeeFirstName] = useState("");
  const [employeeLastName, setEmployeeLastName] = useState("");
  const [permissionType, setPermissionType] = useState<number>(0);
  const [permissionDate, setPermissionDate] = useState<Dayjs | null>(null);
  const [errors, setErrors] = useState<PermissionFormErrors>({
    employeeFirstName: "",
    employeeLastName: "",
    permissionType: "",
    permissionDate: "",
  });

  useEffect(() => {
    if (!permission) return;

    setEmployeeFirstName(permission.employeeFirstName);
    setEmployeeLastName(permission.employeeLastName);
    setPermissionType(permission.permissionTypeId);
    setPermissionDate(
      permission.permissionDate ? dayjs(permission.permissionDate) : null
    );
  }, [permission, open]);

  useEffect(() => {
    if (open) return;

    setEmployeeFirstName("");
    setEmployeeLastName("");
    setPermissionType(0);
    setPermissionDate(null);
    setErrors({
      employeeFirstName: "",
      employeeLastName: "",
      permissionType: "",
      permissionDate: "",
    });
  }, [open]);

  const handleSave = (e: React.FormEvent) => {
    e.preventDefault();

    const { isValid, errors: newErrors } = validatePermissionForm(
      employeeFirstName,
      employeeLastName,
      permissionType,
      permissionDate
    );

    setErrors(newErrors);

    if (!isValid) return;

    if (permission) {
      onSave(permission.id, {
        employeeFirstName,
        employeeLastName,
        permissionTypeId: permissionType,
        permissionDate: permissionDate!.format("YYYY-MM-DD"),
      });
    }
  };

  return (
    <Dialog open={open} onClose={onClose} maxWidth="md" fullWidth>
      <DialogTitle>Modify Permission</DialogTitle>
      <DialogContent sx={{ pt: "1rem !important" }}>
        <PermissionForm
          employeeFirstName={employeeFirstName}
          setEmployeeFirstName={setEmployeeFirstName}
          employeeLastName={employeeLastName}
          setEmployeeLastName={setEmployeeLastName}
          permissionType={permissionType}
          setPermissionType={setPermissionType}
          permissionDate={permissionDate}
          setPermissionDate={setPermissionDate}
          errors={errors}
          onSubmit={handleSave}
        />
      </DialogContent>
      <DialogActions sx={{ mr: 2, gap: 2 }}>
        <Button onClick={onClose}>Cancel</Button>
        <Button type="submit" variant="contained" onClick={handleSave}>
          Save
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default PermissionModifyDialog;
