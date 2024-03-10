import { useEffect, useState } from "react";
import { useLazyGetDoctorByIdQuery } from "@api/doctors.service";
import { IAuthData } from "@interfaces/IAuth";
import { IDoctorsPartialModel } from "@interfaces/doctor";

interface IProps {
  user: IAuthData | null;
}

export const useGetAuthDoctorData = ({ user }: IProps) => {
  const [doctorData, setDoctorData] = useState<IDoctorsPartialModel>();
  const [getDoctorById, { data, isLoading }] = useLazyGetDoctorByIdQuery();

  useEffect(() => {
    if (user?.role === 1) {
      getDoctorById(user.id)
        .then(() => {
          if (!isLoading) {
            setDoctorData(data);
          }
        })
        .catch((error) => console.error(error));
    }
  }, [user, getDoctorById, data, isLoading]);

  return { doctorData };
};
