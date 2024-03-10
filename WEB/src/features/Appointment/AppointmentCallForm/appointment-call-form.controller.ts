import { useNavigate, useParams } from "react-router-dom";
import { useToast } from "@hooks/general/useToast";
import { useTranslation } from "react-i18next";
import { IUpdateSession } from "../AppointmentList/appointment-list.interface";
import { PhysicianAppointmentFormContext } from "@components/context/PhysicianAppointmentForm";
import { useContext } from "react";
import { useUpdateSessionMutation } from "@api/session.service";
import useAuthUser from "@hooks/general/useAuthUser";

interface Props {
  onClose?: () => void;
}

function useAppointmentCallFormController({ onClose }: Props) {
  const navigate = useNavigate();
  const { t } = useTranslation();
  const { id } = useParams();
  const [updateSession, { isLoading: isUpdateSessionLoading }] =
    useUpdateSessionMutation();
  const { successToast, errorToast } = useToast();
  const { isDoctor, isPatient } = useAuthUser();
  const { value } = useContext(PhysicianAppointmentFormContext);

  const handleUpdateSession = async () => {
    try {
      const updatedSessionData: IUpdateSession = {
        sessionId: id as string,
        recommendations: value,
      };
      await updateSession(updatedSessionData).unwrap();
      onClose && onClose();
      successToast(t("RecommendationsSentSuccessfully"));
      navigate(`/appointments/${id}`);
    } catch (error: any) {
      errorToast(error.data.title);
    }
  };

  return {
    doctor: {
      isDoctor: isDoctor,
      handleUpdateSession,
      isUpdateSessionLoading
    },
    patient: {
      isPatient: isPatient,
    },
  };
}

export default useAppointmentCallFormController;
