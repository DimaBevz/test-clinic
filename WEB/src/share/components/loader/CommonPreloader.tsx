import { Spinner } from '@chakra-ui/react'

export interface PreloaderProps {
  size: "xs" | "sm" | "md" | "lg" | "xl";
}

export const CommonPreloader = ({size}: PreloaderProps) => {
  return <Spinner color="#DD6B20" speed="0.65s" size={size} />;
};
