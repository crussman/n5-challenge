import React from "react";
import {
  Grid,
  TextField,
  Select,
  MenuItem,
  Typography,
  Button,
} from "@mui/material";
import { DatePicker } from "@mui/x-date-pickers";
import { Dayjs } from "dayjs";
import { PermissionFormErrors } from "../../utils/permissionValidation";

interface Props {
  employeeFirstName: string;
  setEmployeeFirstName: (value: string) => void;
  employeeLastName: string;
  setEmployeeLastName: (value: string) => void;
  permissionType: number;
  setPermissionType: (value: number) => void;
  permissionDate: Dayjs | null;
  setPermissionDate: (value: Dayjs | null) => void;
  errors: PermissionFormErrors;
  onSubmit: (e: React.FormEvent) => void;
}

const PermissionForm: React.FC<Props> = ({
  employeeFirstName,
  setEmployeeFirstName,
  employeeLastName,
  setEmployeeLastName,
  permissionType,
  setPermissionType,
  permissionDate,
  setPermissionDate,
  errors,
  onSubmit,
}) => (
  <form onSubmit={onSubmit}>
    <Grid container spacing={2}>
      <Grid size={{ xs: 12, sm: 6 }}>
        <TextField
          label="First Name *"
          value={employeeFirstName}
          onChange={(e) => setEmployeeFirstName(e.target.value)}
          fullWidth
          error={!!errors.employeeFirstName}
          helperText={errors.employeeFirstName}
        />
      </Grid>
      <Grid size={{ xs: 12, sm: 6 }}>
        <TextField
          label="Last Name *"
          value={employeeLastName}
          onChange={(e) => setEmployeeLastName(e.target.value)}
          fullWidth
          error={!!errors.employeeLastName}
          helperText={errors.employeeLastName}
        />
      </Grid>
      <Grid size={{ xs: 12, sm: 6 }}>
        <Select
          value={permissionType}
          onChange={(e) => setPermissionType(Number(e.target.value))}
          fullWidth
          displayEmpty
          error={!!errors.permissionType}
          renderValue={(selected) => {
            if (selected === 0) {
              return (
                <Typography
                  color={
                    errors.permissionType ? "error.main" : "text.secondary"
                  }
                >
                  Permission Type
                </Typography>
              );
            }
            return selected === 1 ? "Read" : "Write";
          }}
        >
          <MenuItem value={0} disabled>
            Permission Type
          </MenuItem>
          <MenuItem value={1}>Read</MenuItem>
          <MenuItem value={2}>Write</MenuItem>
        </Select>
        {errors.permissionType && (
          <Typography variant="body2" color="error">
            {errors.permissionType}
          </Typography>
        )}
      </Grid>
      <Grid size={{ xs: 12, sm: 6 }}>
        <DatePicker
          format="DD/MM/YYYY"
          label="Permission Date"
          value={permissionDate}
          onChange={(newValue) => setPermissionDate(newValue)}
          slotProps={{
            textField: {
              fullWidth: true,
              error: !!errors.permissionDate,
              helperText: errors.permissionDate,
            },
          }}
        />
      </Grid>
    </Grid>
  </form>
);

export default PermissionForm;
