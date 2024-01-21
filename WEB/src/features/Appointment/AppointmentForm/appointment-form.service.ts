import api from "@store/rtk.config";
import {
  IAppointmentInfo,
  ICreateSessionReq,
  ICreateSessionRes,
} from "./appointment-form.interface";
import {
  IGetSessionListReq,
  IGetSessionListRes,
  IUpdateArchiveAppointmentStatus,
} from "../AppointmentList/appointment-list.interface";

const ROOT_PATH = "Session";

const timetableService = api.injectEndpoints({
  endpoints: (builder) => ({
    createSession: builder.mutation<ICreateSessionRes, ICreateSessionReq>({
      query: (data) => ({
        url: `${ROOT_PATH}/AddSession`,
        method: "POST",
        body: data,
      }),
      invalidatesTags: ["session"],
      transformResponse: (response: any) => response.data,
    }),
    updateArchiveStatus: builder.mutation<void, IUpdateArchiveAppointmentStatus >({
      query: (data) => ({
        url: `${ROOT_PATH}/UpdateArchiveStatus`,
        method: "PUT",
        params: {
          id: data.id
        },
      }),
      invalidatesTags: ["session"],
      transformResponse: (response: any) => response.data,
    }),
    getSessionList: builder.mutation<IGetSessionListRes, IGetSessionListReq>({
      query: (data) => ({
        url: `${ROOT_PATH}/GetSessions`,
        method: "POST",
        body: data,
      }),
      invalidatesTags: ["session"],
      transformResponse: (response: any) => response.data,
    }),
    getSessionInfo: builder.query<IAppointmentInfo, string>({
      query: (id) => ({
        url: `${ROOT_PATH}/GetSessionById/${id}`,
        method: "GET",
      }),
      providesTags: ["session"],
      transformResponse: (response: any) => response.data,
    }),
  }),
});

export const {
  useCreateSessionMutation,
  useUpdateArchiveStatusMutation,
  useGetSessionListMutation,
  useLazyGetSessionInfoQuery,
} = timetableService;
