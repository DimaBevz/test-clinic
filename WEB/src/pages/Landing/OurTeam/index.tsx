import { Box, Heading, Stack } from "@chakra-ui/react";
import { Header } from "@components/Header";
import { useTranslation } from "react-i18next";

import { Footer } from "../Footer";
import { ScrollToTopOnMount } from "@components/index.ts";

import pic1 from "../../../share/assets/landing/2311072235439.jpg";
import pic2 from "../../../share/assets/landing/2311071237328.jpg";
import pic3 from "../../../share/assets/landing/2311071239251.jpg";
import pic4 from "../../../share/assets/landing/2311071243687.jpg";
import pic5 from "../../../share/assets/landing/2311072238205.jpg";

import { Card } from "@components/Card";

export const OurTeam = () => {
  const { t } = useTranslation();
  return (
    <div className="main">
      <ScrollToTopOnMount/>
      <Header />
      <Stack alignItems="center" mt={20} mb={20} gap={10}>
        <Heading>{t("Our team of specialists")}</Heading>
        <Box display="flex" flexWrap="wrap" justifyContent="center" gap={30}>
          <Card
            src={pic1}
            alt="doctor"
            cardLabel="Дмитрієв Дмитро Валерійович"
            cardText="Професор, доктор наук, анестезіолог, альголог"
          />
          <Card
            src={pic2}
            alt="doctor"
            cardLabel="Ксенчина Катерина Володимирівна"
            cardText="Асистент кафедри внутрішньої та сімейної медицини Вінницького медичного університету"
          />
          <Card
            src={pic3}
            alt="doctor"
            cardLabel="Юлія Гук"
            cardText="Завідувачка реабілітаційного центру у Вінницькій обласній клінічний лікарні"
          />
          <Card
            src={pic4}
            alt="doctor"
            cardLabel="Сергій Коваленко"
            cardText="Кандидат наук, ортопед травматолог, експерт національної служби здоров’я України"
          />
          <Card
            src={pic5}
            alt="doctor"
            cardLabel="Наталія Лапшова"
            cardText="Психолог, кандидат психологічних наук, КПТ-консультант, робота з травмою, кризове консультуванняг"
          />
        </Box>
      </Stack>
      <Footer />
    </div>
  );
};
