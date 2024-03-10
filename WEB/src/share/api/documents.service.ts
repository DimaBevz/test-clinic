import api from "@store/rtk.config";
import { Document } from "@interfaces/document.ts";
import { IResponse } from "@interfaces/general";

const ROOT_PATH = "Document";

const documentsService = api.injectEndpoints({
  endpoints: (builder) => ({
    getDocumentsById: builder.query<Document[], string>({
      query: (id) => ({
        url: `${ROOT_PATH}/GetDocuments`,
        params: { userId: id },
        method: "GET",
      }),
      providesTags: ["documents-list"],
      transformResponse: (response: IResponse) => response.data,
    }),
    uploadDocument: builder.mutation<Document, FormData>({
      query: (body) => ({
        url: `${ROOT_PATH}/UploadDocument`,
        params: { documentType: 0 },
        method: "POST",
        headers: {
          "Content-Type": "multipart/form-data",
        },
        body,
      }),
      invalidatesTags: ["documents-list"],
    }),
    deleteDocument: builder.mutation<void, string>({
      query: (id) => ({
        url: `${ROOT_PATH}/DeleteDocument`,
        params: { id },
        method: "DELETE",
      }),
      invalidatesTags: ["document"],
    }),
  }),
});

export default documentsService;
