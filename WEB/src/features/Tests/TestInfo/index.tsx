import { useTranslation } from "react-i18next";

import { Container } from "@components/Container";
import { Paper } from "@components/Paper";
import { Preloader } from "@components/preloader/Preloader";

import { Box, Button, Radio, RadioGroup, Stack, Text } from "@chakra-ui/react";

import useTestInfoController from "./test-info.controller";

import "./index.scss";

export const TestInfo = () => {
  const { t } = useTranslation();
  const {
    testInfoData,
    isTestInfoLoading,
    isSubmitDisabled,
    currentQuestion,
    handleBack,
    register,
    currentQuestionIndex,
    handleSubmit,
    onSubmit,
    isAnswersLength,
    submitData: {
      isLoading: isSubmitTestAnswersLoading,
      data: submitTestAnswersData,
    },
  } = useTestInfoController();

  if (isTestInfoLoading || isSubmitTestAnswersLoading) {
    return <Preloader size="xl" />;
  }

  return (
    <Container>
      <Paper>
        {submitTestAnswersData ? (
          <Box className="TestInfo__verdict">
            <Text className="TestInfo__verdict-score">
              <Text>{t("YourScore")}:</Text>
              <Text>{submitTestAnswersData.totalScore}</Text>
            </Text>
            <Text className="TestInfo__verdict-text">
              {submitTestAnswersData.verdict}
            </Text>
          </Box>
        ) : (
          <form className="TestInfo" onSubmit={handleSubmit(onSubmit)}>
            <Text className="TestInfo__name">{testInfoData?.name}</Text>
            <Box className="TestInfo__content">
              <Box className="TestInfo__counter">
                <Text className="TestInfo__current-question">
                  {currentQuestionIndex + 1}
                </Text>
                <Text>/</Text>
                <Text>{testInfoData && testInfoData.questions.length}</Text>
              </Box>
              <Box className="TestInfo__test">
                <h4>{currentQuestion?.text}</h4>
                <RadioGroup>
                  <Stack>
                    {currentQuestion?.options.map((option) => (
                      <Radio value={option.id} {...register("optionId")}>{option.text}</Radio>
                    ))}
                  </Stack>
                </RadioGroup>
              </Box>
              <Box className="TestInfo__actions">
                {currentQuestionIndex > 0 && (
                  <Button variant="outline" onClick={handleBack}>
                    {t("Back")}
                  </Button>
                )}
                <Button isDisabled={isSubmitDisabled} type="submit">
                  {t(`${isAnswersLength ? "Submit" : "Next"}`)}
                </Button>
              </Box>
            </Box>
          </form>
        )}
      </Paper>
    </Container>
  );
};
