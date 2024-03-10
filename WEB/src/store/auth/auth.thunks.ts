import { createAsyncThunk } from '@reduxjs/toolkit';

import { ApiError } from '@interfaces/general';

import { isApiError } from '@utils/api';
import { ConfirmRegisterRequest, LoginReq, RegisterRequest } from "@store/auth/auth.interface";
import authService from '@api/auth.service';

export const setAuth = createAsyncThunk("auth/fetchAuth", async (data: /*IAuthData*/ any) => {
	try {
		return data
	} catch (error) { /* empty */ }
});

const getCurrentUser = createAsyncThunk(
	'auth/current-user',
	async (_, { rejectWithValue }) => {
		try {
			return await authService.getCurrentUser();
		} catch ( e ) {
			return rejectWithValue( e as ApiError );
		}
	}
);

const login = createAsyncThunk(
	'auth/login',
	async (data: LoginReq, {rejectWithValue}) => {
		const response = await authService.login(data);
		if (response.isSuccess) {
			localStorage.setItem( "clinicToken", JSON.stringify( ( response.data.idToken ) ).replace( /^"(.*)"$/, '$1' ) );
			return response
		}
		
		if (isApiError(response)) {
			return rejectWithValue(response);
		}
		
		return response.data;
	}
)
const uploadPhoto = createAsyncThunk(
	'auth/uploadPhoto',
	async (data: FormData, {rejectWithValue}) => {
		const response = await authService.uploadPhoto(data);
		if (isApiError(response)) {
			return rejectWithValue(response);
		}
		
		return response.data;
	}
)

const registerUser = createAsyncThunk(
	'auth/register',
	async (data: RegisterRequest, { rejectWithValue }) => {
		try {
			return await authService.registerUser( data );
		} catch ( e ) {
			return rejectWithValue( e );
		}
	}
);

const confirmRegister = createAsyncThunk(
	'auth/confirm-register',
	async ( data: ConfirmRegisterRequest, { rejectWithValue } ) => {
		try {
			return await authService.confirmRegister( data );
		} catch ( e ) {
			return rejectWithValue( e );
		}
	},
);

const authThunks = {
	setAuth,
	getCurrentUser,
	registerUser,
	login,
	confirmRegister,
	uploadPhoto
};

export default authThunks;
