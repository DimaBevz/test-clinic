import { useToast } from "../general/useToast";
import { useEffect } from "react";
import { ApiError } from "@interfaces/general";
import { SerializedError } from "@reduxjs/toolkit";
import { ITest } from "@features/Tests/tests.interface";
import { useLazyGetTestByIdQuery } from "@api/tests.service";

interface IProps {
  testId: string;
}

export const useTestInfo = ({
  testId,
}: IProps): {
  testInfoData?: ITest;
  isTestInfoLoading: boolean;
  isTestInfoError: boolean;
  testInfoError: ApiError | SerializedError | undefined;
} => {
  const { errorToast } = useToast();
  const [
    getTestInfoQuery,
    {
      data: testInfoData,
      isLoading: isTestInfoLoading,
      isError: isTestInfoError,
      error: testInfoError,
    },
  ] = useLazyGetTestByIdQuery();

  useEffect(() => {
    if (testId) {
      getTestInfoQuery(testId);   
    }
  }, [testId]);

  useEffect(() => {
    if (isTestInfoError) {
      errorToast((testInfoError as ApiError)?.data?.title as string);
    }
  }, [isTestInfoError]);

  return {
    testInfoData,
    isTestInfoLoading,
    isTestInfoError,
    testInfoError,
  };
};
