import api from "@store/rtk.config";
import { ISpecialty } from "@interfaces/specialty.ts";
import { IResponse } from "@interfaces/general";

const ROOT_PATH = "Position";

const specialitiesService = api.injectEndpoints({
  endpoints: (builder) => ({
    getSpecialitiesList: builder.query<ISpecialty[], void>({
      query: () => ({
        url: `${ROOT_PATH}/GetPositions`,
        method: "GET",
      }),
      providesTags: ["specialities"],
      transformResponse: (response: IResponse) => response.data.positions,
    }),
  }),
});

export const { useGetSpecialitiesListQuery } = specialitiesService;
