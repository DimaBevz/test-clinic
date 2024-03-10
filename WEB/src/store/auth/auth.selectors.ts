import { RootAppState } from '@store/index';

const authSelectors = {
	getStatus: (state: RootAppState) => state.auth?.status,
	getAuthUser: (state: RootAppState) => state.auth?.data,
};

export default authSelectors;
