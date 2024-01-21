import { object, string, array, date, InferType, boolean } from "yup";

const timeSlotSchema = object({
  startTime: string().required("Start time is required"),
  endTime: string().required("End time is required"),
});

const sessionTemplateSchema = object({
  dayLabel: string().required("Day label is required"),
  timeSlots: array().of(timeSlotSchema),
  IsActive: boolean().required("Disabled status is required")
});

const timeTableFormSchema = object({
    startDate: date().required("Start date is required"),
    endDate: date()
      .required("End date is required")
      .test(
        "end-date",
        "End date must be greater than start date",
        function (value) {
          const { startDate } = this.parent;
          return !startDate || !value || startDate <= value;
        }
      ),
  sessionTemplates: array().of(sessionTemplateSchema),
});

export type TimeTableFormSchema = InferType<typeof timeTableFormSchema>;
export default timeTableFormSchema;
