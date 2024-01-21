import { useState } from "react";

import {
  Box,
  HStack,
  Icon,
  Image,
  Menu,
  MenuButton,
  MenuDivider,
  MenuGroup,
  MenuItem,
  MenuList,
  Skeleton,
  Text,
} from "@chakra-ui/react";
import { CgProfile } from "react-icons/cg";
import { IoMdExit } from "react-icons/io";
import { MdKeyboardArrowDown, MdKeyboardArrowUp } from "react-icons/md";

import { useTranslation } from "react-i18next";

import logoUA from "@assets/img/UAPrometeiLogoCMYKLarge.png";
import { useAppDispatch, useAppSelector } from "@store/index.ts";
import { authActions, authSelectors } from "@features/auth";
import { useNavigate } from "react-router-dom";
import { LanguageSelector } from "@components/index.ts";
import useGetUserBadge from "@utils/useGetUserBadge.tsx";

import "./index.scss";

export const Header = ({isLanding=false}: {isLanding?: boolean}) => {
  const { t } = useTranslation();
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const user = useAppSelector(authSelectors.getAuthUser);
  const userStatus = useAppSelector(authSelectors.getStatus);
  const [isReverseArrow, setIsReverseArrow] = useState(false);
  const userBage = useGetUserBadge(user?.role);

  const isUserDataLoading = userStatus === "loading";

  const reverseIcon = () => {
    setIsReverseArrow(!isReverseArrow);
  };

  const logout = () => {
    dispatch(authActions.logout());
  };
  console.log(isLanding);

  return (
    <Box className="Header">
      <Image h={"100%"} src={logoUA} />
      <HStack gap={5}>
        {isUserDataLoading ? (
          <Skeleton h="20px" w="200px" mr={7} />
        ) : !user ? <></> : (
          <Menu placement={"bottom-end"}>
            <MenuButton p={2} borderRadius={"8px"} onClick={reverseIcon}>
              <Text className="Header__menu-button">
                {user?.email}
                {userBage}
                <Text lineHeight="0.5">
                  {!isReverseArrow ? (
                    <Icon as={MdKeyboardArrowUp} />
                  ) : (
                    <Icon as={MdKeyboardArrowDown} />
                  )}
                </Text>
              </Text>
            </MenuButton>
            <MenuList>
              <MenuGroup title="">
                <MenuItem
                  onClick={() => navigate("/profile")}
                  className="Header__menu-item"
                >
                  <Icon as={CgProfile} />
                  {t("MyAccount")}
                </MenuItem>
              </MenuGroup>
              <MenuDivider />
              <MenuGroup title="">
                <MenuItem onClick={logout} className="Header__menu-item">
                  <Icon as={IoMdExit} />
                  {t("Exit")}
                </MenuItem>
              </MenuGroup>
            </MenuList>
          </Menu>
        )}

        <LanguageSelector />
      </HStack>
    </Box>
  );
};
