import { IResponse } from "@interfaces/general";
import api from "@store/rtk.config";
const ROOT_PATH = "Call";

const callService = api.injectEndpoints({
  endpoints: (builder) => ({
    getCallToken: builder.mutation<string, string>({
      query: (id: string) => ({
        url: `${ROOT_PATH}/GetSessionCallToken/${id}`,
        method: "GET",
      }),
      invalidatesTags: ["call"],
      transformResponse: (response: IResponse) => response.data,
    })
  }),
});

export const {
  useGetCallTokenMutation
} = callService;
