import { useLazyGetTestsQuery } from "@api/tests.service";
import { useEffect } from "react";

function useTestsController() {
  const [getTests, { data: testsListData, isLoading: isTestsListDataLoading }] =
    useLazyGetTestsQuery();

  useEffect(() => {
    getTests();
  }, []);

  return {
    data: testsListData,
    isLoading: isTestsListDataLoading,
  };
}

export default useTestsController;
