import { createTheme } from "@fluentui/react";

export const appTheme = createTheme({
  defaultFontStyle: {
    fontFamily: "Segoe UI",
    fontWeight: "regular",
  },
  fonts: {
    small: {
      fontSize: "11px",
    },
    medium: {
      fontSize: "15px",
    },
    large: {
      fontSize: "20px",
      fontWeight: "semibold",
    },
    xLarge: {
      fontSize: "22px",
      fontWeight: "semibold",
    },
  },
  palette: {
    themePrimary: "#0078d4",
    themeSecondary: "#323130",
    themeTertiary: "#ffffff",
  },
});
