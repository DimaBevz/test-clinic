export interface LoginRes {

}

export interface LoginReq {
	email: string
	password: string
}

export interface RegisterRequest {
	email: string
	firstName: string
	lastName: string
	patronymic: string
	phoneNumber: string
	password: string
	role: number
	militaryData: IsMilitary | null
}

export interface ConfirmRegisterRequest {
	code: string
	email: string
}

export interface IsMilitary {
	isVeteran: boolean
	specialty: string
	servicePlace: string
	isOnVacation: boolean
	hasDisability: boolean
	disabilityCategory: number
	healthProblems: string
	needMedicalOrPsychoCare: boolean
	hasDocuments: boolean
	documentNumber: string
	rehabilitationAndSupportNeeds: string
	hasFamilyInNeed: boolean
	howLearnedAboutRehabCenter: string
	wasRehabilitated: boolean
	placeOfRehabilitation: string
	resultOfRehabilitation: string
}
