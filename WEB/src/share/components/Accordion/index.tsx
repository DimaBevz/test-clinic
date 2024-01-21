import { ReactNode, memo } from "react";

import { Box, Collapse, Icon } from "@chakra-ui/react";
import { MdKeyboardArrowUp } from "react-icons/md";
import { MdKeyboardArrowDown } from "react-icons/md";

import "./index.scss";

interface AccordionProps {
  isActive?: boolean;
  setIsActive: () => void;
  label: string | ReactNode;
  children: ReactNode;
  additionalProps?: ReactNode;
  isDisabled?: boolean;
}

export const Accordion = ({
  isActive,
  setIsActive,
  label,
  children,
  additionalProps,
  isDisabled,
}: AccordionProps) => {
  const isShow = isActive && !isDisabled;

  const Col = memo(
    ({ isShow, children }: { isShow?: boolean; children: ReactNode }) => {
      return (
        <Collapse in={isShow}>
          <Box className="Accordion__content">{children}</Box>
        </Collapse>
      );
    }
  );

  return (
    <Box className="Accordion">
      <Box className="Accordion__header">
        {label}
        <Box
          className="Accordion__actions"
          onClick={setIsActive}
        >
          {additionalProps && additionalProps}
          <Box
            className={`Accordion__icon ${
              isDisabled ? "Accordion__disabled-icon" : ""
            }`}
          >
            {!isShow ? (
              <Icon as={MdKeyboardArrowUp} w={6} h={6} />
            ) : (
              <Icon as={MdKeyboardArrowDown} w={6} h={6} />
            )}
          </Box>
        </Box>
      </Box>
      <Col isShow={isShow}>{children}</Col>
    </Box>
  );
};
