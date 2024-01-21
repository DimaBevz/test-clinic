import { Outlet } from "react-router-dom";

import { Box } from "@chakra-ui/react";
import { Header } from "../Header";
import { Navigation } from "../Navigation";

import './index.scss';

export const MainLayout = () => {
  return (
    <Box className="MainLayout">
      <Header />
      <Box className="MainLayout__content">
        <Navigation />
        <Outlet />
      </Box>
    </Box>
  );
};
