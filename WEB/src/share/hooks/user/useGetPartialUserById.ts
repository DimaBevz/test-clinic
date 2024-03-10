import { useCallback } from "react";
import { useLazyGetPartialUserByIdQuery } from "@api/user.service";
import { IAuthData } from "@interfaces/IAuth";

interface IProps {
  id?: string;
}

export const useGetPartialUserById = ({
  id,
}: IProps): {
  partialUserInfo?: IAuthData;
  getUserInfoById: (id: string) => void;
  isUserInfoLoading: boolean;
} => {
  const [getUserInfoByIdQuery, { isLoading, data }] = useLazyGetPartialUserByIdQuery();

  const getUserInfoById = useCallback(
    (id: string) => {
      if (id) {
        getUserInfoByIdQuery(id);
      }
    },
    [id, getUserInfoByIdQuery]
  );

  return {
    partialUserInfo: data,
    getUserInfoById,
    isUserInfoLoading: isLoading,
  };
};
