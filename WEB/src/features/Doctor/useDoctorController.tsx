import { useAppSelector } from "@store/index.ts";
import { authSelectors } from "@features/auth";
import { useParams } from "react-router-dom";
import doctorsService from "@features/Doctor/doctors.service.ts";

export default function useDoctorController() {
	const user = useAppSelector( authSelectors.getAuthUser );
	const status = useAppSelector( authSelectors.getStatus );
	const { id } = useParams();
	const { isLoading, data} = doctorsService.useGetDoctorByIdQuery(id as string);
	console.log(isLoading, data);
	return {
		user,
		status
	}
}
