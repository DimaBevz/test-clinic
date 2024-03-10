export function arrayFromEnum<E extends Record<string, number | string>>( enumObj: E ) {
	return Object.keys( enumObj )
		.filter( key => !isNaN( parseInt( key ) ) )
		.map( key => {
			return {
				name: enumObj[key],
				value: parseInt( key ),
			};
		} );
}
