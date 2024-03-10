export function filterByField<T>(items: T[], field: keyof T): T[] {
	
	const uniqueItems: T[] = [];
	
	items.forEach(item => {
		if (!uniqueItems.some(unique => unique[field] === item[field])) {
			uniqueItems.push(item);
		}
	});
	
	return uniqueItems;
}
