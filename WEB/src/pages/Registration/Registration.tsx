import { useState } from "react";
import { Field, Formik } from "formik";
import { Link as ReactRouterLink, useNavigate } from 'react-router-dom';
import {
	Box,
	Button,
	Checkbox,
	FormControl,
	FormErrorMessage,
	FormLabel,
	Icon,
	Input,
	InputGroup,
	InputRightElement,
	Link as ChakraLink,
	Stack,
	Tab,
	TabList,
	Tabs,
	VStack,
} from "@chakra-ui/react";
import { ExternalLinkIcon, ViewIcon, ViewOffIcon } from '@chakra-ui/icons';
import { FaUser, FaUserDoctor } from "react-icons/fa6";
import "./index.scss";
import { userRegisterSchema } from "@pages/Registration/validationSchema.ts";
import { useTranslation } from "react-i18next";
import "react-international-phone/style.css";
import { checkIsPhoneValid } from "@utils/validation.ts";
import { authSelectors, authThunks } from "@features/auth";
import { useAppDispatch, useAppSelector } from "@store/index.ts";
import { removeEmptyFieldsFromRequestBody } from "@utils/index.ts";
import { PhoneInputField } from "@components/PhoneInputField/PhoneInputField.tsx";
import { useToast } from "@hooks/useToast.ts";
import LanguageSelector from "@components/LanguageSelector";

export const Registration = () => {
	const { t } = useTranslation();
	const [isAgreeChecked, setAgreeChecked] = useState(false);
	const [ showPassword, setShowPassword ] = useState( false );
	const dispatch = useAppDispatch();
	const navigate = useNavigate();
	const {errorToast} = useToast();
	const status = useAppSelector( authSelectors.getStatus );
	const [ userRole, setRole] = useState(0);
	
	
	const handlePasswordVisibility = () => setShowPassword( !showPassword );
	return (
		<>
			<Stack position="absolute" w="100%" alignItems="flex-end">
				<LanguageSelector />
			</Stack>
			<Stack direction="row" className="Main">
			<Box className="Main__registr" overflowY="auto">
				<Box bg="white" color={"black"} p={ 6 } rounded="md" w={ 80 }>
					<Formik
						initialValues={ {
							email: "",
							password: "",
							firstName: "",
							lastName: "",
							patronymic: "",
							phoneNumber: "",
							role: 0,
						} }
						validationSchema={ userRegisterSchema }
						onSubmit={ async ( values ) => {
							if ( checkIsPhoneValid( values.phoneNumber ) ) {
								const data = await dispatch( authThunks.registerUser( {
									...removeEmptyFieldsFromRequestBody( values ),
									role: userRole
								} ) );
								if ( data.payload.isSuccess ) {
									window.localStorage.setItem("email", values.email)
									navigate("/confirmation")
								} else {
									errorToast(t(data.payload.data.data.title))
								}
							}
						} }
					>
						{ ( { handleSubmit, errors, touched } ) => (
							<form onSubmit={ handleSubmit }>
								<VStack spacing={ 4 } align="flex-start">
									<Tabs w="100%" colorScheme='blue' onChange={(index) => setRole(index)}>
										<TabList borderColor="white" justifyContent="space-around">
											<Tab color="orange.600" gap={4}><Icon as={FaUser}/> { t("Patient") }</Tab>
											<Tab color="orange.600" gap={4}><Icon as={FaUserDoctor}/> { t("Physician") }</Tab>
										</TabList>
									</Tabs>
									<FormControl isInvalid={ !!errors.firstName && touched.firstName }>
										<FormLabel  htmlFor="firstName">{ t( "Name" ) }*</FormLabel>
										<Field
											as={ Input }
											_focusVisible={ { borderColor: "#DD6B20" } }
											id="firstName"
											name="firstName"
											variant="filled"
										/>
										<FormErrorMessage>{ errors.firstName }</FormErrorMessage>
									</FormControl>
									<FormControl isInvalid={ !!errors.lastName && touched.lastName }>
										<FormLabel htmlFor="lastName">{ t( "LastName" ) }*</FormLabel>
										<Field
											as={ Input }
											id="lastName"
											_focusVisible={ { borderColor: "#DD6B20" } }
											name="lastName"
											variant="filled"
										/>
										<FormErrorMessage>{ errors.lastName }</FormErrorMessage>
									</FormControl>
									<FormControl isInvalid={ !!errors.patronymic && touched.patronymic }>
										<FormLabel htmlFor="patronymic">{ t( "Patronymic" ) }</FormLabel>
										<Field
											as={ Input }
											id="patronymic"
											_focusVisible={ { borderColor: "#DD6B20" } }
											name="patronymic"
											variant="filled"
										/>
										<FormErrorMessage>{ errors.patronymic }</FormErrorMessage>
									</FormControl>
									<FormControl isInvalid={ !!errors.phoneNumber && touched.phoneNumber }>
										<FormLabel htmlFor="phoneNumber">{ t( "Phone" ) }*</FormLabel>
										<PhoneInputField label="Phone" name="phoneNumber" id="phoneNumber" className="Main__phone"/>
									</FormControl>
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
										<FormLabel htmlFor="password">{ t("Password") }*</FormLabel>
										<InputGroup>
											<Field
												as={ Input }
												id="password"
												_focusVisible={ { borderColor: "#DD6B20" } }
												name="password"
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
									<Checkbox
										isChecked={isAgreeChecked}
										onChange={(e) => setAgreeChecked(e.target.checked)}
									>
										{`${t("I agree to")} ${t("Terms Conditions")}`}
									</Checkbox>
									<Button
										isLoading={ status === "loading" }
										loadingText={t("Submitting")}
										type="submit"
										// _checked={}
										isDisabled={!isAgreeChecked || !!Object.keys(errors).length}
										colorScheme="orange"
										width="full"
									>
										{ t( "Register" ) }
									</Button>
									<ChakraLink display="flex" colorScheme="orange" alignItems="center" as={ ReactRouterLink } to="/login">
										{ t( "IsRegistered" ) } <ExternalLinkIcon mx="1.5px"/>
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
