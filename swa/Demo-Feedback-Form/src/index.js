import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import { appTheme } from "./theme";
import { ThemeProvider } from "@fluentui/react";
import App from "./App";
import "bootstrap/dist/css/bootstrap.min.css";

ReactDOM.render(
  <React.StrictMode>
    <ThemeProvider theme={appTheme}>
      <App />
    </ThemeProvider>
  </React.StrictMode>,
  document.getElementById("root")
);

