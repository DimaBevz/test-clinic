import { Badge } from "@chakra-ui/react";
import { useTranslation } from "react-i18next";
export default function useGetUserBadge(userRole?: number) {
  const { t } = useTranslation();
  switch (userRole) {
    case 0:
      return (
        <Badge
          variant="subtle"
          px="10px"
          py="2px"
          borderRadius="full"
          colorScheme="purple"
        >
          {t("Patient")}
        </Badge>
      );
    case 1:
      return (
        <Badge
          variant="subtle"
          px="10px"
          py="2px"
          borderRadius="full"
          colorScheme="yellow"
        >
          {t("Physician")}
        </Badge>
      );
    case 2:
      return (
        <Badge
          variant="subtle"
          px="10px"
          py="2px"
          borderRadius="full"
          colorScheme="red"
        >
          {t("Admin")}
        </Badge>
      );
    default:
      return (
        <Badge
          variant="subtle"
          px="10px"
          py="2px"
          borderRadius="full"
          colorScheme="grey"
        >
          User
        </Badge>
      );
  }
}
