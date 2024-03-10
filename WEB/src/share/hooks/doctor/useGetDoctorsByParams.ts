import { useLazyGetDoctorsByParamsQuery } from "@api/doctors.service";
import { IGetDoctorsListRequest } from "@features/Doctor/doctor.interfaces";
import { IDoctorListModel } from "@interfaces/doctor";

export const useGetDoctorsByParams = (): {
  doctorsList?: IDoctorListModel[];
  getDoctorsByParams: (params: IGetDoctorsListRequest) => void;
  isGetDoctorsListLoading: boolean;
} => {
  const [
    getDoctorsByParams,
    { isLoading: isGetDoctorsListLoading, data: doctorsList },
  ] = useLazyGetDoctorsByParamsQuery();

  return {
    getDoctorsByParams,
    doctorsList,
    isGetDoctorsListLoading,
  };
};
