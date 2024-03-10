export interface IGetSessionListReq {
  physicianId?: string;
  patientId?: string;
  startTime?: Date;
  endTime?: Date;
  sortType?: number;
}

export interface IGetPaginatedSessionListReq {
  page: number;
  limit: number;
  physicianId?: string;
  patientId?: string;
  startTime?: Date;
  endTime?: Date;
  sortType?: number;
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

export interface IGetPaginatedSessionListRes {
  sessions: ISessionModel[];
  totalCount: number;
}

export interface IUpdateArchiveAppointmentStatus {
  id: string;
}

export interface IUpdateSession {
  sessionId: string;
  meetingId?: string;
  startTime?: Date;
  endTime?: Date;
  patientId?: string;
  complaints?: string;
  treatment?: string;
  recommendations?: string;
  diagnosisTitle?: string;
}
