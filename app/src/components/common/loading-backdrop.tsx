import React from "react";
import { Backdrop, Box, CircularProgress } from "@mui/material";
import { useLoading } from "../../contexts/LoadingContext";

const LoadingBackdrop: React.FC = () => {
  const { isLoading } = useLoading();

  return (
    <Backdrop open={isLoading} style={{ zIndex: 1300, color: "#fff" }}>
      <CircularProgress />
    </Backdrop>
  );
};

export default LoadingBackdrop;
