import { Container } from "@components/Container";
import { Paper } from "@components/Paper";
import { Image, Text } from "@chakra-ui/react";
import { useTranslation } from "react-i18next";
import comingSoon from "@assets/img/coming-soon.png";

export const BugPage = () => {
  const { t } = useTranslation();
  return (
    <Container>
      <Paper
        h="300px"
        alignItems={"center"}
        display={"flex"}
        flexDirection="column"
        justifyContent={"center"}
      >
        <Image src={comingSoon} />
        <Text fontSize="16px">{t("Coming Soonv2")}...</Text>
      </Paper>
    </Container>
  );
};
