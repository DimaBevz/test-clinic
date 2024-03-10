import { useToast } from "@hooks/general/useToast";
import { IAuthData } from "@interfaces/IAuth";
import { authSelectors } from "@store/auth";
import { useAppDispatch, useAppSelector } from "@store/index";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

function useLoginFormController() {
  const dispatch = useAppDispatch();
  const { errorToast } = useToast();
  const navigate = useNavigate();
  const status = useAppSelector(authSelectors.getStatus);
  const user = useAppSelector(authSelectors.getAuthUser) as IAuthData;
  const [showPassword, setShowPassword] = useState<boolean>(false);
  const handlePasswordVisibility = () => setShowPassword(!showPassword);

  useEffect(() => {
    if (user && status === "success") {
      navigate("/");
    }
  }, [status, user]);

  return {
    dispatch,
    errorToast,
    navigate,
    user,
    handlePasswordVisibility,
    showPassword,
    isLoading: status === "loading"
  };
}

export default useLoginFormController;
