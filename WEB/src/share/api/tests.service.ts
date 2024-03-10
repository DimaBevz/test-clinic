import api from "@store/rtk.config";
import { IProcessTestAnswersReq, IProcessTestAnswersRes, ITest, ITestsRes } from "@features/Tests/tests.interface.ts";
import { IResponse } from "@interfaces/general";

const ROOT_PATH = "Test";

const testsService = api.injectEndpoints({
  endpoints: (builder) => ({
    processTestAnswers: builder.mutation<IProcessTestAnswersRes, IProcessTestAnswersReq>({
      query: (data) => ({
        url: `${ROOT_PATH}/ProcessTestAnswers`,
        method: "POST",
        body: data,
      }),
      invalidatesTags: ["session"],
      transformResponse: (response: IResponse) => response.data,
    }),
    getTests: builder.query<ITestsRes[], void>({
      query: () => ({
        url: `${ROOT_PATH}/GetTests`,
        method: "GET",
      }),
      providesTags: ["tests"],
      transformResponse: (response: any) => response.data,
    }),
    getTestsResultsList: builder.query<ITestsRes[], string>({
      query: (id) => ({
        url: `${ROOT_PATH}/GetAllTestResults/${id}`,
        method: "GET",
      }),
      providesTags: ["tests-result"],
      transformResponse: (response: any) => response.data,
    }),
    getTestById: builder.query<ITest, string>({
      query: (id) => ({
        url: `${ROOT_PATH}/GetTestQuestions/${id}`,
        method: "GET",
      }),
      providesTags: ["tests"],
      transformResponse: (response: IResponse) => response.data,
    }),
  }),
});

export const {
  useLazyGetTestsQuery,
  useLazyGetTestByIdQuery,
  useProcessTestAnswersMutation,
  useLazyGetTestsResultsListQuery
} = testsService;
