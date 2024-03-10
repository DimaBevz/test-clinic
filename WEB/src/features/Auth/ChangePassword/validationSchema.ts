import * as yup from "yup"

export const passwordOption = /^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&_])[A-Za-z\d@$!%*_#?&]*$/;
export const passwordField = yup.string().matches(passwordOption, "Пароль занадто легкий").min(8, "Мінімум 8 символів").required("Введіть ваш пароль");

export const userChangePasswordSchema = yup.object().shape({
	password: passwordField,
	newPassword: passwordField,
})
