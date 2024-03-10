import { useToast } from "@hooks/general/useToast.ts";
import { useAppDispatch } from "@store/index.ts";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

function useConfirmUserController() {
  const dispatch = useAppDispatch();
  const { successToast, errorToast } = useToast();
  const navigate = useNavigate();
  const [loading, setIsLoading] = useState(false);
  
  return {
    dispatch,
    successToast,
    errorToast,
    navigate,
    loading,
    setIsLoading,
  };
}

export default useConfirmUserController;
