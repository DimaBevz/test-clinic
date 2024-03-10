import {
  Badge,
  Box,
  Button,
  Divider,
  HStack,
  Icon,
  Image,
  Stack,
  Text,
} from "@chakra-ui/react";
import { StarRating } from "@components/index.ts";
import { IDoctorListModel } from "@interfaces/doctor.ts";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";
import { RxAvatar } from "react-icons/rx";
import { useAppSelector } from "@store/index.ts";
import authSelectors from "@store/auth/auth.selectors";
import { formatWorkExperience } from "@utils/formatWorkExperience.ts";
import "./index.scss";

const DoctorCard = ({ physician }: { physician: IDoctorListModel }) => {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const user = useAppSelector(authSelectors.getAuthUser);
  
  return (
    <Stack key={physician.id} className="DoctorCard" p={6} borderRadius="lg">
      <HStack>
        {physician.photoUrl ? (
          <Image
            borderRadius="full"
            verticalAlign="middle"
            w="100px"
            h="100px"
            objectFit="cover"
            src={physician.photoUrl}
            alt={t("Doctor`s avatar")}
          />
        ) : (
          <Icon as={RxAvatar} w="100px" h="100px" color="#ECC94B" />
        )}
        <Box p="6" w="100%">
          <Stack isInline alignItems="baseline">
            <Text
              mt="2"
              fontSize="2xl"
              fontWeight="semibold"
              lineHeight="tight"
              isTruncated
            >
              {`${physician.firstName} ${physician.lastName}`}
            </Text>
          </Stack>

          <Stack
            direction="row"
            flexWrap="wrap"
            alignItems="baseline"
            spacing={2}
          >
            {physician.positions.map((position, index) => (
              <Badge
                key={index}
                px="2"
                py="1"
                borderRadius="full"
                colorScheme="yellow"
                flexShrink={1}
                isTruncated
                maxWidth="100%"
              >
                {position.specialty}
              </Badge>
            ))}
          </Stack>

          <Text mt={2}>
            {`${t("Experience")}: ${formatWorkExperience(
              physician.experience || 0
            )}`}
          </Text>
          <Stack direction="row" align="center" justify="space-between" mt={2}>
            <StarRating value={physician.rating} />
            <Text>{`${t("Comments")}: ${physician.commentsCount}`}</Text>
          </Stack>
          <Divider my={2} />
          <Text mt={2}>{physician.bio}</Text>
        </Box>
      </HStack>
      <HStack justifyContent="space-between" w="100%">
        <Box />
        <HStack gap="20px">
          <Button variant="outline" onClick={() => navigate(`/doctors/${physician.id}`)}>
            {t("Details")}
          </Button>
          {!user?.role ? (
            <Button onClick={() => navigate(`/doctors/appointment/${physician?.id}`)}>
              {t("Make an appointment")}
            </Button>
          ) : (
            <></>
          )}
        </HStack>
      </HStack>
    </Stack>
  );
};

export default DoctorCard;
