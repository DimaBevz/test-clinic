import { useField } from "formik";
import { checkIsPhoneValid } from "@utils/validation.ts";
import { FormControl, FormErrorMessage } from "@chakra-ui/react";
import { PhoneInput } from "react-international-phone";

export const PhoneInputField = ( { label, ...props }: any ) => {
	const [ field, meta, helpers ] = useField( "phoneNumber" );
	const isPhoneValid = checkIsPhoneValid( field.value );
	
	return (
		<FormControl isInvalid={ !isPhoneValid && meta.touched }>
			<PhoneInput
				{ ...props }
				{ ...field }
				value={ field.value }
				color="yellow"
				defaultCountry="ua"
				style={ {
					width: "100%",
				} }
				onChange={ ( value ) => {
					helpers.setValue( value );
				} }
			/>
			
			{ (!isPhoneValid && meta.touched) && <FormErrorMessage>phone is not valid</FormErrorMessage> }
		</FormControl>
	
	);
};
