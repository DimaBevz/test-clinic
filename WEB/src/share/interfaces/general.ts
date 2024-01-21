import React from "react";

export type SVGComponent = React.FC<React.SVGProps<SVGSVGElement>> & {
  title?: string;
};

export interface ApiError {
  data: {
    status: number;
    title: string;
  } | null;
  status: number;
}

export interface RejectedValue {
  rejectValue: ApiError;
  rejectValue2: ApiError;
}

export enum StateStatuses {
  idle = "idle",
  loading = "loading",
  success = "success",
  error = "error",
}

export type StateStatus = keyof typeof StateStatuses;

export interface ServerPagination {
  page: number;
  limit: number;
  count: number;
}

export interface StandardPaginatedServerResponse<T> {
  pagination: ServerPagination;
  data: Array<T>;
}

export type Keyof<T extends Record<string, unknown>> = keyof T;

export type WithNullableFields<
  T extends Record<string, unknown>,
  V extends Keyof<T>
> = {
  [Key in keyof T]: Key extends V ? T[Key] | null : T[Key];
};

type Identity<T> = { [P in keyof T]: T[P] };

export type ReplaceField<T, K extends keyof T, R> = Identity<
  Pick<T, Exclude<keyof T, K>>
> & { [P in K]: R };
export type AnyFunction<R = void> = (...args: unknown[]) => R;