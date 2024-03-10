import { IAuthData } from "@interfaces/IAuth.ts";
import { ISpecialty } from "@interfaces/specialty.ts";

export interface IDoctorsPartialModel {
	experience: any
	rating: number
	bio: any
	positions: ISpecialty[]
}

export interface IDoctorListModel {
	id: string
	firstName: string
	lastName: string
	patronymic: string
	photoUrl: string | null
	experience: any
	rating: number
	positions: ISpecialty[]
	commentsCount: number
	bio: any
}

export interface IDoctorsFullModel extends IAuthData, IDoctorsPartialModel{}
