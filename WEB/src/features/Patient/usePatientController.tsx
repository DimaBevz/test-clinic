import { useAppSelector } from "@store/index.ts";
import { authSelectors } from "@features/auth";
import patientsService from "@features/Patient/patients.service.ts";
import { useParams } from "react-router-dom";

export default function usePatientController() {
	const user = useAppSelector( authSelectors.getAuthUser );
	const status = useAppSelector( authSelectors.getStatus );
	const { id } = useParams();
	const { isLoading, data} = patientsService.useGetPatientByIdQuery(id as string);
	console.log(isLoading, data);
	return {
		user,
		status
	}
}
