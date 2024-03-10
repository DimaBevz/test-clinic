import api from "@store/rtk.config";
import { IDoctorsPartialModel, IDoctorListModel } from "@interfaces/doctor.ts";
import { IAuthData } from "@interfaces/IAuth.ts";
import { IGetDoctorsListRequest } from "@features/Doctor/doctor.interfaces.ts";
import { IResponse } from "@interfaces/general";

const ROOT_PATH = "Physician";

const doctorsService = api.injectEndpoints({
  endpoints: (builder) => ({
    getDoctorById: builder.query<IDoctorsPartialModel, IAuthData["id"]>({
      query: (id) => ({
        url: `${ROOT_PATH}/GetPhysicianData/${id}`,
        method: "GET",
      }),
      providesTags: ["doctor"],
      transformResponse: (response: IResponse) => response.data,
    }),
    getAllDoctors: builder.query<IDoctorListModel[], void>({
      query: () => ({
        url: `${ROOT_PATH}/GetPhysicians`,
        method: "GET",
      }),
      providesTags: ["doctors-list"],
      transformResponse: (response: IResponse) => response.data.physicians,
    }),
    getDoctorsByParams: builder.query<
      IDoctorListModel[],
      IGetDoctorsListRequest
    >({
      query: (request: IGetDoctorsListRequest) => ({
        url: `${ROOT_PATH}/GetPhysiciansByParams`,
        method: "GET",
        params: { ...request },
      }),
      transformResponse: (response: IResponse) => response.data.physicians,
    }),
  }),
});

export const {
  useGetAllDoctorsQuery,
  useLazyGetDoctorsByParamsQuery,
  useLazyGetDoctorByIdQuery,
} = doctorsService;
