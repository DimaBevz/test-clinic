import authThunks from '@store/auth/auth.thunks';

import { StateStatuses } from '@interfaces/general';
import { AuthStateProps, IAuthData } from "@interfaces/IAuth.ts";
import { createSlice } from "@reduxjs/toolkit";

const initialState: AuthStateProps = {
	data: null,
	status: "",
};

export const authSlice = createSlice({
	name: 'auth',
	initialState,
	reducers: {
		logout: (state) => {
			state.data = null;
			state.status = "";
			window.localStorage.removeItem( "clinicToken" );
		},
	},
	extraReducers: (builder) => {
		/* Login */
		builder.addCase(authThunks.getCurrentUser.pending, (state) => ({
			...state,
			status: StateStatuses.loading,
		}));
		builder.addCase(authThunks.getCurrentUser.fulfilled, ( _, { payload }) => ({
				data: payload.data,
				status: StateStatuses.success,
			}));
		builder.addCase(authThunks.getCurrentUser.rejected, (state) => ({
			...state,
			status: StateStatuses.error,
		}));
		
		builder.addCase(authThunks.registerUser.pending, (state) => ({
			...state,
			status: StateStatuses.loading,
		}));
		builder.addCase(authThunks.registerUser.fulfilled, (state) => ({
			...state,
			status: StateStatuses.success,
		}));
		builder.addCase(authThunks.registerUser.rejected, (state) => ({
			...state,
			status: StateStatuses.error,
		}));
		
		builder.addCase( authThunks.login.pending, ( state ) => ( {
			...state,
			status: StateStatuses.loading,
		} ) );
		builder.addCase( authThunks.login.fulfilled, ( state ) => ( {
			...state,
			status: StateStatuses.success,
		} ) );
		builder.addCase( authThunks.login.rejected, ( state ) => ( {
			...state,
			status: StateStatuses.error,
		} ) );
		
		
		builder.addCase( authThunks.uploadPhoto.fulfilled, ( state, { payload } ) => ( {
				data: {
					...state.data,
					photoUrl: payload.presignedUrl,
				} as IAuthData,
				status: StateStatuses.success,
			}
		) );
		
		builder.addCase(authThunks.setAuth.fulfilled, (_, { payload }) => ({
			status: "",
			data: payload,
		}));
	},
});

export const authReducer = authSlice.reducer;
export const authActions = authSlice.actions;
