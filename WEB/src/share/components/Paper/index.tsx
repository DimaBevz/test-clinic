import { Box } from "@chakra-ui/react";
import { ReactNode } from "react";

import './index.scss';

interface PaperProps {
  children: ReactNode;
  [key: string]: any;
}

export const Paper = ({ children, ...props }: PaperProps) => {
  return <Box className="Paper" {...props}>{children}</Box>;
};
