import { useAppSelector } from "@store/index.ts";
import { authSelectors } from "@store/auth";
import { IAuthData } from "@interfaces/IAuth.ts";
import { IDoctorsFullModel } from "@interfaces/doctor.ts";
import { IPatientsFullModel } from "@interfaces/patient.ts";
import { useMemo } from "react";
import { useGetAuthDoctorData } from "@hooks/doctor/useGetAuthDoctorData";
import { useGetAuthPatientData } from "@hooks/patient/useGetAuthPatientData";

interface IAuthUserController {
  user: IPatientsFullModel | IDoctorsFullModel | IAuthData;
  status: string;
  isDoctor: boolean;
  isPatient: boolean;
}

export default function useAuthUser(): IAuthUserController {
  const user = useAppSelector(authSelectors.getAuthUser);
  const status = useAppSelector(authSelectors.getStatus);

  const { patientData } = useGetAuthPatientData({ user });
  const { doctorData } = useGetAuthDoctorData({ user });

  const isDoctor = useMemo(() => user?.role === 1, [user]);
  const isPatient = useMemo(() => user?.role === 0, [user]);

  const userData = useMemo(() => {
    if (isPatient && patientData) {
      return { ...user, ...patientData } as IPatientsFullModel;
    } else if (isDoctor && doctorData) {
      return { ...user, ...doctorData } as IDoctorsFullModel;
    }
    return user as IAuthData;
  }, [user, patientData, doctorData, isPatient, isDoctor]);

  return {
    user: userData,
    status,
    isDoctor,
    isPatient,
  };
}
