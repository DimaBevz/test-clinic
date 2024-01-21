import * as yup from "yup"

export const passwordField = yup.string().min(8, ).required();
export const nameField = yup.string().required();
export const surnameField = yup.string().required();
export const middleNameField = yup.string();
export const emailField =  yup.string().email().required();

export const userRegisterSchema = yup.object().shape({
	firstName: nameField,
	lastName: surnameField,
	patronymic: middleNameField,
	email: emailField,
	password: passwordField
})
