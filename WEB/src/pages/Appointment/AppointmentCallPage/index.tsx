import { PhysicianAppointmentFormProvider } from "@components/context/PhysicianAppointmentForm";
import { AppointmentCall } from "@features/Appointment/AppointmentCall";

export const AppointmentCallPage = () => {
  return (
    <PhysicianAppointmentFormProvider>
      <AppointmentCall />
    </PhysicianAppointmentFormProvider>
  );
};
