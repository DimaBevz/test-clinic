import { AddComment } from "@components/CommentBlock/AddComment/AddComment";
import useAppointmentCallFormController from "./appointment-call-form.controller";
import { ModalWindow } from "@components/ModalWindow";
import { AppointmentRecomendations } from "../AppointmentCall";
import { useContext } from "react";
import { PhysicianAppointmentFormContext } from "@components/context/PhysicianAppointmentForm";
import { useTranslation } from "react-i18next";
import { useNavigate, useParams } from "react-router-dom";
import { useToast } from "@hooks/general/useToast";

interface AppointmentCallFormProps {
  isOpen: boolean;
  onClose: () => void;
  doctorId: string;
}
export const AppointmentCallForm = ({
  isOpen,
  onClose,
  doctorId,
}: AppointmentCallFormProps) => {
  const navigate = useNavigate();
  const { t } = useTranslation();
  const { id } = useParams();
  const { successToast } = useToast();
  const { patient, doctor } = useAppointmentCallFormController({onClose});
  const { value } = useContext(PhysicianAppointmentFormContext);
  const { isPatient } = patient;
  const { isDoctor, handleUpdateSession, isUpdateSessionLoading } = doctor;

  const handleCloseRatingForm = () => {
    navigate(`/appointments/${id}`);
    successToast(t("RatingSentSuccessfully"))
    onClose();
  }

  return (
    <>
      <ModalWindow
        isOpen={isOpen}
        onClose={onClose}
        title={
          isDoctor && value
            ? t("CompleteYourRecommendations")
            : t("WriteRecommendations")
        }
        content={
          <>
            {isPatient && <AddComment doctorId={doctorId} onClose={handleCloseRatingForm} />}
            {isDoctor && <AppointmentRecomendations />}
          </>
        }
        onSubmit={handleUpdateSession}
        isActions={isDoctor}
        onCloseText={t("Cancel")}
        onSubmitText={t("Send")}
        isLoading={isUpdateSessionLoading}
        isDisabled={!value}
        isHideCancelButton
      />
    </>
  );
};
