import { useEffect, useState } from "react";
import { useForm, useFieldArray } from "react-hook-form";
import dayjs from "dayjs";
import { useTranslation } from "react-i18next";

import {
  parseSessionTemplates,
  transformSessionTemplatesData,
} from "@utils/parser";
import {
  ITimeTableFormValues,
  ICreateTimetableReq,
  ITimeSlot,
  IGetTimetableReq,
} from "./time-table-form.interface";
import { useNavigate } from "react-router-dom";
import { useToast } from "@hooks/general/useToast";
import { ApiError } from "@interfaces/general";
import {
  useCreateTimetableMutation,
  useDeleteTimetableMutation,
  useGetTimetableMutation,
  useUpdateTimetableMutation,
} from "@api/time-table-form.service";
import useAuthUser from "@hooks/general/useAuthUser";

const daysOfWeekEng = [
  "Monday",
  "Tuesday",
  "Wednesday",
  "Thursday",
  "Friday",
  "Saturday",
  "Sunday",
];

function useTimeTableFormController() {
  const navigate = useNavigate();
  const { user } = useAuthUser();
  const { successToast, errorToast } = useToast();
  const { t } = useTranslation();

  const [isDayOpen, setIsDayOpen] = useState(
    daysOfWeekEng.reduce((a, v) => ({ ...a, [v]: false }), {})
  );

  const [
    createTimetable,
    { isLoading: isCreateTimetableLoading, isError: isCreateTimetableError },
  ] = useCreateTimetableMutation();
  const [
    updateTimetable,
    { isLoading: isUpdateTimetableLoading, isError: isUpdateTimetableError },
  ] = useUpdateTimetableMutation();
  const [
    deleteTimetable,
    { isLoading: isDeleteTimetableLoading, isError: isDeleteTimetableError },
  ] = useDeleteTimetableMutation();
  const [
    getTimetableTemplete,
    {
      data: getTimetableTemplateData,
      isLoading: isGetTimetableTemplateLoading,
      isError: isGetTimetableTemplateError,
      error: getTimetableTempleteError,
    },
  ] = useGetTimetableMutation();

  const {
    control,
    register,
    handleSubmit,
    formState: { errors },
    getValues,
    setValue,
  } = useForm<ITimeTableFormValues>({
    defaultValues: {
      startDate: new Date(),
      endDate: new Date(),
      sessionTemplates: daysOfWeekEng.map((day) => ({
        isOpen: false,
        dayLabel: day,
        isActive: false,
        timeSlots: [{ startTime: "", endTime: "" }],
      })),
    },
  });

  const sessionTemplates = useFieldArray({
    control,
    name: "sessionTemplates",
  });

  const onSubmit = async (data: ITimeTableFormValues) => {
    const body: ICreateTimetableReq = {
      sessionTemplates: parseSessionTemplates(data.sessionTemplates),
      startDate: dayjs(data.startDate).format("YYYY-MM-DD"),
      endDate: dayjs(data.endDate).format("YYYY-MM-DD"),
      physicianDataId: user?.id,
    };
    const requestData: IGetTimetableReq = {
      physicianId: user?.id,
    };

    try {
      getTimetableTemplateData
        ? await updateTimetable(body).unwrap()
        : await createTimetable(body).unwrap();
      await getTimetableTemplete(requestData);
      successToast(t("ScheduleUpdated"));
    } catch (error: any) {
      errorToast(error.data.title);
    }
  };

  const addTimeSlot = (sessionIndex: number) => {
    const newTimeSlot: ITimeSlot = { startTime: "", endTime: "" };
    const currentSessions = getValues("sessionTemplates");

    currentSessions[sessionIndex].timeSlots.push(newTimeSlot);
    setValue("sessionTemplates", currentSessions);
  };

  const removeTimeSlot = (sessionIndex: number, slotIndex: number) => {
    const currentSessions = getValues("sessionTemplates");
    const updatedSessions = currentSessions.map((session, index) => {
      if (index === sessionIndex) {
        return {
          ...session,
          timeSlots: session.timeSlots.filter((_, idx) => idx !== slotIndex),
        };
      }
      return session;
    });
    setValue("sessionTemplates", updatedSessions);
  };

  const changeAccordionState = (sessionIndex: number) => {
    const currentSessions = getValues("sessionTemplates");
    const updatedSessions = currentSessions.map((session, index) => {
      if (index === sessionIndex) {
        return {
          ...session,
          isOpen: !session.isOpen,
        };
      }
      return session;
    });
    setValue("sessionTemplates", updatedSessions);
  };

  const deleteTimetableFn = async () => {
    try {
      const requestData: IGetTimetableReq = {
        physicianId: user?.id,
      };
      await deleteTimetable(requestData).unwrap();
      navigate("/profile");
    } catch (error: any) {
      errorToast(error.data.title);
    }
  };

  useEffect(() => {
    if (user?.id) {
      const requestData: IGetTimetableReq = {
        physicianId: user?.id,
      };
      getTimetableTemplete(requestData);
    }
  }, [user.id]);

  useEffect(() => {
    if (isGetTimetableTemplateError && getTimetableTempleteError) {
      errorToast(
        (getTimetableTempleteError as ApiError)?.data?.title as string
      );
    }
  }, [isGetTimetableTemplateError, getTimetableTempleteError]);

  useEffect(() => {
    if (getTimetableTemplateData) {
      const { startDate, endDate, sessionTemplates } = getTimetableTemplateData;

      // Transform the session templates data
      const transformedTemplates = transformSessionTemplatesData(
        sessionTemplates,
        daysOfWeekEng
      );

      // Update the form values
      setValue("startDate", new Date(startDate)); // Make sure the format is compatible with your DatePicker
      setValue("endDate", new Date(endDate)); // Make sure the format is compatible with your DatePicker
      setValue("sessionTemplates", transformedTemplates);
    }
  }, [getTimetableTemplateData, setValue]);

  return {
    control,
    register,
    handleSubmit,
    sessionTemplates,
    onSubmit,
    errors,
    addTimeSlot,
    removeTimeSlot,
    changeAccordionState,
    createTimetableTemplate: {
      isLoading: isCreateTimetableLoading,
      isError: isCreateTimetableError,
    },
    updateTimetableTemplate: {
      isLoading: isUpdateTimetableLoading,
      isError: isUpdateTimetableError,
    },
    getTimetable: {
      data: getTimetableTemplateData,
      isLoading: isGetTimetableTemplateLoading,
      isError: isGetTimetableTemplateError,
    },
    deleteTimetable: {
      delete: deleteTimetableFn,
      isLoading: isDeleteTimetableLoading,
      isError: isDeleteTimetableError,
    },
    isDayOpen,
    setIsDayOpen,
  };
}

export default useTimeTableFormController;
