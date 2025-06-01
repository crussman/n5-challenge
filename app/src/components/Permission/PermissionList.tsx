import React from "react";
import {
  List,
  ListItem,
  ListItemText,
  IconButton,
  Divider,
  Box,
} from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import { Permission } from "../../features/permissions/types";
import dayjs from "dayjs";

interface Props {
  permissions: Permission[];
  onEdit: (permission: Permission) => void;
}

const PermissionList: React.FC<Props> = ({ permissions, onEdit }) => (
  <List>
    {permissions.map((permission) => (
      <React.Fragment key={permission.id}>
        <ListItem
          secondaryAction={
            <IconButton edge="end" onClick={() => onEdit(permission)}>
              <EditIcon />
            </IconButton>
          }
        >
          <ListItemText
            primary={`${permission.employeeFirstName} ${permission.employeeLastName} (${permission.permissionTypeDescription} Permission)`}
            secondary={`Date: ${dayjs(permission.permissionDate).format(
              "DD/MM/YYYY"
            )}`}
          />
        </ListItem>
        <Divider />
      </React.Fragment>
    ))}
    {permissions.length === 0 && (
      <Box textAlign="center" p={2} color="text.secondary">
        No permissions found. Try adding a new one!
      </Box>
    )}
  </List>
);

export default PermissionList;
