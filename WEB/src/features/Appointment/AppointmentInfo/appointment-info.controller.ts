import { useNavigate } from "react-router-dom";
import { useAppointmentInfo } from "@hooks/appointment/useAppointmentInfo.ts";
import { useTestsResultList } from "@hooks/tests/useTestsResultList.ts";

interface IProps {
	id?: string;
}

function useAppointmentInfoController({ id }: IProps) {
	const navigate = useNavigate();
	const { sessionInfoData, isSessionInfoLoading } = useAppointmentInfo({
		appointmentId: id as string,
	});
	const { testsListResult, isTestsListResultLoading } = useTestsResultList({
		patientId: sessionInfoData?.patient.id,
	});
	
	return {
		navigate,
		session: {
			sessionInfoData,
			isSessionInfoLoading,
		},
		tests: {
			testsListResult,
			isTestsListResultLoading,
		},
	};
}

export default useAppointmentInfoController;
