/* eslint-disable @typescript-eslint/no-explicit-any */
type Callback = (this: unknown, ...args: any[]) => void;
type TimeoutId = ReturnType<typeof setTimeout>;

export default function debounce<C extends Callback>(cb: C, t = 1200): C {
	let id: TimeoutId;

	return function debouncedFn(this: unknown, ...args: Parameters<C>) {
		clearTimeout(id);

		id = setTimeout(() => {
			cb.apply(this, args);
		}, t);
	} as C;
}
