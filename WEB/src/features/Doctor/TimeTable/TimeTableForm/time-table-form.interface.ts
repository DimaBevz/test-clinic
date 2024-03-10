import { ISpecialty } from "@interfaces/specialty.ts";

export interface ITimeSlot {
  startTime: string | undefined;
  endTime: string | undefined;
}

export interface ISessionTemplateFormValues {
  isOpen: boolean;
  dayLabel: string;
  timeSlots: ITimeSlot[];
  isActive: boolean;
}

export interface ISessionTemplate {
  [key: string]: {
    isActive: boolean;
    sessionTimeTemplates: ITimeSlot[];
  };
}

export interface ITimeTableFormValues {
  startDate: Date;
  endDate: Date;
  sessionTemplates: ISessionTemplateFormValues[];
}

export interface ICreateTimetableReq {
  startDate: string;
  endDate: string;
  sessionTemplates: any;
  physicianDataId?: string;
}

export interface IGetTimetableRes {
  startDate: Date;
  endDate: Date;
  sessionTemplates: ISessionTemplate;
  physicianDataId: string;
  isEditable: boolean;
}

export interface IGetTimetableReq {
  startDate?: Date;
  endDate?: Date;
  physicianId: string;
  patientId?: string;
}

export interface IGetAvailableTimetableRes {
  availableSessions: ISessionTemplate;
  physician: {
    firstName: string;
    lastName: string;
    patronymic: string;
    photoUrl: string;
    positions: ISpecialty[];
  };
}
