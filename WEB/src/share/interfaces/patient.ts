import { IAuthData } from "@interfaces/IAuth.ts";

export interface PatientsPartialModel {
	settlement: string
	street: string
	house: string
	apartment: any
	institution: string
	position: string
}

export interface PatientsFullModel extends IAuthData, PatientsPartialModel{}
