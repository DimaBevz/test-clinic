import { Box } from "@chakra-ui/react";
import { ReactNode } from "react";

import './index.scss';

interface CustomPaperProps {
  children: ReactNode;
  [key: string]: any;
}

export const CustomPaper = ({ children, ...props }: CustomPaperProps) => {
  return <Box className="CustomPaper" {...props}>{children}</Box>;
};
