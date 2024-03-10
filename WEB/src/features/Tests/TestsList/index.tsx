import { Container } from "@components/Container";
import { Paper } from "@components/Paper";
import useTestsController from "./tests-list.controller";
import { Grid } from "@chakra-ui/react";
import { ITestsRes } from "../tests.interface";
import { TestListCardSkeleton } from "../TestListCardSkeleton";
import { TestsListCard } from "../TestsListCard";

export const TestsList = () => {
  const { data, isLoading } = useTestsController();

  return (
    <Container>
      <Grid className="grid" gap={6}>
        {(isLoading ? [...Array(5)] : data)?.map(
          (obj: ITestsRes, index: number) => {
            return (
              <Paper key={isLoading ? index : obj?.id}>
                {isLoading ? (
                  <TestListCardSkeleton />
                ) : (
                  <TestsListCard info={obj} />
                )}
              </Paper>
            );
          }
        )}
      </Grid>
    </Container>
  );
};
