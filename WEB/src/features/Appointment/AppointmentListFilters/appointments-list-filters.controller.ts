import useAuthUser from "@hooks/general/useAuthUser";
import dayjs from "dayjs";
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useLocation, useSearchParams } from "react-router-dom";

export type FormData = {
  sessionDateRange: [Date, Date];
  sessionStatus: "archived" | "deleted" | "all";
  type: string;
};

const sessionStatuses = ["all", "archived", "deleted"];

function useAppointmentListFiltersController() {
  const { control, handleSubmit, setValue, reset } = useForm<FormData>();
  const [searchParams, setSearchParams] = useSearchParams();
  const { isDoctor } = useAuthUser();
  const location = useLocation();
  const listType = searchParams.get(
    "type"
  );
  const sessionDateRange = searchParams.get("sessionDateRange")?.split("—") as [
    string,
    string
  ];
  const sessionStatus = searchParams.get(
    "sessionStatus"
  ) as FormData["sessionStatus"];
  
  const isListType = listType === "list";
  const isAppointments = location.pathname === "/appointments";

  useEffect(() => {
    if (isAppointments && searchParams.size === 0) {
      const defaultSessionDateRange: [Date, Date] = [new Date(), new Date()];
      reset({
        sessionDateRange: defaultSessionDateRange,
        sessionStatus: "all",
        type: "list"
      });

      setSearchParams({
        sessionDateRange: defaultSessionDateRange
          .map((date) => dayjs(date).format("YYYY-MM-DD"))
          .join("—"),
        sessionStatus: "all",
        type: "list"
      });
    }
  }, [isAppointments, searchParams]);

  useEffect(() => {
    // Parse and set default values from URL search parameters
    const sessionDateRange = searchParams.get("sessionDateRange")?.split("—") as [string, string];
    
    const sessionStatus = searchParams.get("sessionStatus") as FormData["sessionStatus"];
    
    const type = searchParams.get("type");
    
    if (sessionDateRange)
      setValue("sessionDateRange", [
        new Date(sessionDateRange[0]),
        new Date(sessionDateRange[1]),
      ]);
    setValue("sessionStatus", sessionStatus || "all");
    setValue("type", type || "list");
  }, [searchParams, setValue]);

  const onSubmit = (data: FormData) => {
    // Update URL search parameters
    const newSearchParams = new URLSearchParams();
    if (data.sessionDateRange) {
      newSearchParams.set(
        "sessionDateRange",
        data.sessionDateRange
          .map((date) => dayjs(date).format("YYYY-MM-DD"))
          .join("—")
      );
    }
    newSearchParams.set("sessionStatus", data.sessionStatus);
    newSearchParams.set("type", data.type);
    setSearchParams(newSearchParams);
  };

  const onReset = () => {
    // Explicitly typing the default date range as a tuple [Date, Date]
    const defaultSessionDateRange: [Date, Date] = [new Date(), new Date()];
    reset({ sessionDateRange: defaultSessionDateRange, sessionStatus: "all",  type: "list", });
    setSearchParams({
      sessionDateRange: defaultSessionDateRange
        .map((date) => dayjs(date).format("YYYY-MM-DD"))
        .join("—"),
      sessionStatus: "all",
      type: "list",
    });
  };
  
  const onChangeListType = () => {
    if (isListType) {
      setSearchParams({
        sessionDateRange: sessionDateRange
          .map((date) => dayjs(date).format("YYYY-MM-DD"))
          .join("—"),
        sessionStatus: sessionStatus,
        type: "calendar"
      });
    } else {
      setSearchParams({
        sessionDateRange: sessionDateRange
          .map((date) => dayjs(date).format("YYYY-MM-DD"))
          .join("—"),
        sessionStatus: sessionStatus,
        type: "list"
      });
    }
  }

  return {
    sessionStatuses,
    onSubmit,
    onReset,
    onChangeListType,
    isListType,
    form: {
      control,
      handleSubmit,
    },
    isDoctor
  };
}

export default useAppointmentListFiltersController;
