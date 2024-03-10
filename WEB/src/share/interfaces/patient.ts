import { IAuthData } from "@interfaces/IAuth.ts";

export interface IPatientsPartialModel {
  settlement: string;
  street: string;
  house: string;
  apartment: any;
  institution: string;
  position: string;
}

export interface IPatientsFullModel extends IAuthData, IPatientsPartialModel {}
