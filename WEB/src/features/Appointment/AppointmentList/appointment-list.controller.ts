import { useEffect } from "react";
import { useToast } from "@hooks/useToast";
import { useGetSessionListMutation } from "../AppointmentForm/appointment-form.service";
import { ApiError } from "@interfaces/general";
import { useAuthUserController } from "@features/auth";
import { IGetSessionListReq } from "./appointment-list.interface";
import { useSearchParams } from "react-router-dom";
import { FormData } from "../AppointmentListFilters/appointments-list-filters.controller";

function useAppointmentListController() {
  const [searchParams] = useSearchParams();
  const { errorToast } = useToast();
  const { user } = useAuthUserController();
  const sessionDateRange = searchParams.get("sessionDateRange")?.split("—") as [
    string,
    string
  ];
  const sessionStatus = searchParams.get(
    "sessionStatus"
  ) as FormData["sessionStatus"];

  const [
    getSessionList,
    {
      data: getSessionListData,
      isLoading: isGetSessionListLoading,
      isError: isGetSessionListError,
      error: getSessionListError,
    },
  ] = useGetSessionListMutation();

  useEffect(() => {
    if (isGetSessionListError) {
      errorToast((getSessionListError as ApiError)?.data?.title as string);
    }
  }, [isGetSessionListError]);

  useEffect(() => {
    if (user?.id && sessionDateRange?.[0] && sessionDateRange?.[1] && sessionStatus) {
      const filters = {
        startTime: new Date(sessionDateRange[0]), // Add date filter if it exists
        endTime: new Date(sessionDateRange[1]), // Add date filter if it exists
        showArchived: sessionStatus === "archived" ? true : undefined,
        showDeleted: sessionStatus === "deleted" ? true : undefined,
      };

      const requestModel: IGetSessionListReq = {
        ...(user?.role === 0
          ? { patientId: user?.id }
          : { physicianId: user?.id }),
        ...filters,
      };
      getSessionList(requestModel);
    }
  }, [user.id, sessionDateRange?.[0], sessionDateRange?.[1], sessionStatus]); // Depend on user ID, date, and status

  return {
    sessionList: {
      data: getSessionListData?.sessions ?? [],
      isLoading: isGetSessionListLoading,
      isError: isGetSessionListError,
    },
  };
}

export default useAppointmentListController;
