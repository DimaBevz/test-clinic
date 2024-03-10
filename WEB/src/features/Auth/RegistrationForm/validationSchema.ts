import * as yup from "yup";

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

export const userRegisterExtendedSchema = yup.object().shape( {
	firstName: nameField,
	lastName: surnameField,
	patronymic: middleNameField,
	email: emailField,
	password: passwordField,
	militaryData: yup.object().shape( {
		isVeteran: yup.boolean(),
		specialty: yup.string(),
		servicePlace: yup.string(),
		isOnVacation: yup.boolean(),
		hasDisability: yup.boolean(),
		disabilityCategory: yup.number(),
		healthProblems: yup.string(),
		needMedicalOrPsychoCare: yup.boolean(),
		hasDocuments: yup.boolean(),
		documentNumber: yup.string(),
		rehabilitationAndSupportNeeds: yup.string(),
		hasFamilyInNeed: yup.boolean(),
		howLearnedAboutRehabCenter: yup.string(),
		wasRehabilitated: yup.boolean(),
		placeOfRehabilitation: yup.string(),
		resultOfRehabilitation: yup.string(),
})
})
