
import { Box } from "@chakra-ui/react";
import { CommonPreloader, PreloaderProps } from "@components/loader/CommonPreloader";

export const Preloader = ({ size }: PreloaderProps) => {
  return <Box style={ {
    position: "absolute",
    top: "50%",
    left: "50%",
    transform: "translate(-50%, -50%)",
  } }>
    <CommonPreloader size={ size }/>
  </Box>;
};
