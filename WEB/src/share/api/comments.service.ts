import api from "@store/rtk.config";
import { ICommentModel, ICreateCommentModel } from "@interfaces/comment.ts";
import { IResponse } from "@interfaces/general";

const ROOT_PATH = "Comment";

const commentsService = api.injectEndpoints({
  endpoints: (builder) => ({
    getCommentsById: builder.query<ICommentModel[], string>({
      query: (id) => ({
        url: `${ROOT_PATH}/GetComments`,
        params: { physicianId: id },
        method: "GET",
      }),
      providesTags: ["comments-list"],
      transformResponse: (response: IResponse) => response.data,
    }),
    createComment: builder.mutation<void, ICreateCommentModel>({
      query: (body) => ({
        url: `${ROOT_PATH}/CreateComment`,
        method: "POST",
        body,
      }),
    }),
  }),
});

export const {
	useCreateCommentMutation,
	useGetCommentsByIdQuery
} = commentsService;
