import { Schema } from 'yup';
import { PhoneNumberUtil } from 'google-libphonenumber';

const errorStyles = `color: #bae6fd; background-color: #881337; padding: 4px 8px; border: 1px dashed #bae6fd; border-radius: 4px; font-family: 'Poppins', sans-serif; font-size: 14px;`;

export const validateServerResponse = async <T>(schema: Schema, data: T): Promise<void> => {
	try {
		await schema.validate(data);
	} catch (error) {
		/* eslint-disable-next-line */
		console.error(`%cNot valid data from server:`, errorStyles, error);
	}
};

export const checkIsPhoneValid = (phone: string) => {
	const phoneUtil = PhoneNumberUtil.getInstance();
	try {
		return phoneUtil.isValidNumber(phoneUtil.parseAndKeepRawInput(phone));
	} catch (error) {
		return false;
	}
}
