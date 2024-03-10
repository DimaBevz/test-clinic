import api from "@store/rtk.config";
import {
  ICreateTimetableReq,
  IGetAvailableTimetableRes,
  IGetTimetableReq,
  IGetTimetableRes,
} from "@features/Doctor/TimeTable/TimeTableForm/time-table-form.interface.ts";
import { IResponse } from "@interfaces/general";

const ROOT_PATH = "Timetable";

const timetableService = api.injectEndpoints({
  endpoints: (builder) => ({
    createTimetable: builder.mutation<IGetTimetableRes, ICreateTimetableReq>({
      query: (data) => ({
        url: `${ROOT_PATH}/CreateTimetableTemplate`,
        method: "POST",
        body: data,
      }),
      invalidatesTags: ["timetable"],
      transformResponse: (response: IResponse) => response.data,
    }),
    updateTimetable: builder.mutation<IGetTimetableRes, ICreateTimetableReq>({
      query: (data) => ({
        url: `${ROOT_PATH}/UpdateTimetableTemplate`,
        method: "PUT",
        body: data,
      }),
      invalidatesTags: ["timetable"],
      transformResponse: (response: IResponse) => response.data,
    }),
    getTimetable: builder.mutation<IGetTimetableRes, IGetTimetableReq>({
      query: (data) => ({
        url: `${ROOT_PATH}/GetTimetableTemplate`,
        method: "POST",
        body: data,
      }),
      invalidatesTags: ["timetable"],
      transformResponse: (response: IResponse) => response.data,
    }),
    getAvailableTimetable: builder.query<IGetAvailableTimetableRes, string>({
      query: (id) => ({
        url: `${ROOT_PATH}/GetAvailableTimetable/${id}`,
        method: "GET",
      }),
      transformResponse: (response: IResponse) => response.data,
    }),
    deleteTimetable: builder.mutation<boolean, IGetTimetableReq>({
      query: ({ physicianId }) => ({
        url: `${ROOT_PATH}/DeleteTimetableTemplate`,
        method: "DELETE",
        params: {physicianId},
      }),
      invalidatesTags: ["timetable"],
      transformResponse: (response: IResponse) => response.data,
    }),
  }),
});

export const {
  useCreateTimetableMutation,
  useUpdateTimetableMutation,
  useGetTimetableMutation,
  useLazyGetAvailableTimetableQuery,
  useDeleteTimetableMutation,
} = timetableService;
