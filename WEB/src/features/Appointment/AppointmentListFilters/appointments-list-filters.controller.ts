// import dayjs from "dayjs";
// import { useEffect } from "react";
// import { useForm } from "react-hook-form";
// import { useLocation, useSearchParams } from "react-router-dom";

// export type FormData = {
//   sessionStartDate: Date;
//   sessionEndDate: Date;
//   sessionStatus: "archived" | "deleted" | "all";
// };

// const statuses = ["all", "archived", "deleted"];

// function useAppointmentListFiltersController() {
//   const { control, handleSubmit, watch, setValue, reset } = useForm<FormData>();
//   const [searchParams, setSearchParams] = useSearchParams();
//   const location = useLocation();

//   const isAppointments = location.pathname === "/appointments";

//   useEffect(() => {
//     // Parse and set default values from URL search parameters
//     const sessionStartDate = searchParams.get("sessionStartDate");
//     const sessionEndDate = searchParams.get("sessionEndDate");
//     const sessionStatus = searchParams.get(
//       "sessionStatus"
//     ) as FormData["sessionStatus"];
//     if (sessionStartDate)
//       setValue("sessionStartDate", new Date(sessionStartDate));
//     if (sessionEndDate) setValue("sessionEndDate", new Date(sessionEndDate));
//     if (sessionStatus) setValue("sessionStatus", sessionStatus);
//   }, [searchParams, setValue]);

//   useEffect(() => {
//     if (isAppointments && searchParams.size === 0) {
//       const now = new Date();
//       reset({
//         sessionStartDate: now,
//         sessionEndDate: now,
//         sessionStatus: "all",
//       });
//       setSearchParams({
//         sessionStartDate: dayjs(now).format("YYYY-MM-DD"),
//         sessionEndDate: dayjs(now).format("YYYY-MM-DD"),
//         sessionStatus: "all",
//       });
//     }
//   }, [isAppointments, searchParams]);

//   const onSubmit = (data: FormData) => {
//     // Update URL search parameters
//     const newSearchParams = new URLSearchParams();
//     if (data.sessionStartDate) {
//       newSearchParams.set(
//         "sessionStartDate",
//         dayjs(data.sessionStartDate).format("YYYY-MM-DD")
//       );
//     }
//     if (data.sessionEndDate) {
//       newSearchParams.set(
//         "sessionEndDate",
//         dayjs(data.sessionEndDate).format("YYYY-MM-DD")
//       );
//     }
//     if (data.sessionStatus)
//       newSearchParams.set("sessionStatus", data.sessionStatus);
//     setSearchParams(newSearchParams);
//   };

//   const handleStatuseChange = (value: string) => {
//     setValue("sessionStatus", value as any);
//   };

//   const onReset = () => {
//     const now = new Date();
//     reset({ sessionStartDate: now, sessionEndDate: now, sessionStatus: "all" });
//     setSearchParams({
//       sessionStartDate: dayjs(now).format("YYYY-MM-DD"),
//       sessionEndDate: dayjs(now).format("YYYY-MM-DD"),
//       sessionStatus: "all",
//     });
//   };

//   return {
//     statuses,
//     onSubmit,
//     handleStatuseChange,
//     onReset,
//     form: {
//       control,
//       handleSubmit,
//       watch
//     },
//   };
// }

// export default useAppointmentListFiltersController;

import dayjs from "dayjs";
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useLocation, useSearchParams } from "react-router-dom";

export type FormData = {
  sessionDateRange: [Date, Date];
  sessionStatus: "archived" | "deleted" | "all";
};

const sessionStatuses = ["all", "archived", "deleted"];

function useAppointmentListFiltersController() {
  const { control, handleSubmit, setValue, reset } = useForm<FormData>();
  const [searchParams, setSearchParams] = useSearchParams();
  const location = useLocation();

  const isAppointments = location.pathname === "/appointments";

  useEffect(() => {
    if (isAppointments && searchParams.size === 0) {
      const defaultSessionDateRange: [Date, Date] = [new Date(), new Date()];
      reset({
        sessionDateRange: defaultSessionDateRange,
        sessionStatus: "all",
      });

      setSearchParams({
        sessionDateRange: defaultSessionDateRange
          .map((date) => dayjs(date).format("YYYY-MM-DD"))
          .join("—"),
        sessionStatus: "all",
      });
    }
  }, [isAppointments, searchParams]);

  useEffect(() => {
    // Parse and set default values from URL search parameters
    const sessionDateRange = searchParams
      .get("sessionDateRange")
      ?.split("—") as [string, string];
    const sessionStatus = searchParams.get(
      "sessionStatus"
    ) as FormData["sessionStatus"];
    if (sessionDateRange)
      setValue("sessionDateRange", [
        new Date(sessionDateRange[0]),
        new Date(sessionDateRange[1]),
      ]);
    setValue("sessionStatus", sessionStatus || "all");
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
    setSearchParams(newSearchParams);
  };

  const onReset = () => {
    // Explicitly typing the default date range as a tuple [Date, Date]
    const defaultSessionDateRange: [Date, Date] = [new Date(), new Date()];
    reset({ sessionDateRange: defaultSessionDateRange, sessionStatus: "all" });

    setSearchParams({
      sessionDateRange: defaultSessionDateRange
        .map((date) => dayjs(date).format("YYYY-MM-DD"))
        .join("—"),
      sessionStatus: "all",
    });
  };

  return {
    sessionStatuses,
    onSubmit,
    onReset,
    form: {
      control,
      handleSubmit,
    },
  };
}

export default useAppointmentListFiltersController;
