import { Store } from '@reduxjs/toolkit';
import axios from 'axios';

import API_CONFIG from '@configs/api';
import { authActions } from "@features/auth";

/* eslint-disable-next-line */
let store: Store<any, any>;

export const injectStoreToHttpClient = (st: Store) => {
	store = st;
};


const httpClient = axios.create({
	baseURL: API_CONFIG.BASE_URL,
	headers: API_CONFIG.HEADERS.Auth,
});

httpClient.interceptors.request.use(
	(config) => {
		const configCopy = { ...config };

		configCopy.headers.Accept = '*/*';
		configCopy.headers['Access-Control-Allow-Credentials'] = true;
		configCopy.headers['Access-Control-Allow-Origin'] = '*/*';
		configCopy.headers['Content-Type'] = configCopy.headers['Content-Type'] || 'application/json';

		if (store && 'getState' in store) {
				configCopy.headers.Authorization = `Bearer ${window.localStorage.getItem(
					"clinicToken"
				)}`;
			return configCopy;
		}

		return configCopy;
	},
	(error) => {
		throw new Error(error.response.data.ErrorMessage);
	}
);

httpClient.interceptors.response.use(
	(response) => response,
	(error) => {
		if ( error.code === "ERR_NETWORK" ) {
			window.location.replace("/login")
		}
		if ( error.response?.status === 401 ) {
			store.dispatch( authActions.logout() );
			window.location.replace("/login")
		} else {
			return Promise.reject(error);
		}
	}
);

export default httpClient;
