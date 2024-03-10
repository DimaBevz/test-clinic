import { ISessionModel } from "../AppointmentList/appointment-list.interface";
import FullCalendar from "@fullcalendar/react";
import dayGridPlugin from "@fullcalendar/daygrid";
import timeGridPlugin from "@fullcalendar/timegrid";
import { EventSourceInput } from "@fullcalendar/core/index.js";
import interactionPlugin from "@fullcalendar/interaction";
import useCalendarController from "./calendar.controller";
import { ModalWindow } from "@components/ModalWindow";
import { useTranslation } from "react-i18next";
import { AppointmentModalInfo } from "../AppointmentModalInfo";

interface ICalendarProps {
  data?: ISessionModel[];
}

export const Calendar = ({ data }: ICalendarProps) => {
  const { t } = useTranslation();
  const {
    mappedData,
    isModalOpened,
    sessionInfo,
    handleEventClick,
    onCloseModal,
    handleEventMouseEnter,
    handleEventMouseLeave,
  } = useCalendarController({ data });

  return (
    <>
      <FullCalendar
        eventClick={handleEventClick}
        eventMouseEnter={handleEventMouseEnter}
        eventMouseLeave={handleEventMouseLeave}
        events={mappedData as EventSourceInput}
        plugins={[dayGridPlugin, timeGridPlugin, interactionPlugin]}
        initialView="dayGridMonth"
        slotMinTime="08:00:00"
        headerToolbar={{
          left: "",
          center: "",
          right: "timeGridWeek,timeGridDay,dayGridMonth",
        }}
      />
      <ModalWindow
        title={t("AppointmentInfo")}
        isOpen={isModalOpened}
        onClose={onCloseModal}
        content={
          <AppointmentModalInfo
            sessionInfo={sessionInfo}
          />
        }
      />
    </>
  );
};
