import { ISessionTemplate, ISessionTemplateFormValues, ITimeSlot } from "@features/Doctor/TimeTable/TimeTableForm/time-table-form.interface";
import dayjs from "dayjs";

export const parseSessionTemplates = (
  sessionTemplatesArray: ISessionTemplateFormValues[]
): { [dayLabel: string]: ISessionTemplate } => {
  const sessionTemplatesObject: { [dayLabel: string]: ISessionTemplate } = {};

  sessionTemplatesArray.forEach(({ dayLabel, timeSlots, isActive }) => {
    const formattedTimeSlots = timeSlots
      .filter(({ startTime, endTime }) => startTime && startTime !== "" && endTime && endTime !== "")
      .map(({ startTime, endTime }) => {
        return {
        startTime: dayjs(startTime, { utc: true, }).format(),
        endTime: dayjs(endTime, { utc: true, }).format(),
      }});
      
    // Only add the day to the object if the array of time slots is not empty
    if (formattedTimeSlots.length > 0) {
      sessionTemplatesObject[dayLabel] = {
        isActive, // Assuming default value for isActive
        sessionTimeTemplates: formattedTimeSlots
      } as any;
    }
  });

  return sessionTemplatesObject;
};


export function transformSessionTemplatesData(
  sessionTemplatesData: ISessionTemplate,
  daysOfWeekEng: string[]
): ISessionTemplateFormValues[] {
  const sessionTemplates = daysOfWeekEng.map((day: string) => {
    const dayData = sessionTemplatesData[day];

    return {
      isOpen: false,
      dayLabel: day,
      isActive: dayData?.isActive,
      timeSlots: dayData ? (dayData?.sessionTimeTemplates??[]).map((slot: ITimeSlot) => ({
        startTime: slot.startTime ? dayjs(slot.startTime, { utc: true, }).format() : undefined, 
        endTime: slot.endTime ? dayjs(slot.endTime, { utc: true, }).format()  : undefined     
      })) : [{ startTime: undefined, endTime: undefined }] // Default empty slot
    };
  });

  return sessionTemplates;
}

