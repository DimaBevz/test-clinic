
import { createAPIError } from '@utils/api';
import { ConfirmRegisterRequest, LoginReq, RegisterRequest } from "@features/auth/auth.interface.ts";
import httpClient from "@http-client/index.ts";

const getCurrentUser = async () => {
	const url = '/User/GetCurrentUserData';

	try {
		const { data } = await httpClient.get(url);
		return data;
	} catch (error) {
		return createAPIError(error);
	}
};

const login = async (args: LoginReq) => {
	const url = '/User/SignIn';

	try {
		const { data } = await httpClient.post(url, args);
		return data;
	} catch (error) {
		return createAPIError(error);
	}
};

const uploadPhoto = async (args: FormData) => {
	const url = '/User/UploadUserPhoto';

	try {
		const { data } = await httpClient.put(url, args,  {
			headers: {
				'Content-Type': 'multipart/form-data'
			}
		});
		return data;
	} catch (error) {
		return createAPIError(error);
	}
};

const registerUser = async (args: RegisterRequest) => {
	const url = '/User/Register';
	try {
		const { data }  = await httpClient.post(url, args);
		return data;
	} catch (error) {
		return createAPIError(error);
	}
}

const confirmRegister = async (args: ConfirmRegisterRequest) => {
	const url = '/User/ConfirmRegistration';
	try {
		const { data } = await httpClient.post(url, args);
		return data;
	} catch (error) {
		return createAPIError(error);
	}
}

const authService = {
	getCurrentUser,
	registerUser,
	login,
	confirmRegister,
	uploadPhoto
};

export default authService;
