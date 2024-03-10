import { authSelectors } from "@store/auth";
import { useGetDoctorById } from "@hooks/doctor/useGetDoctorById";
import { useGetPartialUserById } from "@hooks/user/useGetPartialUserById";
import { IAuthData } from "@interfaces/IAuth";
import { useAppSelector } from "@store/index";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

interface IProps {
  id?: string;
}

function useDoctorController({ id }: IProps) {
  const navigate = useNavigate();
  const authUser = useAppSelector(authSelectors.getAuthUser) as IAuthData;
  const { doctorInfoData, isDoctorInfoLoading, getDoctorById } =
    useGetDoctorById({ id });
  const { getUserInfoById, partialUserInfo, isUserInfoLoading } =
    useGetPartialUserById({ id });
  const [rating, setRating] = useState<number>(0);
  const user = {
    ...doctorInfoData,
    ...partialUserInfo,
  };

  useEffect(() => {
    if (doctorInfoData?.rating) {
      setRating(doctorInfoData.rating);
    }
  }, [doctorInfoData]);

  useEffect(() => {
    if (id) {
      getDoctorById(id);
      getUserInfoById(id);
    }
  }, [id]);

  return {
    navigate,
    authUser,
    user,
    rating,
    doctor: {
        doctorInfoData,
        isDoctorInfoLoading
    },
    userInfo: {
        isUserInfoLoading
    }
  }
}

export default useDoctorController;
