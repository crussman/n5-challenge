import { useCallback, useEffect, useState } from "react";
import {
  getPermissions,
  requestPermission,
  modifyPermission,
} from "../../api/permissionsApi";
import { Permission } from "./types";
import { useLoading } from "../../contexts/LoadingContext";

export function usePermissions() {
  const [permissions, setPermissions] = useState<Permission[]>([]);
  const { showLoading, hideLoading } = useLoading();

  const fetchPermissions = useCallback(async () => {
    showLoading();
    try {
      setPermissions(await getPermissions());
    } finally {
      hideLoading();
    }
  }, [showLoading, hideLoading]);

  useEffect(() => {
    fetchPermissions();
  }, [fetchPermissions]);

  return { permissions, fetchPermissions };
}

export { requestPermission, modifyPermission };
