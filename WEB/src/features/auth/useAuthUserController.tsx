import { useAppSelector } from "@store/index.ts";
import { authSelectors } from "@features/auth";
import doctorsService from "@features/Doctor/doctors.service.ts";
import patientsService from "@features/Patient/patients.service.ts";
import { IAuthData } from "@interfaces/IAuth.ts";
import { DoctorsFullModel } from "@interfaces/doctor.ts";
import { PatientsFullModel } from "@interfaces/patient.ts";

interface IAuthUserController {
	user: PatientsFullModel | DoctorsFullModel | IAuthData;
	status: string;
}

export default function useAuthUserController(): IAuthUserController {
	const user = useAppSelector(authSelectors.getAuthUser) as IAuthData;
	const status = useAppSelector(authSelectors.getStatus);
	
	let userData: PatientsFullModel | DoctorsFullModel | IAuthData = user;
	
	if (user!.role === 0) {
		const {data} = patientsService.useGetPatientByIdQuery(user!.id as string);
		userData = {...user, ...data} as PatientsFullModel;
	} else if (user!.role === 1) {
		const {data} = doctorsService.useGetDoctorByIdQuery(user!.id as string);
		userData = {...user, ...data} as DoctorsFullModel;
	}
	
	return {
		user: userData,
		status
	};
}
