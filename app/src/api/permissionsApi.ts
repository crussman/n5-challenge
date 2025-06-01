import axios from "axios";
import {
  Permission,
  PermissionCreateRequest,
  PermissionUpdateRequest,
} from "../features/permissions/types";
import { api } from "./api";

export const getPermissions = () =>
  api.get<Permission[]>("/Permissions").then((res) => res.data);

export const requestPermission = (data: PermissionCreateRequest) =>
  api.post("/permissions", data);

export const modifyPermission = (id: number, data: PermissionUpdateRequest) =>
  api.put(`/permissions/${id}`, data);
