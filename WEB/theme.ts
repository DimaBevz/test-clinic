import { extendTheme } from "@chakra-ui/react";

export const theme = extendTheme({
	styles: {
		global: {
			a: {
				color: 'teal.500',
				_hover: {
					color: "#ECC94B",
					textDecoration: 'underline',
				},
				_active: {
					color: "#ECC94B",
					textDecoration: 'underline',
				},
				_focus: {
					color: "#ECC94B",
					textDecoration: 'underline',
				},
			},
			input: {
				_focusVisible: {
					borderColor: "#ECC94B !important",
				}
			},
			textarea: {
				_focusVisible: {
					borderColor: "#ECC94B !important",
				}
			}
		},
	},
	components: {
		Button: {
			variants: {
				"solid": {
					bg: "#ECC94B",
					color: "#000",
					_hover: {
						bg: "#D69E2E",
					},
				},
				outline: {
					bg: "transparent",
					color: "#B7791F",
					borderColor: "#B7791F",
					_hover: {
						bg: "#FFFFF0",
					},
				},
				ghost: {
					bg: "transparent",
					color: "#B7791F",
					_hover: {
						bg: "#FFFFF0",
					},
				},
			},
		},
		Switch: {
			baseStyle: {
				track: {
					_checked: {
						bg: "#B7791F",
					},
				},
			},
		},
		Radio: {
			baseStyle: {
				control: {
					_checked: {
						bg: "#B7791F",
						borderColor: "#B7791F",
						color: "#fff",
					},
				},
			},
		},
		Checkbox: {
			baseStyle: {
				control: {
					_checked: {
						bg: "#B7791F",
						borderColor: "#B7791F",
						_hover: {
							bg: "#B7791F",
							borderColor: "#B7791F",
						},
					},
				},
			},
		},
		Tabs: {
			variants: {
				"line": {
					tab: {
						color: "#B7791F",
						_selected: {
							color: "#B7791F",
						},
					},
				},
				
			}
		},
	},
})
