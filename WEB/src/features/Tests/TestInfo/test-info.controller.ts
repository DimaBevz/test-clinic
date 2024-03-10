import { useState } from "react";
import { useParams } from "react-router-dom";
import { useForm, SubmitHandler } from "react-hook-form";

import { useTestInfo } from "@hooks/tests/useTestInfo";
import { Answer } from "../tests.interface";
import { useProcessTestAnswersMutation } from "@api/tests.service.ts";
import { useToast } from "@hooks/general/useToast";
import { filterByField } from "@utils/filterByField.ts";

function useTestInfoController() {
  const { id } = useParams();
  const { testInfoData, isTestInfoLoading } = useTestInfo({
    testId: id as string,
  });
  const [
    submitTestAnswers,
    { isLoading: isSubmitTestAnswersLoading, data: submitTestAnswersData },
  ] = useProcessTestAnswersMutation();
  const [currentQuestionIndex, setCurrentQuestionIndex] = useState<number>(0);
  const [answers, setAnswers] = useState<Answer[]>([]);
  const { handleSubmit, register, watch, reset } = useForm<Answer>();
  const { errorToast } = useToast();

  const isAnswersLength = testInfoData?.questions.length === answers.length;

  const onSubmit: SubmitHandler<Answer> = async (data) => {
    const newAnswers = filterByField([...answers, data] as Answer[], "optionId" );
    setAnswers(newAnswers);

    if (
      testInfoData &&
      currentQuestionIndex < testInfoData.questions.length - 1
    ) {
      setCurrentQuestionIndex(currentQuestionIndex + 1);
      reset();
    } else {
      if (isAnswersLength) {
        const payload = {
          testId: id as string,
          answers: newAnswers.map((answer) => ({
            questionId:
              testInfoData && testInfoData.questions[currentQuestionIndex].id,
            optionId: answer.optionId,
          })),
        };

        try {
          await submitTestAnswers(payload).unwrap();
        } catch (error: any) {
          errorToast(error.data.title);
        }
      }
    }
  };

  const handleBack = () => {
    if (currentQuestionIndex > 0) {
      setAnswers(answers.slice(0, currentQuestionIndex-1));
      setCurrentQuestionIndex(currentQuestionIndex - 1);
      reset();    }
  };
  const isSubmitDisabled = !watch("optionId");
  
  const currentQuestion =
    testInfoData && testInfoData.questions[currentQuestionIndex];

  return {
    testInfoData,
    isTestInfoLoading,
    isSubmitDisabled,
    handleBack,
    currentQuestion,
    currentQuestionIndex,
    onSubmit,
    handleSubmit,
    register,
    isAnswersLength,
    submitData: {
      isLoading: isSubmitTestAnswersLoading,
      data: submitTestAnswersData,
    },
  };
}

export default useTestInfoController;
