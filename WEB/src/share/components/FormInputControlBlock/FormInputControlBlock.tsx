import { Field, FormikErrors, FormikTouched, FormikValues } from "formik";
import { useTranslation } from "react-i18next";
import { FormControl, FormErrorMessage, FormLabel, Input } from "@chakra-ui/react";

type FormInputControlBlockProps = {
	htmlFor: string,
	name: string,
	label: string,
	errors: FormikErrors<FormikValues>,
	touched: FormikTouched<FormikValues>,
}

const FormInputControlBlock: React.FC<FormInputControlBlockProps> = ({ htmlFor, name, label, errors, touched  }) => {
	const { t } = useTranslation();
	
	return (
		<FormControl isInvalid={Boolean(!!errors[name] && touched[name])}>
			<FormLabel htmlFor={htmlFor}>{t(label)}*</FormLabel>
			<Field
				as={Input}
				id={name}
				name={name}
				variant="filled"
			/>
			<FormErrorMessage>{errors[name] as string}</FormErrorMessage>
		</FormControl>
	);
};

export default FormInputControlBlock;
