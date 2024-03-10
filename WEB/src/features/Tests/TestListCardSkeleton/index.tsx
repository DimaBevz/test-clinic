import { Box, Skeleton } from "@chakra-ui/react";
import "./index.scss";

export const TestListCardSkeleton = () => {
  return (
    <Box className="TestListCardSkeleton">
      <Box className="TestListCardSkeleton__header">
        <Skeleton height={3} width="100%" />
        <Skeleton height={3} width="100%" />
      </Box>
      <Box className="TestListCardSkeleton__content">
        <Skeleton height={3} width="100%" />
        <Skeleton height={3} width="100%" />
        <Skeleton height={3} width="100%" />
      </Box>
      <Box className="TestListCardSkeleton__actions">
        <Skeleton height={7} width="20%" />
      </Box>
    </Box>
  );
};
