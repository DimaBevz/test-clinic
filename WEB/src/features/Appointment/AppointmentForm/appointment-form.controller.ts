import { useNavigate, useParams } from "react-router-dom";
import { useForm } from "react-hook-form";
import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";

import { useLazyGetAvailableTimetableQuery } from "@features/TimeTable/TimeTableForm/time-table-form.service";

import {
  IAppointmentFormInput,
  ICreateSessionReq,
} from "./appointment-form.interface";
import { useCreateSessionMutation } from "./appointment-form.service";
import { useToast } from "@hooks/useToast";
import { ApiError } from "@interfaces/general";

const painScale = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

function useAppointmentFormController() {
  const navigate = useNavigate();
  const { successToast, errorToast } = useToast();
  const { t } = useTranslation();
  const { id } = useParams();
  const [
    getAvailableTimetable,
    {
      data: availableTimetableData,
      isLoading: isAvailableTimetableDataLoading,
      isError: isAvailableTimetableDataError,
      error: availableTimetableDataError,
    },
  ] = useLazyGetAvailableTimetableQuery();

  const [
    createSession,
    { isLoading: isCreateSessionLoading, isError: isCreateSessionError },
  ] = useCreateSessionMutation();

  const { control, handleSubmit, watch, setValue } =
    useForm<IAppointmentFormInput>();
  const [activeIndex, setActiveIndex] = useState<number | number[]>(0);

  const onSubmit = async (data: IAppointmentFormInput) => {
    const appointmentForm: ICreateSessionReq = {
      sessionDate: data.date,
      startTime: JSON.parse(watch("timeSlot")).startTime,
      endTime: JSON.parse(watch("timeSlot")).endTime,
      physicianId: id as string,
      description: data.description,
      currentPainScale: data.currentPainScale,
      averagePainScaleLastMonth: data.averagePainScaleLastMonth,
      highestPainScaleLastMonth: data.highestPainScaleLastMonth,
    };
    try {
      await createSession(appointmentForm).unwrap();
      navigate("/doctors");
      successToast(t("AppointmentCreated"));
    } catch (error: any) {
      errorToast(error.data.title);
    }
  };

  const handleDateChange = (date: string) => {
    setValue("date", date);
    setValue("timeSlot", "");
    setActiveIndex(1); // Open the next accordion item
  };

  const handleTimeSlotChange = (timeSlot: string) => {
    setValue("timeSlot", timeSlot);
    setActiveIndex(2); // Open the next accordion item
  };

  const handlePainScaleChange = (
    name:
      | "currentPainScale"
      | "averagePainScaleLastMonth"
      | "highestPainScaleLastMonth",
    painScale: number
  ) => {
    setValue(name, painScale);
  };

  const handleDescriptionChange = (value: string) => {
    setValue("description", value);
  };

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
    availableTimetable: {
      data: availableTimetableData,
      isLoading: isAvailableTimetableDataLoading,
      isError: isAvailableTimetableDataError,
    },
    form: {
      control,
      watch,
      handleSubmit,
      activeIndex,
      onSubmit,
      handleDateChange,
      handleTimeSlotChange,
      handleDescriptionChange,
      handlePainScaleChange,
      setActiveIndex,
    },
    createSessionObj: {
      isLoading: isCreateSessionLoading,
      isError: isCreateSessionError,
    },
    painScale,
  };
}

export default useAppointmentFormController;
