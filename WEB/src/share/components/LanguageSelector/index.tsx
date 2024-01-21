import {
  Icon,
  Menu,
  MenuButton,
  MenuDivider,
  MenuItem,
  MenuList,
  Text,
} from "@chakra-ui/react";
import i18n, { handleChangeLanguage } from "../../../i18n.ts";
import { useState } from "react";
import { MdKeyboardArrowDown, MdKeyboardArrowUp } from "react-icons/md";
import { useTranslation } from "react-i18next";
import "./index.scss";

const LanguageSelector = () => {
  const { t } = useTranslation();
  const [isReverseArrow, setIsReverseArrow] = useState(false);
  const reverseIcon = () => {
    setIsReverseArrow(!isReverseArrow);
  };
  return (
    <Menu>
      <MenuButton
        bgColor="transparent"
        p={2}
        borderRadius={"8px"}
        onClick={reverseIcon}
      >
        <Text className="LanguageMenu__menu-button">
          {t(i18n.language)}
          <Text lineHeight="0.5">
            {!isReverseArrow ? (
              <Icon as={MdKeyboardArrowUp} />
            ) : (
              <Icon as={MdKeyboardArrowDown} />
            )}
          </Text>
        </Text>
      </MenuButton>
      <MenuList defaultValue={i18n.language}>
        <MenuItem
          onClick={handleChangeLanguage as any}
          value="en"
          className="LanguageMenu__menu-item"
        >
          English
        </MenuItem>
        <MenuDivider />
        <MenuItem
          onClick={handleChangeLanguage as any}
          value="uk"
          className="LanguageMenu__menu-item"
        >
          Українська
        </MenuItem>
      </MenuList>
    </Menu>
  );
};

export default LanguageSelector;
