import { useToast } from "@hooks/general/useToast";
import { authSelectors } from "@store/auth";
import { useAppDispatch, useAppSelector } from "@store/index";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { DisabilityCategory } from "@interfaces/IAuth.ts";
import { arrayFromEnum } from "@utils/arrayFromEnum.ts";

function useRegistrationFormController() {
  const [isAgreeChecked, setAgreeChecked] = useState<boolean>(false);
  const [ isMilitaryChecked, setMilitaryChecked ] = useState<boolean>( false );
  const [showPassword, setShowPassword] = useState<boolean>(false);
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const { errorToast } = useToast();
  const status = useAppSelector(authSelectors.getStatus);
  const [userRole, setRole] = useState<number>(0);
  
  const disabilityCategories = arrayFromEnum( DisabilityCategory );

  const initMilitaryData = {
    isVeteran: false,
    specialty: "",
    servicePlace: "",
    isOnVacation: false,
    hasDisability: false,
    disabilityCategory: 0,
    healthProblems: "",
    needMedicalOrPsychoCare: false,
    hasDocuments: false,
    documentNumber: "",
    rehabilitationAndSupportNeeds: "",
    hasFamilyInNeed: false,
    howLearnedAboutRehabCenter: "",
    wasRehabilitated: false,
    placeOfRehabilitation: "",
    resultOfRehabilitation: "",
  };
  
  const initialValue = {
    email: "",
    password: "",
    firstName: "",
    lastName: "",
    patronymic: "",
    phoneNumber: "",
    role: 0,
    militaryData: initMilitaryData,
  };

  const handlePasswordVisibility = () => setShowPassword(!showPassword);

  return {
    isAgreeChecked,
    setAgreeChecked,
    setMilitaryChecked,
    isMilitaryChecked,
    initMilitaryData,
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
  };
}

export default useRegistrationFormController;
