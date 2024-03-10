import { IDoctorsFullModel } from "@interfaces/doctor.ts";
import { IAuthData } from "@interfaces/IAuth.ts";

export enum DoctorsPossibleOrderBy {
  "name" = "name",
}

export enum SortField {
  Experience = 0,
  Rating = 1,
}

export interface IGetDoctorsListRequest {
  page: number;
  limit: number;
  isAscending?: boolean;
  isApproved?: boolean;
  search?: string;
  sortField?: SortField;
}

export interface GetDoctorsListRes {
  count: number;
  data: Array<IAuthData>;
}

export interface CreateDoctorReq {
  body: {
    name: string;
    surname: string;
    patronymic?: string;
    email: string;
    phone: string;
  };
}

export interface UpdateDoctorReq {
  id: IAuthData["id"];
  body: Partial<IDoctorsFullModel>;
}
