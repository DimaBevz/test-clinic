import { useField } from "formik";
import { FormControl, FormErrorMessage, HStack, PinInput, PinInputField } from "@chakra-ui/react";

export const OTPInputField = ( { numberCount, ...props }: any ) => {
	const [ field, meta, helpers ] = useField( "otp" );

	return (
		<FormControl isInvalid={ !meta.error && meta.touched }>
			<HStack justifyContent="space-evenly">
				<PinInput
					{ ...props }
					{ ...field }
					value={ field.value }
					onChange={ ( value ) => {
						helpers.setValue( value );
					} }
					type="alphanumeric"
				>
					{Array.from({ length: numberCount }).map((_, index) => (
						<PinInputField key={index} />
					))}
				</PinInput>
			</HStack>
			{ ( field.value.length < numberCount && meta.touched ) && <FormErrorMessage>Code should have {numberCount} numbers</FormErrorMessage> }
		</FormControl>
	
	);
};
