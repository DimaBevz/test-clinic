import { useTranslation } from "react-i18next";
import { useParams } from "react-router-dom";

import { Container } from "@components/Container";
import { Paper } from "@components/Paper";
import {
  Box,
  Button,
  Heading,
  HStack,
  Icon,
  Image,
  Text,
  VStack,
} from "@chakra-ui/react";

import useDoctorController from "./doctor.controller";
import { Preloader } from "@components/preloader/Preloader";
import { CommentsBlock, Documents, StarRating } from "@components/index";
import { formatWorkExperience } from "@utils/formatWorkExperience";
import { RxAvatar } from "react-icons/rx";

import "./index.scss";

export const DoctorInfo = () => {
  const { t } = useTranslation();
  const { id } = useParams();
  const { doctor, user, userInfo, rating, authUser, navigate } =
    useDoctorController({ id });
  const { isDoctorInfoLoading } = doctor;
  const { isUserInfoLoading } = userInfo;

  if (isDoctorInfoLoading || isUserInfoLoading) {
    return <Preloader size="xl" />;
  }

  return (
    <Container flexWrap="wrap">
      {isDoctorInfoLoading || isUserInfoLoading ? (
        <Preloader size="xl" />
      ) : (
        <>
          <VStack gap={4} w="49%">
            <Paper w="100%">
              {user && (
                <HStack alignItems="flex-start" w="100%" p={4}>
                  <VStack pr={2} gap={10} w="100%">
                    <HStack gap={4}>
                      {user.photoUrl ? (
                        <Image
                          borderRadius="full"
                          verticalAlign="middle"
                          w="120px"
                          h="120px"
                          minW="100px"
                          objectFit="cover"
                          src={user.photoUrl}
                          alt={t("Doctor`s avatar")}
                        />
                      ) : (
                        <Icon
                          as={RxAvatar}
                          w="120px"
                          h="120px"
                          color="#ECC94B"
                        />
                      )}
                      <VStack>
                        <Text
                          fontWeight={600}
                          fontSize="20px"
                        >{`${user.firstName} ${user.lastName}`}</Text>
                      </VStack>
                    </HStack>
                    <Box pt={2} w="100%">
                      <Heading as={"h4"} size={"md"}>
                        {t("Info")}
                      </Heading>
                      <Box>
                        {user && "rating" in user ? (
                          <HStack justifyContent="space-between" pt={2}>
                            <Text>{t("Rating")}</Text>
                            <StarRating value={rating} />
                          </HStack>
                        ) : (
                          <Box></Box>
                        )}
                        {"experience" in user && user.experience ? (
                          <HStack justifyContent="space-between" pt={2}>
                            <Text>{t("Experience")}</Text>
                            <Text>
                              {formatWorkExperience(user.experience || 0)}
                            </Text>
                          </HStack>
                        ) : (
                          <Box></Box>
                        )}
                        {"bio" in user && user.bio && (
                          <>
                            <Text className="UserBlock__title">{t("Bio")}</Text>
                            <Text pt={2}>
                              {user.bio.length > 200
                                ? `${user.bio.substring(0, 200)}...`
                                : user.bio}
                            </Text>
                          </>
                        )}
                      </Box>
                    </Box>
                    <HStack w="100%" gap="20px" justifyContent="space-between">
                      <Box></Box>
                      {!authUser?.role ? (
                        <Button onClick={() => navigate(`/doctors/appointment/${user?.id}`)}>
                          {t("Make an appointment")}
                        </Button>
                      ) : (
                        <></>
                      )}
                    </HStack>
                  </VStack>
                </HStack>
              )}
            </Paper>

            <Paper w="100%">
              <Documents isGuest userId={user.id as string} />
            </Paper>
          </VStack>
          {user.role && (
            <Paper w="49%">
              <CommentsBlock
                usersCommentsId={user.id as string}
                visitorId={authUser.id}
                isDoctor={authUser.role === 1}
              />
            </Paper>
          )}
        </>
      )}
    </Container>
  );
};
