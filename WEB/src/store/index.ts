import { configureStore } from '@reduxjs/toolkit';
import { TypedUseSelectorHook, useDispatch, useSelector } from 'react-redux';
import { FLUSH, PAUSE, PERSIST, PURGE, REGISTER, REHYDRATE, persistStore } from 'redux-persist';

import isProd from '@utils/is-prod';

import rootReducer from './root-reducer';
import rtkQueryMiddleware from './rtk-query.middleware';
import api from './rtk.config';

const middlewareConfig = {
	serializableCheck: {
		ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER],
	},
};

export const store = configureStore({
	reducer: rootReducer,
	middleware: (gdm) => gdm(middlewareConfig).concat([api.middleware, rtkQueryMiddleware]),
	devTools: !isProd(),
});

export const persistor = persistStore(store);
export type RootAppState = ReturnType<typeof store.getState>;

export type AppDispatch = typeof store.dispatch;
export const useAppDispatch = () => useDispatch<AppDispatch>();

export const useAppSelector: TypedUseSelectorHook<RootAppState> = useSelector;
