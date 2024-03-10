
import { useToast } from "../general/useToast";
import { useEffect } from "react";
import { ApiError } from "@interfaces/general";
import { IAppointmentInfo } from "@features/Appointment/AppointmentForm/appointment-form.interface";
import { SerializedError } from "@reduxjs/toolkit";
import { useLazyGetSessionInfoQuery } from "@api/session.service";

interface IProps {
  appointmentId: string;
}

export const useAppointmentInfo = ({
  appointmentId,
}: IProps): {
    sessionInfoData: IAppointmentInfo | undefined;
    isSessionInfoLoading: boolean;
    isSessionInfoError: boolean;
    sessionInfoError: ApiError | SerializedError | undefined;
} => {
  const { errorToast } = useToast();
  const [
    getSessionInfoQuery,
    {
      data: sessionInfoData,
      isLoading: isSessionInfoLoading,
      isError: isSessionInfoError,
      error: sessionInfoError,
    },
  ] = useLazyGetSessionInfoQuery();

  useEffect(() => {
    if (appointmentId) {
      getSessionInfoQuery(appointmentId);
    }
  }, [appointmentId]);

  useEffect(() => {
    if (isSessionInfoError) {
      errorToast((sessionInfoError as ApiError)?.data?.title as string);
    }
  }, [isSessionInfoError]);

  return {
    sessionInfoData,
    isSessionInfoLoading,
    isSessionInfoError,
    sessionInfoError,
  };
};
