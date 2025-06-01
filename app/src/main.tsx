import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import App from "./App";
import { ThemeProvider, CssBaseline } from "@mui/material";
import theme from "./theme";
import LoadingBackdrop from "./components/common/loading-backdrop";
import { LoadingProvider } from "./contexts/LoadingContext";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";

createRoot(document.getElementById("root") as HTMLElement).render(
  <StrictMode>
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <LoadingProvider>
        <LocalizationProvider dateAdapter={AdapterDayjs}>
          <LoadingBackdrop />
          <App />
        </LocalizationProvider>
      </LoadingProvider>
    </ThemeProvider>
  </StrictMode>
);
