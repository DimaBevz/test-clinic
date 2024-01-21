import { useState } from "react";
import { Link as ReactRouterLink, useNavigate } from "react-router-dom";
import { Field, Formik } from "formik";
import {
	Box,
	Button,
	FormControl,
	FormErrorMessage,
	FormLabel,
	Input,
	InputGroup,
	InputRightElement,
	Link as ChakraLink,
	Stack,
	VStack,
} from "@chakra-ui/react";
import { ExternalLinkIcon, ViewIcon, ViewOffIcon } from '@chakra-ui/icons';
import "./index.scss";
import { userLoginSchema } from "@pages/Login/validationSchema.ts";
import { useTranslation } from "react-i18next";
import { authSelectors, authThunks } from "@features/auth";
import { useAppDispatch, useAppSelector } from "@store/index.ts";
import { useToast } from "@hooks/useToast.ts";
import LanguageSelector from "@components/LanguageSelector";

export const LoginForm = () => {
	const { t } = useTranslation();
	const dispatch = useAppDispatch();
	const { errorToast } = useToast();
	const navigate = useNavigate();
	const status = useAppSelector( authSelectors.getStatus );
	const [ showPassword, setShowPassword ] = useState( false );
	const handlePasswordVisibility = () => setShowPassword( !showPassword );
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
								email: "",
								password: "",
							} }
							validationSchema={ userLoginSchema }
							onSubmit={ async ( values ) => {
									const data = await dispatch( authThunks.login( values ) );
									
									if ( data.payload.isSuccess ) {
										await dispatch( authThunks.getCurrentUser() );
										navigate( "/" );
									} else {
										errorToast( t( data.payload.data.title ) );
									}
								
							} }
						>
							{ ( { handleSubmit, errors, touched } ) => (
								<form onSubmit={ handleSubmit }>
									<VStack spacing={ 4 } align="flex-start">
										<FormControl isInvalid={ !!errors.email && touched.email }>
											<FormLabel htmlFor="email">{ t( "Email" ) }*</FormLabel>
											<Field
												as={ Input }
												id="email"
												name="email"
												_focusVisible={ { borderColor: "#DD6B20" } }
												type="email"
												variant="filled"
											/>
											<FormErrorMessage>{ errors.email }</FormErrorMessage>
										</FormControl>
										<FormControl isInvalid={ !!errors.password && touched.password }>
											<FormLabel htmlFor="password">{ t( "Password" ) }</FormLabel>
											<InputGroup>
												<Field
													as={ Input }
													id="password"
													name="password"
													_focusVisible={ { borderColor: "#DD6B20" } }
													type={ showPassword ? 'text' : 'password' }
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
													<Button h="1.5rem" size="sm" onClick={ handlePasswordVisibility }>
														{ showPassword ? <ViewOffIcon/> : <ViewIcon/> }
													</Button>
												</InputRightElement>
											</InputGroup>
											<FormErrorMessage>{ errors.password }</FormErrorMessage>
										</FormControl>
										{/*<ChakraLink display="flex" alignItems="center" as={ReactRouterLink} to='/forgot-password'>*/ }
										{/*	{t("Forgot password")} <ExternalLinkIcon mx='1.5px' />*/ }
										{/*</ChakraLink>*/ }
										<Button
											isLoading={ status === "loading" }
											loadingText={ t( "Submitting" ) }
											type="submit"
											colorScheme="orange"
											width="full"
										>
											{ t( "Login" ) }
										</Button>
										<ChakraLink display="flex" alignItems="center" colorScheme="orange" as={ ReactRouterLink } to="/registration">
											{ t( "IsNotRegistered" ) } <ExternalLinkIcon mx="1.5px"/>
										</ChakraLink>
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
