import api from "@store/rtk.config";
import { IPatientsPartialModel } from "@interfaces/patient.ts";
import { IAuthData } from "@interfaces/IAuth.ts";
import { IResponse } from "@interfaces/general";

const ROOT_PATH = "Patient";

const patientsService = api.injectEndpoints({
  endpoints: (builder) => ({
    getPatientById: builder.query<IPatientsPartialModel, IAuthData["id"]>({
      query: (id) => ({
        url: `${ROOT_PATH}/GetPatientData/${id}`,
        method: "GET",
      }),
      providesTags: ["patient"],
      transformResponse: (response: IResponse) => response.data,
    }),
  }),
});

export const { useLazyGetPatientByIdQuery } = patientsService;
