export default function parseFileUrl(url: string) {
	const regex = /https:\/\/[^/]+\/api\/v1\/file\/([^/]+)\/([^/]+)\/([^/]+)$/;
	const matches = url.match(regex);

	if (!matches || matches.length !== 4) {
		return null; // Invalid URL format
	}

	const [, directory, subdirectory, fileName] = matches;
	return {
		directory,
		subdirectory,
		fileName,
	};
}
