const BASE_URL = import.meta.env.VITE_APP_API_URL as string;

const HEADERS = {
	Auth: {
		'Content-Type': 'application/json',
		accept: 'application/x-www-form-urlencoded',
	},
};

const API_CONFIG = {
	BASE_URL,
	HEADERS,
};

export default API_CONFIG;
