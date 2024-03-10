import {
  Box,
  Heading,
  ListItem,
  Stack,
  Text,
  UnorderedList,
} from "@chakra-ui/react";
import { Header } from "@components/Header";
import { useTranslation } from "react-i18next";

import { Footer } from "../Footer";
import { ScrollToTopOnMount } from "@components/index";

import "./index.scss";

export const AboutUs = () => {
  const { t } = useTranslation();
  return (
    <div className="main">
      <Header />
      <ScrollToTopOnMount/>
      <Stack alignItems="center" mt={20} mb={20} gap={10}>
        <Heading textAlign="center" w="90%">{t("Welcome to the Rehabilitation Center")}</Heading>
        <Stack alignItems="center" w="90%" gap={10}>
          <Box className="Landing__MainInfo">
            <Text>{t("Landing1")}</Text>
            <Text>{t("Landing2")}</Text>
            <Text>{t("Landing3")}</Text>
            <UnorderedList>
              <Text fontWeight="bold" fontSize="20px">
                {t("PsychologicalComponent")}
              </Text>
              <ListItem fontSize="20px">{t("Mindfulness-oriented")}</ListItem>
              <ListItem fontSize="20px">{t("TherapeuticApproach")}</ListItem>
            </UnorderedList>
            <UnorderedList>
              <Text fontWeight="bold" fontSize="20px">
                {t("RehabilitationComponent")}
              </Text>
              <ListItem fontSize="20px">{t("RecordSystem")}</ListItem>
              <ListItem fontSize="20px">{t("VR")}</ListItem>
              <ListItem fontSize="20px">{t("Myo-")}</ListItem>
            </UnorderedList>
            <UnorderedList>
              <Text fontWeight="bold" fontSize="20px">
                {t("MedicinalComponent")}
              </Text>
              <ListItem fontSize="20px">{t("SpecializedDiagnostics")}</ListItem>
              <ListItem fontSize="20px">{t("AlgologicalDoctors")}</ListItem>
              <ListItem fontSize="20px">
                {t("SelectionOfPharmacotherapy")}
              </ListItem>
            </UnorderedList>
          </Box>
        </Stack>
      </Stack>
      <Footer/>
    </div>
  );
};
