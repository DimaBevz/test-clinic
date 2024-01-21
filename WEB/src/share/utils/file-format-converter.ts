export const formatFileSize = (fileSizeBytes: number): string => {
	if (fileSizeBytes >= 1e9) {
		return `${(fileSizeBytes / 1e9).toFixed(2)} GB`;
	}
	if (fileSizeBytes >= 1e6) {
		return `${(fileSizeBytes / 1e6).toFixed(2)} MB`;
	}
	if (fileSizeBytes >= 1e3) {
		return `${(fileSizeBytes / 1e3).toFixed(2)} KB`;
	}
	return `${fileSizeBytes} bytes`;
};

export default formatFileSize;
