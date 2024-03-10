export interface ITestsRes {
  id: string;
  name: string;
  description: string;
  type?: TestsType;
  totalScore?: number;
  verdict?: string;
}

export interface ITest {
  name: string;
  subtitle: string;
  questions: Question[];
}

export interface Question {
  id: string;
  text: string;
  options: Option[];
}

export interface Option {
  id: string;
  text: string;
}

export enum TestsType {
  MentalHealth,
}

export interface Answer {
  questionId: string;
  optionId: string;
}

export interface IProcessTestAnswersReq {
  testId: string;
  answers: Answer[];
}

export interface IProcessTestAnswersRes {
  totalScore: number;
  verdict: string;
}
