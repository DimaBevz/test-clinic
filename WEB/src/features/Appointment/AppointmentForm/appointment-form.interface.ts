import { IDoctorsFullModel } from "@interfaces/doctor";
import { IPatientsFullModel } from "@interfaces/patient";

export interface IAppointmentFormInput {
  date: string;
  timeSlot: string;
  description: string;
  currentPainScale: number;
  averagePainScaleLastMonth: number;
  highestPainScaleLastMonth: number;
}

export interface ICreateSessionReq {
  sessionDate: string;
  startTime: string;
  endTime: string;
  physicianId: string;
  description: string;
  currentPainScale: number;
  averagePainScaleLastMonth: number;
  highestPainScaleLastMonth: number;
}

export interface ICreateSessionRes {
  id: string;
  startTime: Date;
  endTime: Date;
  physicianId: string;
  patientId: string;
  complaints: string;
  treatment: string;
  recommendations: string;
  documents: [
    {
      title: string;
      presignedUrl: string;
    }
  ];
}

export interface IAppointmentInfo {
  id: string;
  startTime: Date;
  endTime: Date;
  physician: IDoctorsFullModel;
  patient: IPatientsFullModel;
  currentPainScale: number;
  averagePainScaleLastMonth: number;
  highestPainScaleLastMonth: number;
  complaints: string;
  treatment: string;
  recommendations: string;
  diagnosisTitle: string;
  isArchived: boolean;
  isDeleted: boolean;
  documents: [
    {
      title: string;
      presignedUrl: string;
    }
  ];
}
