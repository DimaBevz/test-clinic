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
}

export interface ConfirmRegisterRequest {
	code: string
	email: string
}
