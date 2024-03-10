import { Field, Formik } from "formik";
import { Link as ReactRouterLink } from "react-router-dom";
import {
  Box,
  Button,
  Checkbox,
  FormControl,
  FormErrorMessage,
  FormLabel,
  HStack,
  Icon,
  Image,
  Input,
  InputGroup,
  InputRightElement,
  Link as ChakraLink,
  Select,
  Stack,
  Tab,
  TabList,
  Tabs,
  Text,
  VStack,
} from "@chakra-ui/react";
import { ExternalLinkIcon, ViewIcon, ViewOffIcon } from "@chakra-ui/icons";
import { FaUser, FaUserDoctor } from "react-icons/fa6";
import { useTranslation } from "react-i18next";
import { checkIsPhoneValid } from "@utils/validation.ts";
import { authThunks } from "@store/auth/index.ts";
import { PhoneInputField } from "@components/PhoneInputField/PhoneInputField.tsx";
import LanguageSelector from "@components/LanguageSelector";
import logo from "@assets/img/EN Prometei Logo CMYK.png";
import i18n from "../../../i18n.ts";
import logoUA from "@assets/img/UA Prometei Logo CMYK.png";

import "react-international-phone/style.css";
import "./index.scss";
import useRegistrationFormController from "./registration-form.controller.ts";
import { userRegisterExtendedSchema, userRegisterSchema } from "./validationSchema.ts";
import { FormInputControlBlock } from "@components/index.ts";

