import { useCallback } from "react";
import { useLazyGetDoctorByIdQuery } from "@api/doctors.service";
import { IDoctorsPartialModel } from "@interfaces/doctor";

interface IProps {
  id?: string;
}

export const useGetDoctorById = ({
  id,
}: IProps): {
  doctorInfoData?: IDoctorsPartialModel;
  getDoctorById: (id: string) => void;
  isDoctorInfoLoading: boolean;
} => {
  const [getDoctorByIdQuery, { isLoading, data }] = useLazyGetDoctorByIdQuery();

  const getDoctorById = useCallback(
    (id: string) => {
      if (id) {
        getDoctorByIdQuery(id);
      }
    },
    [id, getDoctorByIdQuery]
  );

  return {
    doctorInfoData: data,
    getDoctorById,
    isDoctorInfoLoading: isLoading,
  };
};
