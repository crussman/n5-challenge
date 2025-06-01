import React, { useState } from "react";
import { Box, Button, Grid } from "@mui/material";
import { PermissionCreateRequest } from "../../features/permissions/types";
import { Dayjs } from "dayjs";
import {
  PermissionFormErrors,
  validatePermissionForm,
} from "../../utils/permissionValidation";
import PermissionForm from "./PermissionForm";

interface Props {
  onSubmit: (data: PermissionCreateRequest) => void;
}

const PermissionRequest: React.FC<Props> = ({ onSubmit }) => {
  const [employeeFirstName, setEmployeeFirstName] = useState("");
  const [employeeLastName, setEmployeeLastName] = useState("");
  const [permissionType, setPermissionTypeId] = useState<number>(0);
  const [permissionDate, setPermissionDate] = useState<Dayjs | null>(null);
  const [errors, setErrors] = useState<PermissionFormErrors>({
    employeeFirstName: "",
    employeeLastName: "",
    permissionType: "",
    permissionDate: "",
  });

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    const { isValid, errors: newErrors } = validatePermissionForm(
      employeeFirstName,
      employeeLastName,
      permissionType,
      permissionDate
    );

    setErrors(newErrors);

    if (!isValid) return;

    onSubmit({
      employeeFirstName,
      employeeLastName,
      permissionTypeId: permissionType,
      permissionDate: permissionDate!.format("YYYY-MM-DD"),
    });

    setEmployeeFirstName("");
    setEmployeeLastName("");
    setPermissionTypeId(0);
    setPermissionDate(null);
  };

  return (
    <Grid container spacing={2}>
      <Grid size={{ xs: 12 }}>
        <PermissionForm
          employeeFirstName={employeeFirstName}
          setEmployeeFirstName={setEmployeeFirstName}
          employeeLastName={employeeLastName}
          setEmployeeLastName={setEmployeeLastName}
          permissionType={permissionType}
          setPermissionType={setPermissionTypeId}
          permissionDate={permissionDate}
          setPermissionDate={setPermissionDate}
          errors={errors}
          onSubmit={handleSubmit}
        />
      </Grid>

      <Grid size={{ xs: 12, sm: 6 }} sx={{ mb: 5 }}>
        <Button
          type="submit"
          variant="contained"
          fullWidth
          onClick={handleSubmit}
          sx={{ mt: 2 }}
        >
          Request Permission
        </Button>
      </Grid>
    </Grid>
  );
};

export default PermissionRequest;
