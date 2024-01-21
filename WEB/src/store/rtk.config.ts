import { Store } from "@reduxjs/toolkit";
import { BaseQueryFn, createApi, retry } from "@reduxjs/toolkit/query/react";
import { AxiosRequestConfig } from "axios";
import { Schema } from "yup";

import httpClient from "@http-client/index";

import API_CONFIG from "@configs/api";

import { ApiError } from "@interfaces/general";

import { createAPIError, isProd, validateServerResponse } from "@utils/index";

const generateUrl = (baseUrl: string, url: string) => {
  let formattedBaseUrl = "";
  let formattedUrl = "";

  if (baseUrl) {
    if (baseUrl.endsWith("/")) formattedBaseUrl = baseUrl;
    else formattedBaseUrl = `${baseUrl}/`;
  }

  if (url) {
    if (url.startsWith("/")) formattedUrl = url.substring(1);
    else formattedUrl = url;
  }

  return formattedBaseUrl + formattedUrl;
};

interface RTKArgs {
  baseURL: string;
  prepareHeaders?: (
    headers: NonNullable<AxiosRequestConfig["headers"]>,
    api: {
      dispatch: Store["dispatch"];
      getState: Store["getState"];
    }
  ) => AxiosRequestConfig["headers"];
}

const axiosBaseQuery =
  ({
    baseURL = "",
    prepareHeaders,
  }: RTKArgs): BaseQueryFn<
    {
      url: string;
      method: AxiosRequestConfig["method"];
      body?: AxiosRequestConfig["data"];
      baseUrl?: string;
      params?: AxiosRequestConfig["params"];
      headers?: AxiosRequestConfig["headers"];
      validationSchema?: Schema;
    },
    unknown,
    ApiError
  > =>
  async (
    {
      url,
      baseUrl = baseURL,
      method,
      body,
      params,
      headers = {},
      validationSchema,
    },
    { dispatch, getState }
  ) => {
    try {
      const result = await httpClient({
        url: generateUrl(baseUrl, url),
        method,
        data: body,
        params,
        headers: prepareHeaders
          ? prepareHeaders(headers, { dispatch, getState })
          : headers,
      });

      if (validationSchema && !isProd()) {
        validateServerResponse(validationSchema, result);
      }

      return { data: result.data };
    } catch (error: unknown) {
      const data = {
        error: createAPIError(error) as ApiError,
      };

      return data;
    }
  };

const baseAxiosQuery = axiosBaseQuery({
  baseURL: API_CONFIG.BASE_URL,
});

const baseQueryWithRetry = retry(baseAxiosQuery, { maxRetries: 2 });

const api = createApi({
  reducerPath: "Api",
  baseQuery: baseQueryWithRetry,
  tagTypes: [
    "patient",
    "doctor",
    "document",
    "doctors-list",
    "documents-list",
    "comments-list",
    "user-partial",
    "timetable",
    "available-timetable",
    "session",
    "call",
    "session-archive-status",
    "specialities",
  ],
  endpoints: () => ({}),
});

export default api;
