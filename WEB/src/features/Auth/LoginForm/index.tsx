import { Link as ReactRouterLink } from "react-router-dom";
import { Field, Formik } from "formik";
import {
  Box,
  Button,
  FormControl,
  FormErrorMessage,
  FormLabel,
  HStack,
  Image,
  Input,
  InputGroup,
  InputRightElement,
  Link as ChakraLink,
  Stack,
  Text,
  VStack,
} from "@chakra-ui/react";
import { ExternalLinkIcon, ViewIcon, ViewOffIcon } from "@chakra-ui/icons";
import { useTranslation } from "react-i18next";
import { authThunks } from "@store/auth/index.ts";
import LanguageSelector from "@components/LanguageSelector";
import logo from "@assets/img/EN Prometei Logo CMYK.png";
import logoUA from "@assets/img/UA Prometei Logo CMYK.png";
import i18n from "../../../i18n.ts";

import { userLoginSchema } from "./validationSchema.ts";

import useLoginFormController from "./login-form.controller.ts";

const LoginForm = () => {
  const { t } = useTranslation();
  const {
    navigate,
    dispatch,
    errorToast,
    handlePasswordVisibility,
    showPassword,
    isLoading
  } = useLoginFormController();

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
        <Box className="Main__form">
          <Box bg="white" color={"black"} p={6} rounded="md" w={80}>
            <Formik
              initialValues={{
                email: "",
                password: "",
              }}
              validationSchema={userLoginSchema}
              onSubmit={async (values) => {
                const data = await dispatch(authThunks.login(values));

                if (data.payload.isSuccess) {
                  await dispatch(authThunks.getCurrentUser()).unwrap();
                } else {
                  errorToast(t(data.payload.data.title));
                }
              }}
            >
              {({ handleSubmit, errors, touched }) => (
                <form onSubmit={handleSubmit}>
                  <VStack spacing={4} align="flex-start">
                    <FormControl isInvalid={!!errors.email && touched.email}>
                      <FormLabel htmlFor="email">{t("Email")}*</FormLabel>
                      <Field
                        as={Input}
                        id="email"
                        name="email"
                        type="email"
                        variant="filled"
                      />
                      <FormErrorMessage>{errors.email}</FormErrorMessage>
                    </FormControl>
                    <FormControl
                      isInvalid={!!errors.password && touched.password}
                    >
                      <FormLabel htmlFor="password">{t("Password")}</FormLabel>
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
                              error =
                                "Password must contain at least 8 characters";
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
                    {/*<ChakraLink display="flex" alignItems="center" as={ReactRouterLink} to='/forgot-password'>*/}
                    {/*	{t("Forgot password")} <ExternalLinkIcon mx='1.5px' />*/}
                    {/*</ChakraLink>*/}
                    <Button
                      isLoading={isLoading}
                      loadingText={t("Submitting")}
                      type="submit"
                      width="full"
                    >
                      {t("Login")}
                    </Button>
                    <ChakraLink
                      display="flex"
                      alignItems="center"
                      colorScheme="yellow"
                      as={ReactRouterLink}
                      to="/registration"
                    >
                      {t("IsNotRegistered")} <ExternalLinkIcon mx="1.5px" />
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

export default LoginForm;
