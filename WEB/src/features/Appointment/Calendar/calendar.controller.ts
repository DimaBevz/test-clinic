import { EventClickArg } from "@fullcalendar/core/index.js";
import { ISessionModel } from "../AppointmentList/appointment-list.interface";
import { useState } from "react";

interface IProps {
  data?: ISessionModel[];
}

function useCalendarController({ data }: IProps) {
  const [isModalOpened, setIsModalOpened] = useState<boolean>(false);
  const [eventData, setEventData] = useState<EventClickArg>();

  const sessionInfo = eventData?.event._def.extendedProps.sessionInfo;

  const mappedData = data?.map((session) => ({
    title: `${session.patient.firstName} ${session.patient.lastName}`,
    date: session.startTime,
    sessionInfo: session
  }));

  const handleEventClick = (eventClickArg: EventClickArg) => {
    setEventData(eventClickArg);
    setIsModalOpened(true)
  };

  const onCloseModal = () => {
    setIsModalOpened(false)
  }

  const handleEventMouseEnter = (eventInfo: any) => {
    const eventElement = eventInfo.el;
    eventElement.style.backgroundColor = "#ecc94a";
    eventElement.style.cursor = "pointer";
  };

  // Callback function for when the mouse leaves an event
  const handleEventMouseLeave = (eventInfo: any) => {
    const eventElement = eventInfo.el;
    eventElement.style.backgroundColor = "";
  };

  return {
    mappedData,
    isModalOpened,
    sessionInfo,
    handleEventClick,
    onCloseModal,
    handleEventMouseEnter,
    handleEventMouseLeave
  };
}

export default useCalendarController;
