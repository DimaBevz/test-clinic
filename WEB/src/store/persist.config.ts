import storage from 'redux-persist/lib/storage';

const persistConfig = {
	key: 'root',
	version: 1,
	storage,
	whitelist: ['session'],
};

export default persistConfig;
