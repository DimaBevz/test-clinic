export interface IGetSessionListReq {
  physicianId?: string;
  patientId?: string;
  startTime?: Date;
  endTime?: Date;
  showArchived?: boolean;
  showDeleted?: boolean;
}

export interface IUserSessionModel {
  id: string;
  lastName: string;
  firstName: string;
  patronymic: string;
}

export interface ISessionModel {
  sessionId: string;
  startTime: Date;
  endTime: Date;
  physician: IUserSessionModel;
  patient: IUserSessionModel;
  isArchived: boolean;
  isDeleted: boolean;
  currentPainScale: number;
  averagePainScaleLastMonth: number;
  highestPainScaleLastMonth: number;
}

export interface IGetSessionListRes {
  sessions: ISessionModel[];
}

export interface IUpdateArchiveAppointmentStatus {
  id: string;
}
