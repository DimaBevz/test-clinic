import { extendTheme } from "@chakra-ui/react";

export const theme = extendTheme({
	colors: {
		test: {
			500: "#FDCA38"
		}
	},
	styles: {
		global: {
			// styles for the `body`
			body: {
				bg: '#0000',
				color: '#fff',
			},
			// styles for the `a`
			a: {
				color: '#fff',
				_hover: {
					color: "#000",
					textDecoration: 'underline',
				},
			},
		},
	},
})
