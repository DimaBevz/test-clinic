import { useNavigate } from "react-router-dom";
import { Formik } from "formik";
import { Box, Button, FormControl, FormLabel, Stack, VStack } from "@chakra-ui/react";
import "./index.scss";
import { useTranslation } from "react-i18next";
import { authThunks } from "@features/auth";
import { useAppDispatch } from "@store/index.ts";
import { useToast } from "@hooks/useToast.ts";
import { OTPInputField } from "@components/OTPInputField/OTPInputField.tsx";
import { useState } from "react";
import LanguageSelector from "@components/LanguageSelector";

export const ConfirmUser = () => {
	const { t } = useTranslation();
	const dispatch = useAppDispatch();
	const {successToast, errorToast} = useToast();
	const navigate = useNavigate();
	const [ loading, setIsLoading ] = useState( false );
	return (
		<>
			<Stack position="absolute" w="100%" alignItems="flex-end">
				<LanguageSelector />
			</Stack>
		<Stack direction="row" className="Main">
			<Box className="Main__login">
				<Box bg="white" color={"black"} p={ 6 } rounded="md" w={ 80 }>
					<Formik
						initialValues={ {
							otp: "",
						} }
						onSubmit={ async ( values ) => {
							setIsLoading( true );
							const data = await dispatch(authThunks.confirmRegister({email: window.localStorage.getItem("email") as string, code: values.otp}))
							if ( data.payload.isSuccess ) {
								successToast(t("Registration successfully"))
								window.localStorage.removeItem("email")
								navigate( "/login" );
							} else {
								errorToast(t(data.payload.data.data.title))
							}
							setIsLoading( false );
						} }
					>
						{ ( { handleSubmit, errors, touched } ) => (
							<form onSubmit={ handleSubmit }>
								<VStack spacing={ 4 } align="flex-start">
									<FormControl isInvalid={ !!errors.otp && touched.otp }>
										<FormLabel htmlFor="otp">{ t( "OTP" ) }*</FormLabel>
										<OTPInputField name="otp" id="otp" numberCount={6}/>
									</FormControl>
									
									<Button
										isLoading={ loading }
										loadingText={t("Submitting")}
										type="submit"
										colorScheme="orange"
										width="full"
									>
										{ t("Confirm") }
									</Button>
								</VStack>
							</form>
						) }
					</Formik>
				</Box>
			</Box>
		</Stack>
		</>
	);
};
