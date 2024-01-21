import api from "@store/rtk.config";
import {
  ICreateTimetableReq,
  IGetAvailableTimetableRes,
  IGetTimetableReq,
  IGetTimetableRes,
} from "./time-table-form.interface";

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
      transformResponse: (response: any) => response.data,
    }),
    updateTimetable: builder.mutation<IGetTimetableRes, ICreateTimetableReq>({
      query: (data) => ({
        url: `${ROOT_PATH}/UpdateTimetableTemplate`,
        method: "PUT",
        body: data,
      }),
      invalidatesTags: ["timetable"],
      transformResponse: (response: any) => response.data,
    }),
    getTimetable: builder.mutation<IGetTimetableRes, IGetTimetableReq>({
      query: (data) => ({
        url: `${ROOT_PATH}/GetTimetableTemplate`,
        method: "POST",
        body: data,
      }),
      invalidatesTags: ["timetable"],
      transformResponse: (response: any) => response.data,
    }),
    getAvailableTimetable: builder.query<IGetAvailableTimetableRes, string>({
      query: (id) => ({
        url: `${ROOT_PATH}/GetAvailableTimetable/${id}`,
        method: "GET",
      }),
      transformResponse: (response: any) => response.data,
    }),
    deleteTimetable: builder.mutation<boolean, IGetTimetableReq>({
      query: ({ physicianId }) => ({
        url: `${ROOT_PATH}/DeleteTimetableTemplate`,
        method: "DELETE",
        params: {physicianId},
      }),
      invalidatesTags: ["timetable"],
      transformResponse: (response: any) => response.data,
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
