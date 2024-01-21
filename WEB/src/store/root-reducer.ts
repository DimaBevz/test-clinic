import { combineReducers } from '@reduxjs/toolkit';
import persistReducer from 'redux-persist/es/persistReducer';

import { authReducer, authSlice } from '@features/auth';

import persistConfig from './persist.config';
import api from './rtk.config';

export type AuthReducer = ReturnType<typeof authReducer>;

const persistedAuthReducer = persistReducer<AuthReducer, any>(
	persistConfig,
	authReducer
);

const rootReducer = combineReducers({
	[api.reducerPath]: api.reducer,
	[authSlice.name]: persistedAuthReducer,
});

export default rootReducer;
