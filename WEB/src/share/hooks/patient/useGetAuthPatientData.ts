import { useEffect, useState } from "react";
import { IAuthData } from "@interfaces/IAuth";
import { useLazyGetPatientByIdQuery } from "@api/patients.service";
import { IPatientsPartialModel } from "@interfaces/patient";

interface IProps {
  user: IAuthData | null;
}

export const useGetAuthPatientData = ({ user }: IProps) => {
  const [patientData, setPatientData] = useState<IPatientsPartialModel>();
  const [getPatientById, { data, isLoading }] = useLazyGetPatientByIdQuery();

  useEffect(() => {
    if (user?.role === 0) {
      getPatientById(user.id)
        .then(() => {
          if (!isLoading) {
            setPatientData(data);
          }
        })
        .catch((error) => console.error(error));
    }
  }, [user, getPatientById, data, isLoading]);

  return { patientData };
};
