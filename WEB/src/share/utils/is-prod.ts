export default function isProd() {
	return import.meta.env.MODE === 'prod';
}
