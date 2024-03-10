import api from "@store/rtk.config";
import {
  IAppointmentInfo,
  ICreateSessionReq,
  ICreateSessionRes,
} from "@features/Appointment/AppointmentForm/appointment-form.interface.ts";
import {
  IGetPaginatedSessionListReq,
  IGetPaginatedSessionListRes,
  IGetSessionListReq,
  IGetSessionListRes,
  IUpdateArchiveAppointmentStatus,
  IUpdateSession,
} from "@features/Appointment/AppointmentList/appointment-list.interface.ts";
import { IResponse } from "@interfaces/general";

const ROOT_PATH = "Session";

const sessionService = api.injectEndpoints({
  endpoints: (builder) => ({
    createSession: builder.mutation<ICreateSessionRes, ICreateSessionReq>({
      query: (data) => ({
        url: `${ROOT_PATH}/AddSession`,
        method: "POST",
        body: data,
      }),
      invalidatesTags: ["session"],
      transformResponse: (response: IResponse) => response.data,
    }),
    updateArchiveStatus: builder.mutation<
      void,
      IUpdateArchiveAppointmentStatus
    >({
      query: (data) => ({
        url: `${ROOT_PATH}/UpdateArchiveStatus`,
        method: "PUT",
        params: {
          id: data.id,
        },
      }),
      invalidatesTags: ["session"],
      transformResponse: (response: IResponse) => response.data,
    }),
    updateSession: builder.mutation<void, IUpdateSession>({
      query: (data) => ({
        url: `${ROOT_PATH}/UpdateSession`,
        method: "PUT",
        body: data,
      }),
      invalidatesTags: ["session"],
      transformResponse: (response: IResponse) => response.data,
    }),
    getSessionList: builder.mutation<IGetSessionListRes, IGetSessionListReq>({
      query: (data) => ({
        url: `${ROOT_PATH}/GetSessions`,
        method: "POST",
        body: data,
      }),
      invalidatesTags: ["session"],
      transformResponse: (response: IResponse) => response.data,
    }),
    getPaginatedSessionList: builder.mutation<IGetPaginatedSessionListRes, IGetPaginatedSessionListReq>({
      query: (data) => ({
        url: `${ROOT_PATH}/GetSessionsByParams`,
        method: "POST",
        body: data,
      }),
      invalidatesTags: ["session"],
      transformResponse: (response: any): IGetPaginatedSessionListRes => response.data,
    }),
    getSessionInfo: builder.query<IAppointmentInfo, string>({
      query: (id) => ({
        url: `${ROOT_PATH}/GetSessionById/${id}`,
        method: "GET",
      }),
      providesTags: ["session"],
      transformResponse: (response: IResponse) => response.data,
    }),
  }),
});

export const {
  useCreateSessionMutation,
  useUpdateArchiveStatusMutation,
  useUpdateSessionMutation,
  useGetSessionListMutation,
  useGetPaginatedSessionListMutation,
  useLazyGetSessionInfoQuery,
} = sessionService;
