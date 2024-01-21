import { useEffect } from "react";
import { useLazyGetSessionInfoQuery } from "../AppointmentForm/appointment-form.service";
import { useToast } from "@hooks/useToast";
import { useParams } from "react-router-dom";
import { ApiError } from "@interfaces/general";

function useAppointmentInfoController() {
  const { errorToast } = useToast();
  const { id } = useParams();
  const [
    getSessionInfoQuery,
    {
      data: getSessionInfoData,
      isLoading: isGetSessionInfoLoading,
      isError: isGetSessionInfoError,
      error: getSessionInfoError,
    },
  ] = useLazyGetSessionInfoQuery();

  useEffect(() => {
    if (id) {
      getSessionInfoQuery(id);
    }
  }, [id]);

  useEffect(() => {
    if (isGetSessionInfoError) {
      errorToast((getSessionInfoError as ApiError)?.data?.title as string);
    }
  }, [isGetSessionInfoError]);

  return {
    getSessionInfo: {
      data: getSessionInfoData,
      isLoading: isGetSessionInfoLoading,
      isError: isGetSessionInfoError,
      error: getSessionInfoError,
    },
  };
}

export default useAppointmentInfoController;
