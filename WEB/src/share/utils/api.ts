import { SerializedError } from '@reduxjs/toolkit';
import { AxiosError } from 'axios';

import { ApiError } from '@interfaces/general';

export const createAPIError = (error: unknown): ApiError => {
	const response = (error as any)?.response;

	if (response) {
		return { data: response.data, status: response.status };
	}

	return { data: null, status: 500 };
};

export const isApiError = (
	error: ApiError | SerializedError | undefined | unknown
): error is ApiError => {
	if (!error) return false;
	if (error instanceof AxiosError && typeof error === 'object' && 'status' in error) return true;
	return false;
};

export const createFormData = <T>(body: T) => {
	const formData = new FormData();
	const entries = Object.entries(body as object);

	entries.forEach(([key, value]) => {
		if (value instanceof File) {
			formData.append(key, value);
		} else if (value instanceof Object || value instanceof Array) {
			formData.append(key, JSON.stringify(value));
		} else {
			formData.append(key, value);
		}
	});

	return formData;
};
