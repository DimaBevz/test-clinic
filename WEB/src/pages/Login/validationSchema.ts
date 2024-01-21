import * as yup from "yup"

export const passwordField = yup.string().min(8, ).required();
export const emailField =  yup.string().email().required();


export const userLoginSchema = yup.object().shape({
	email: emailField,
	password: passwordField
})
