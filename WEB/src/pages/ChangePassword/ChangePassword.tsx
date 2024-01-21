import React from "react";
import { Field, Formik } from "formik";

import { userChangePasswordSchema } from "./validationSchema.ts";

import "./index.scss";
import {
	Box,
	Button,
	FormControl,
	FormErrorMessage,
	FormLabel,
	Heading,
	Input,
	InputGroup,
	InputRightElement,
	Stack,
	VStack,
} from "@chakra-ui/react";
import { useTranslation } from "react-i18next";
import { ViewIcon, ViewOffIcon } from "@chakra-ui/icons";
import LanguageSelector from "@components/LanguageSelector";

export const ChangePassword = () => {
	const { t } = useTranslation();
	const [ showPassword, setShowPassword ] = React.useState( {
		oldPassword: false,
		newPassword: false,
	} );
	
	const handleClickShowPassword = ( name: string ) => {
		switch ( name ) {
			case "oldPass":
				setShowPassword( {
					...showPassword,
					oldPassword: !showPassword.oldPassword,
				} );
				break;
			case "newPass":
				setShowPassword( {
					...showPassword,
					newPassword: !showPassword.newPassword,
				} );
				break;
			default:
				break;
		}
	};
	
	return (
		<>
			<Stack position="absolute" w="100%" alignItems="flex-end">
				<LanguageSelector />
			</Stack>
			<Stack direction="row" className="Main">
			<Box className="Main__change-password">
			<Box bg="white" p={ 6 } rounded="md" w={ 80 }>
				<Formik
					initialValues={ {
						password: "",
						newPassword: "",
					} }
					validationSchema={ userChangePasswordSchema }
					onSubmit={ ( values ) => {
						console.log(values);
					} }>
					{ ( { handleSubmit, errors, touched } ) => (
						<form onSubmit={ handleSubmit }>
							<VStack>
								<Heading size="md">{t("Password change form")}</Heading>
								<FormControl isInvalid={ !!errors.password && touched.password}>
									<FormLabel htmlFor="password">{ t( "Old password" ) }</FormLabel>
									<InputGroup>
										<Field
											as={ Input }
											id="password"
											name="password"
											type={ showPassword.oldPassword ? 'text' : 'password' }
											variant="filled"
											validate={ ( value: string | any[] ) => {
												let error;
												if ( value.length < 8 ) {
													error = "Password must contain at least 8 characters";
												}
												return error;
											} }
										/>
										
										<InputRightElement width="3rem">
											<Button h="1.5rem" size="sm" onClick={ ()=>handleClickShowPassword("oldPass") }>
												{ showPassword.oldPassword ? <ViewOffIcon/> : <ViewIcon/> }
											</Button>
										</InputRightElement>
									</InputGroup>
									<FormErrorMessage>{ errors.password }</FormErrorMessage>
								</FormControl>
								<FormControl isInvalid={ !!errors.newPassword && touched.newPassword}>
									<FormLabel htmlFor="newPassword">{ t( "Old password" ) }</FormLabel>
									<InputGroup>
										<Field
											as={ Input }
											id="newPassword"
											name="newPassword"
											type={ showPassword.newPassword ? 'text' : 'password' }
											variant="filled"
											validate={ ( value: string | any[] ) => {
												let error;
												if ( value.length < 8 ) {
													error = "Password must contain at least 8 characters";
												}
												return error;
											} }
										/>
										
										<InputRightElement width="3rem">
											<Button h="1.5rem" size="sm" onClick={ ()=>handleClickShowPassword("newPass") }>
												{ showPassword.newPassword ? <ViewOffIcon/> : <ViewIcon/> }
											</Button>
										</InputRightElement>
									</InputGroup>
									<FormErrorMessage>{ errors.newPassword }</FormErrorMessage>
								</FormControl>
								<Button
									// isLoading={ isLoading }
									loadingText="Submitting"
									type="submit"
									colorScheme="orange"
									width="full"
								>
									{t("Submit")}
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
