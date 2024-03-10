import { useEffect } from "react";
import { ApiError } from "@interfaces/general";
import { SerializedError } from "@reduxjs/toolkit";
import { ITestsRes } from "@features/Tests/tests.interface";
import { useToast } from "@hooks/general/useToast.ts";
import { useLazyGetTestsResultsListQuery } from "@api/tests.service.ts";

interface IProps {
  patientId?: string;
}

export const useTestsResultList = ({
  patientId,
}: IProps): {
  testsListResult?: ITestsRes[];
  isTestsListResultLoading: boolean;
  isTestsListResultError: boolean;
  testsListResultError: ApiError | SerializedError | undefined;
} => {
  const { errorToast } = useToast();
  const [
    getTestsResultListQuery,
    {
      data: testsListResult,
      isLoading: isTestsListResultLoading,
      isError: isTestsListResultError,
      error: testsListResultError,
    },
  ] = useLazyGetTestsResultsListQuery();

  useEffect(() => {
    if (patientId) {
      getTestsResultListQuery(patientId);
    }
  }, [patientId]);

  useEffect(() => {
    if (isTestsListResultError) {
      errorToast((testsListResultError as ApiError)?.data?.title as string);
    }
  }, [isTestsListResultError]);

  return {
    testsListResult,
    isTestsListResultLoading,
    isTestsListResultError,
    testsListResultError,
  };
};
