
import { useToast } from "../general/useToast";
import { useEffect } from "react";
import { ApiError } from "@interfaces/general";
import { SerializedError } from "@reduxjs/toolkit";
import { IGetAvailableTimetableRes } from "@features/Doctor/TimeTable/TimeTableForm/time-table-form.interface";
import { useLazyGetAvailableTimetableQuery } from "@api/time-table-form.service";

interface IProps {
  id?: string;
}

export const useAvailableTimetable = ({
  id,
}: IProps): {
    availableTimetableData: IGetAvailableTimetableRes | undefined;
    isAvailableTimetableDataLoading: boolean;
    isAvailableTimetableDataError: boolean;
    availableTimetableDataError: ApiError | SerializedError | undefined;
} => {
  const { errorToast } = useToast();
  const [
    getAvailableTimetable,
    {
      data: availableTimetableData,
      isLoading: isAvailableTimetableDataLoading,
      isError: isAvailableTimetableDataError,
      error: availableTimetableDataError,
    },
  ] = useLazyGetAvailableTimetableQuery();

  useEffect(() => {
    if (id) {
        getAvailableTimetable(id);
    }
  }, [id]);

  useEffect(() => {
    if (isAvailableTimetableDataError) {
      errorToast((availableTimetableDataError as ApiError)?.data?.title as string);
    }
  }, [isAvailableTimetableDataError]);

  return {
    availableTimetableData,
    isAvailableTimetableDataLoading,
    isAvailableTimetableDataError,
    availableTimetableDataError
  };
};
