import { useState } from "react";

import {
  Box, Button,
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

import logo from "@assets/img/EN Prometei Logo CMYK.png";
import logoUA from "@assets/img/UA Prometei Logo CMYK.png";
import { useAppDispatch, useAppSelector } from "@store/index.ts";
import { authActions, authSelectors } from "@store/auth/index.ts";
import { useNavigate } from "react-router-dom";
import { LanguageSelector } from "@components/index.ts";
import useGetUserBadge from "@hooks/useGetUserBadge.tsx";

import "./index.scss";
import i18n from "../../../i18n.ts";

export const Header = () => {
  const { t } = useTranslation();
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const user = useAppSelector(authSelectors.getAuthUser);
  const userStatus = useAppSelector(authSelectors.getStatus);
  const [isReverseArrow, setIsReverseArrow] = useState(false);
  const userBadge = useGetUserBadge(user?.role);

  const isUserDataLoading = userStatus === "loading";

  const reverseIcon = () => {
    setIsReverseArrow(!isReverseArrow);
  };

  const logout = () => {
    dispatch(authActions.logout());
  };

  return (
    <Box className="Header">
      <Image maxH="50px" _hover={{cursor: "pointer"}} onClick={()=>navigate("/")}  src={ i18n.language === "en" ? logo : logoUA } />
      <HStack gap={5}>
        {isUserDataLoading ? (
          <Skeleton h="20px" w="200px" mr={7} />
        ) : !user ? <Button colorScheme="orange" borderRadius="full" onClick={() => navigate("/login")}>{t("Login")}</Button> : (
          <Menu placement={"bottom-end"}>
            <MenuButton p={2} borderRadius={"8px"} onClick={reverseIcon}>
              <Text className="Header__menu-button">
                {user?.email}
                {userBadge}
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
