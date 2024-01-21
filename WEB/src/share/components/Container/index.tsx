import { Stack } from "@chakra-ui/react";
import { ReactNode } from "react";

import "./index.scss";

interface CustomContainerProps {
  children: ReactNode;
  [key: string]: any;
}

export const CustomContainer = ({ children, ...props }: CustomContainerProps) => {
  return <Stack className="CustomContainer" gap={5} {...props}>{children}</Stack>;
};
