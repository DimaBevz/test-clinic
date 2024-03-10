import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";

import { Box, Button, Text } from "@chakra-ui/react";
import { ITestsRes, TestsType } from "../tests.interface";
import { enumNameMapper } from "@utils/emunMapper";

import "./index.scss";

interface TestsListCardProps {
  info: ITestsRes;
}

export const TestsListCard = ({ info }: TestsListCardProps) => {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const getTestTypeName = enumNameMapper(TestsType);

  return (
    <Box className="TestsListCard">
      <Box className="TestsListCard__header">
        <Box>{info.name}</Box>
        <Box>{getTestTypeName(info.type as number)}</Box>
      </Box>
      <Box className="TestsListCard__content">
        <Text>{info.description}</Text>
      </Box>
      <Box className="TestsListCard__actions">
        <Button onClick={() => navigate(`/tests/${info.id}`)} >
          {t("View")}
        </Button>
      </Box>
    </Box>
  );
};
