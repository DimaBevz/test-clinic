export default function removeEmptyFieldsFromRequestBody<T>(obj: T): T {
	const copy = JSON.parse(JSON.stringify(obj));
	const keys = Object.keys(copy);

	keys.forEach((key) => {
		if (!copy[key]) {
			delete copy[key];
			return;
		}
		const keyType = typeof copy[key];

		if (keyType !== 'object') return;

		if (copy[key] instanceof Object && !('map' in copy[key])) {
			copy[key] = removeEmptyFieldsFromRequestBody(copy[key]);

			if (Object.keys(copy[key]).length === 0) {
				copy[key] = {};
				return;
			}

			return;
		}

		if (copy[key] instanceof Array) {
			if (copy[key].length === 0) {
				copy[key] = [];
				return;
			}

			const filtered = copy[key].filter((item: unknown) => item);
			copy[key] = filtered;

			filtered.forEach((item: unknown, idx: number) => {
				const itemType = typeof item;

				if (itemType !== 'object') return;

				if (item instanceof Object) {
					filtered[idx] = removeEmptyFieldsFromRequestBody(item);
				}
			});
		}
	});

	return copy;
}
