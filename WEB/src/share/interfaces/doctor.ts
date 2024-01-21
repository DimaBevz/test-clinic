import { IAuthData } from "@interfaces/IAuth.ts";
import { Specialty } from "@interfaces/specialty.ts";

export interface DoctorsPartialModel {
	experience: any
	rating: number
	bio: any
	positions: Specialty[]
}

export interface IDoctorListModel {
	id: string
	firstName: string
	lastName: string
	patronymic: string
	photoUrl: any
	experience: any
	rating: number
	positions: Specialty[]
	commentsCount: number
	bio: any
}

export interface DoctorsFullModel extends IAuthData, DoctorsPartialModel{}