export const RegistrationForm = () => {
  const { t } = useTranslation();
  const {
    isAgreeChecked,
    setAgreeChecked,
    setMilitaryChecked,
    isMilitaryChecked,
    dispatch,
    navigate,
    errorToast,
    status,
    disabilityCategories,
    userRole,
    setRole,
    handlePasswordVisibility,
    initialValue,
    showPassword
  } = useRegistrationFormController();

  return (
    <>
      <HStack position="absolute" w="100%" justifyContent="space-between">
        <Image
          m={1}
          maxH="50px"
          _hover={{ cursor: "pointer" }}
          onClick={() => navigate("/")}
          src={i18n.language === "en" ? logo : logoUA}
        />
        <Text className="Header__title">{t("Slogan")}</Text>
        <LanguageSelector />
      </HStack>
      <Stack direction="row" className="Main">
        <Box className="Main__form" overflowY="auto">
          <Box bg="white" color={ "black" } p={ 6 } rounded="md" w={ 80 } minWidth="382px">
            <Formik
              initialValues={initialValue}
              validationSchema={ isMilitaryChecked ? userRegisterExtendedSchema : userRegisterSchema }
              enableReinitialize={ true }
              onSubmit={async (values) => {
                if (checkIsPhoneValid(values.phoneNumber)) {
                  const militaryData = isMilitaryChecked
                    ? {...initialValue.militaryData, ...values.militaryData}
                    : null;
                  const data = await dispatch(
                    authThunks.registerUser({
                      ...values,
                      militaryData,
                      role: userRole
                    })
                  );
                  if (data.payload.isSuccess) {
                    window.localStorage.setItem("email", values.email);
                    navigate("/confirmation");
                  } else {
                    errorToast(t(data.payload.data.data.title));
                  }
                }
              }}
            >
              { ( { handleSubmit, errors, touched, handleChange, setFieldValue, values } ) => (
                <form onSubmit={handleSubmit}>
                  <VStack spacing={4} align="flex-start">
                    <Tabs w="100%" onChange={ ( index ) => setRole( index ) }>
                      <TabList borderColor="white" justifyContent="space-around">
                        <Tab gap={ 4 }>
                          <Icon as={FaUser} /> {t("Patient")}
                        </Tab>
                        <Tab gap={ 4 }>
                          <Icon as={FaUserDoctor} /> {t("Physician")}
                        </Tab>
                      </TabList>
                    </Tabs>
                    <FormInputControlBlock htmlFor="firstName" name="firstName" label="Name" errors={errors} touched={touched}/>
                    <FormInputControlBlock htmlFor="lastName" name="lastName" label="LastName" errors={errors} touched={touched}/>
                    <FormInputControlBlock htmlFor="patronymic" name="patronymic" label="Patronymic" errors={errors} touched={touched}/>
                    <FormControl isInvalid={!!errors.phoneNumber && touched.phoneNumber}>
                      <FormLabel htmlFor="phoneNumber">{t("Phone")}*</FormLabel>
                      <PhoneInputField
                        label="Phone"
                        name="phoneNumber"
                        id="phoneNumber"
                        className="Main__phone"
                      />
                    </FormControl>
                    <FormInputControlBlock htmlFor="email" name="email" label="Email" errors={errors} touched={touched}/>
                    <FormControl isInvalid={!!errors.password && touched.password}>
                      <FormLabel htmlFor="password">{t("Password")}*</FormLabel>
                      <InputGroup>
                        <Field
                          as={Input}
                          id="password"
                          name="password"
                          type={showPassword ? "text" : "password"}
                          variant="filled"
                          validate={(value: string) => {
                            let error;

                            if (value.length < 8) {
                              error = "Password must contain at least 8 characters";
                            }

                            return error;
                          }}
                        />
                        <InputRightElement width="3rem">
                          <Button
                            h="1.5rem"
                            size="sm"
                            variant="ghost"
                            className="Main__show-icon"
                            onClick={handlePasswordVisibility}
                          >
                            {showPassword ? <ViewOffIcon /> : <ViewIcon />}
                          </Button>
                        </InputRightElement>
                      </InputGroup>
                      <FormErrorMessage>{errors.password}</FormErrorMessage>
                    </FormControl>
                    { !userRole && <Checkbox
                      isChecked={ isMilitaryChecked }
                      onChange={ ( e ) => {
                        setFieldValue( "militaryData", !isMilitaryChecked ? {} : initialValue.militaryData );
                        setMilitaryChecked( e.target.checked );
                      } }
                    >
                      { t( "AreYouMilitary" ) }
                    </Checkbox> }
                    
                    { isMilitaryChecked && <>
                      <FormControl className="Main__checkbox">
                        <Field
                          as={ Checkbox }
                          id="militaryData.isVeteran"
                          name="militaryData.isVeteran"
                        />
                        <FormLabel htmlFor="militaryData.isVeteran">
                          {t("AreYouAVeteran")}
                        </FormLabel>
                      </FormControl>
                      <FormInputControlBlock htmlFor="militaryData.specialty" name="militaryData.specialty" label="WhatIsYourMilitarySpecialty" errors={errors} touched={touched}/>
                      <FormInputControlBlock htmlFor="militaryData.servicePlace" name="militaryData.servicePlace" label="WhereDidYouServeORServe" errors={errors} touched={touched}/>
                      <FormControl className="Main__checkbox">
                        <Field
                          as={ Checkbox }
                          id="militaryData.isOnVacation"
                          name="militaryData.isOnVacation"
                        />
                        <FormLabel htmlFor="militaryData.isOnVacation">
                          {t("AreYouOnTreatmentOrVacation")}
                        </FormLabel>
                      </FormControl>
                      <FormControl className="Main__checkbox">
                        <Field
                          as={ Checkbox }
                          id="militaryData.hasDisability"
                          name="militaryData.hasDisability"
                        />
                        <FormLabel htmlFor="militaryData.hasDisability">
                          {t("DoYouHaveADisability")}
                        </FormLabel>
                      </FormControl>
                      {
                        values.militaryData.hasDisability && <>
                          <FormControl>
                            <FormLabel htmlFor="militaryData.disabilityCategory">
                              Категорія інвалідності
                            </FormLabel>
                            <Select
                              as="select"
                              id="militaryData.disabilityCategory"
                              name="militaryData.disabilityCategory"
                              variant="filled"
                              onChange={ handleChange }
                              value={ values.militaryData.disabilityCategory }
                            >
                              { disabilityCategories.map( ( category ) => (
                                <option key={ category.value } value={ +category.value }>
                                  { category.name }
                                </option>
                              ) ) }
                            </Select>
                          </FormControl>
                        </>
                      }
                      <FormInputControlBlock
                        htmlFor="militaryData.healthProblems"
                        name="militaryData.healthProblems"
                        label="WhatInjuriesOrProblemsHaveYouExperiencedAsAResultOfYourMilitaryService"
                        errors={errors}
                        touched={touched}
                      />
                      <FormControl className="Main__checkbox">
                        <Field
                          as={ Checkbox }
                          id="militaryData.needMedicalOrPsychoCare"
                          name="militaryData.needMedicalOrPsychoCare"
                        />
                        <FormLabel htmlFor="militaryData.needMedicalOrPsychoCare">
                          {t("DoYouNeedMedicalOrPsychologicalHelp")}
                        </FormLabel>
                      </FormControl>
                      <FormControl className="Main__checkbox">
                        <Field
                          as={ Checkbox }
                          id="militaryData.hasDocuments"
                          name="militaryData.hasDocuments"
                        />
                        <FormLabel htmlFor="militaryData.hasDocuments">
                          {t("DoYouHaveDocumentsConfirmingYourMilitaryOrVeteranStatus")}
                        </FormLabel>
                      </FormControl>
                      { values.militaryData.hasDocuments &&
                        <FormInputControlBlock
                          htmlFor="militaryData.documentNumber"
                          name="militaryData.documentNumber"
                          label="DocumentNumber"
                          errors={errors}
                          touched={touched}
                        />
                      }
                      <FormInputControlBlock
                        htmlFor="militaryData.rehabilitationAndSupportNeeds"
                        name="militaryData.rehabilitationAndSupportNeeds"
                        label="WhatAreYourRehabilitationAndSupportNeeds"
                        errors={errors}
                        touched={touched}
                      />
                      <FormControl className="Main__checkbox">
                        <Field
                          as={ Checkbox }
                          id="militaryData.hasFamilyInNeed"
                          name="militaryData.hasFamilyInNeed"
                        />
                        <FormLabel htmlFor="militaryData.hasFamilyInNeed">
                          {t("DidYouHaveAFamilyMemberWhoMayAlsoNeedHelp")}
                        </FormLabel>
                      </FormControl>
                      <FormInputControlBlock
                        htmlFor="militaryData.howLearnedAboutRehabCenter"
                        name="militaryData.howLearnedAboutRehabCenter"
                        label="HowDidYouHearAboutUsWhatMotivatedYouToContactUs"
                        errors={errors}
                        touched={touched}
                      />
                      <FormControl className="Main__checkbox">
                        <Field
                          as={ Checkbox }
                          id="militaryData.wasRehabilitated"
                          name="militaryData.wasRehabilitated"
                        />
                        <FormLabel htmlFor="militaryData.wasRehabilitated">
                          {t("HaveYouAlreadyGoneThroughRehabilitation")}
                        </FormLabel>
                      </FormControl>
                      <FormInputControlBlock
                        htmlFor="militaryData.placeOfRehabilitation"
                        name="militaryData.placeOfRehabilitation"
                        label="WhereDidYouRehabilitateBefore"
                        errors={errors}
                        touched={touched}
                      />
                      <FormInputControlBlock
                        htmlFor="militaryData.resultOfRehabilitation"
                        name="militaryData.resultOfRehabilitation"
                        label="WhatWasTheResultOfTheRehabilitation"
                        errors={errors}
                        touched={touched}
                      />
                    </> }
                    
                    <Checkbox
                      isChecked={ isAgreeChecked }
                      onChange={ ( e ) => setAgreeChecked( e.target.checked ) }
                    >
                      {`${t("I agree to")} ${t("Terms Conditions")}`}
                    </Checkbox>
                    <Button
                      isLoading={status === "loading"}
                      loadingText={t("Submitting")}
                      type="submit"
                      isDisabled={
                        !isAgreeChecked || !!Object.keys(errors).length
                      }
                      width="full"
                    >
                      {t("Register")}
                    </Button>
                    <ChakraLink
                      display="flex"
                      alignItems="center"
                      as={ReactRouterLink}
                      to="/login"
                    >
                      {t("IsRegistered")}{" "}
                      <ExternalLinkIcon
                        _hover={{ color: "#ECC94B !important" }}
                        mx="1.5px"
                      />
                    </ChakraLink>
                  </VStack>
                </form>
              )}
            </Formik>
          </Box>
        </Box>
      </Stack>
    </>
  );
};
