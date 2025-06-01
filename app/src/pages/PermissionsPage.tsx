import React, { useState } from "react";
import { Container, Typography, Snackbar, Alert, Divider } from "@mui/material";
import {
  usePermissions,
  requestPermission,
  modifyPermission,
} from "../features/permissions/hooks";
import {
  Permission,
  PermissionCreateRequest,
  PermissionUpdateRequest,
} from "../features/permissions/types";
import PermissionRequest from "../components/Permission/PermissionRequest";
import PermissionModifyDialog from "../components/Permission/PermissionModifyDialog";
import PermissionList from "../components/Permission/PermissionList";
import { useLoading } from "../contexts/LoadingContext";

const PermissionsPage: React.FC = () => {
  const { permissions, fetchPermissions } = usePermissions();
  const { showLoading, hideLoading } = useLoading();
  const [editPermission, setEditPerm] = useState<Permission | null>(null);
  const [snackbar, setSnackbar] = useState<{
    open: boolean;
    message: string;
    severity: "success" | "error";
  }>({
    open: false,
    message: "",
    severity: "success",
  });

  const handleRequest = async (data: PermissionCreateRequest) => {
    showLoading();
    try {
      await requestPermission(data);
      setSnackbar({
        open: true,
        message: "Permission requested!",
        severity: "success",
      });
      fetchPermissions();
    } catch {
      setSnackbar({
        open: true,
        message: "Error requesting permission",
        severity: "error",
      });
    } finally {
      hideLoading();
    }
  };

  const handleSave = async (id: number, data: PermissionUpdateRequest) => {
    showLoading();
    try {
      await modifyPermission(id, data);
      setSnackbar({
        open: true,
        message: "Permission modified!",
        severity: "success",
      });
      setEditPerm(null);
      fetchPermissions();
    } catch {
      setSnackbar({
        open: true,
        message: "Error modifying permission",
        severity: "error",
      });
    } finally {
      hideLoading();
    }
  };

  return (
    <Container>
      <Typography variant="h4" sx={{ my: 3 }}>
        Permissions
      </Typography>
      <PermissionRequest onSubmit={handleRequest} />
      <Divider />
      <PermissionList permissions={permissions} onEdit={setEditPerm} />
      <PermissionModifyDialog
        open={!!editPermission}
        onClose={() => setEditPerm(null)}
        permission={editPermission}
        onSave={handleSave}
      />
      <Snackbar
        open={snackbar.open}
        autoHideDuration={5000}
        onClose={() => setSnackbar({ ...snackbar, open: false })}
        anchorOrigin={{ vertical: "top", horizontal: "right" }}
      >
        <Alert severity={snackbar.severity}>{snackbar.message}</Alert>
      </Snackbar>
    </Container>
  );
};

export default PermissionsPage;
