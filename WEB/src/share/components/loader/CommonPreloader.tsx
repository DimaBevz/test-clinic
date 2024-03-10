import { Spinner } from '@chakra-ui/react'

export interface PreloaderProps {
  size: "xs" | "sm" | "md" | "lg" | "xl";
}

export const CommonPreloader = ({size}: PreloaderProps) => {
  return <Spinner color="#ECC94B" speed="0.65s" size={size} />;
};
