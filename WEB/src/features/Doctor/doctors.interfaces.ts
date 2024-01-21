import { DoctorsFullModel } from "@interfaces/doctor.ts";
import { IAuthData } from "@interfaces/IAuth.ts";

export enum DoctorsPossibleOrderBy {
	'name' = 'name',
}


export interface GetDoctorsListReq {
	orderBy?: DoctorsPossibleOrderBy;
	search?: string;
	minInfo?: boolean;
	page?: number;
	limit?: number;
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
	id: IAuthData['id'];
	body: Partial<DoctorsFullModel>;
}
