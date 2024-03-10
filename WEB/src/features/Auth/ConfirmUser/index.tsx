import { Formik } from "formik";
import { useTranslation } from "react-i18next";

import {
  Box,
  Button,
  FormControl,
  FormLabel,
  HStack,
  Image,
  Stack,
  Text,
  VStack,
} from "@chakra-ui/react";
import { authThunks } from "@store/auth";
import { OTPInputField } from "@components/OTPInputField/OTPInputField.tsx";
import LanguageSelector from "@components/LanguageSelector";
import logo from "@assets/img/EN Prometei Logo CMYK.png";
import logoUA from "@assets/img/UA Prometei Logo CMYK.png";
import i18n from "../../../i18n.ts";

import useConfirmUserController from "./confirm-user.controller.ts";


export const ConfirmUser = () => {
  const { t } = useTranslation();
  const {
    dispatch,
    successToast,
    errorToast,
    navigate,
    loading,
    setIsLoading,
  } = useConfirmUserController();

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
                otp: "",
              }}
              onSubmit={async (values) => {
                setIsLoading(true);
                const data = await dispatch(
                  authThunks.confirmRegister({
                    email: window.localStorage.getItem("email") as string,
                    code: values.otp,
                  })
                );
                if (data.payload.isSuccess) {
                  successToast(t("Registration successfully"));
                  window.localStorage.removeItem("email");
                  navigate("/login");
                } else {
                  errorToast(t(data.payload.data.data.title));
                }
                setIsLoading(false);
              }}
            >
              {({ handleSubmit, errors, touched }) => (
                <form onSubmit={handleSubmit}>
                  <VStack spacing={4} align="flex-start">
                    <FormControl isInvalid={!!errors.otp && touched.otp}>
                      <FormLabel htmlFor="otp">{t("OTP")}*</FormLabel>
                      <OTPInputField name="otp" id="otp" numberCount={6} />
                    </FormControl>

                    <Button
                      isLoading={loading}
                      loadingText={t("Submitting")}
                      type="submit"
                      width="full"
                    >
                      {t("Confirm")}
                    </Button>
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
