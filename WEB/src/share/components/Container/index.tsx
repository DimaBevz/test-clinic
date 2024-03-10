import { Stack } from "@chakra-ui/react";
import { ReactNode } from "react";

import "./index.scss";

interface ContainerProps {
  children: ReactNode;
  [key: string]: any;
}

export const Container = ({ children, ...props }: ContainerProps) => {
  return <Stack className="CustomContainer" gap={5} {...props}>{children}</Stack>;
};
