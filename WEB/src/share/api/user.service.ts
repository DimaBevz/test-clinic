import api from "@store/rtk.config.ts";
import { IAuthData } from "@interfaces/IAuth.ts";
import { IResponse } from "@interfaces/general";

const ROOT_PATH = "User";

const userService = api.injectEndpoints({
  endpoints: (builder) => ({
    getPartialUserById: builder.query<IAuthData, string>({
      query: (id) => ({
        url: `${ROOT_PATH}/GetPartialUserData/${id}`,
        method: "GET",
      }),
      providesTags: ["user-partial"],
      transformResponse: (response: IResponse) => response.data,
    }),
  }),
});

export const { useLazyGetPartialUserByIdQuery } = userService;
