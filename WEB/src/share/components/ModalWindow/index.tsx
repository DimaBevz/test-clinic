import {
  Button,
  Modal,
  ModalBody,
  ModalCloseButton,
  ModalContent,
  ModalFooter,
  ModalHeader,
  ModalOverlay,
} from "@chakra-ui/react";
import { ReactNode } from "react";

interface ModalWindowProps {
  isOpen: boolean;
  onClose: () => void;
  content: ReactNode;
  onSubmit?: () => void;
  onSubmitText?: string;
  onCloseText?: string;
  title?: string;
  isActions?: boolean;
  isDisabled?: boolean;
  isHideCancelButton?: boolean;
  isLoading?: boolean;
}

export const ModalWindow = ({
  isOpen,
  onClose,
  content,
  onSubmit,
  onSubmitText,
  onCloseText,
  title,
  isActions,
  isDisabled,
  isHideCancelButton,
  isLoading,
}: ModalWindowProps) => {
  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <ModalOverlay />
      <ModalContent>
        {title && <ModalHeader color="#000">{title}</ModalHeader>}
        <ModalCloseButton />
        <ModalBody>{content}</ModalBody>

        {isActions && (
          <ModalFooter>
            {!isHideCancelButton && (
              <Button
                variant="outline"
                mr={3}
                onClick={onClose}
              >
                {onCloseText}
              </Button>
            )}
            <Button
              isLoading={isLoading && isLoading}
              onClick={onSubmit}
              isDisabled={isDisabled}
            >
              {onSubmitText}
            </Button>
          </ModalFooter>
        )}
      </ModalContent>
    </Modal>
  );
};
