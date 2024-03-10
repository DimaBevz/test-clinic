import { ReactNode } from "react";
import { useTranslation } from "react-i18next";

import {
  Button,
  Drawer as ChakraDrawer,
  DrawerBody,
  DrawerCloseButton,
  DrawerContent,
  DrawerFooter,
  DrawerHeader,
  DrawerOverlay,
} from "@chakra-ui/react";

interface DrawerExampleProps {
  refProp: any;
  placement: "right" | "left" | "bottom" | "top";
  title?: string;
  isOpen: boolean;
  content: ReactNode;
  handleCloseDrawer: () => void;
  handleSubmit?: () => void;
}

export function Drawer({
  refProp,
  title,
  isOpen,
  content,
  handleCloseDrawer,
  handleSubmit,
}: DrawerExampleProps) {
  const { t } = useTranslation();

  return (
    <ChakraDrawer
      isOpen={isOpen}
      placement="right"
      onClose={handleCloseDrawer}
      finalFocusRef={refProp}
      size="md"
    >
      <DrawerOverlay />
      <DrawerContent>
        <DrawerCloseButton />
        {title && <DrawerHeader color="#000">{title}</DrawerHeader>}

        <DrawerBody>{content}</DrawerBody>

        {handleSubmit && (
          <DrawerFooter>
            <Button variant="outline" mr={3} onClick={handleCloseDrawer}>
              {t("Cancel")}
            </Button>
            <Button onClick={handleSubmit}>
              {t("Save")}
            </Button>
          </DrawerFooter>
        )}
      </DrawerContent>
    </ChakraDrawer>
  );
}
