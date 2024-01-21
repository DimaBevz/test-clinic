import { CustomContainer } from "@components/Container";
import { CustomPaper } from "@components/Paper";
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

import "./index.scss";
import { useTranslation } from "react-i18next";
import { CommentsBlock, Documents, StarRating } from "@components/index";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import doctorsService from "@features/Doctor/doctors.service.ts";
import userService from "@features/auth/user.service.ts";
import { useAppSelector } from "@store/index.ts";
import { authSelectors } from "@features/auth";
import { IAuthData } from "@interfaces/IAuth.ts";
import { Preloader } from "@components/preloader/Preloader";
import { RxAvatar } from "react-icons/rx";
import { formatWorkExperience } from "@utils/formatWorkExperience";

export const DoctorPage = () => {
  const { t } = useTranslation();
  const { id } = useParams();
  const navigate = useNavigate();
  const authUser = useAppSelector(authSelectors.getAuthUser) as IAuthData;
  const { data: DoctorData, isLoading: isDoctorLoading } =
    doctorsService.useGetDoctorByIdQuery(id as string);
  const { data: GeneralData, isLoading: isGeneralLoading } =
    userService.useGetPartialUserByIdQuery(id as string);
  const [rating, setRating] = useState(0);
  const user = {
    ...DoctorData,
    ...GeneralData,
  };

  useEffect(() => {
    if (DoctorData?.rating) {
      setRating(DoctorData.rating);
    }
  }, [DoctorData]);

  useEffect(() => {
    if (authUser.id === id) {
      navigate("/profile");
    }
  }, [authUser, id]);

  if (isDoctorLoading || isGeneralLoading) {
    return <Preloader size="xl" />;
  }

  return (
    <CustomContainer flexWrap="wrap">
      {isDoctorLoading || isGeneralLoading ? (
        <Preloader size="xl" />
      ) : (
        <>
          <VStack gap={4} w="49%">
            <CustomPaper w="100%">
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
                          color="#DD6B20"
                        />
                      )}
                      <VStack>
                        <Text
                          fontWeight={600}
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
                        <Button
                          colorScheme="orange"
                          onClick={() =>
                            navigate(`/doctors/appointment/${user?.id}`)
                          }
                        >
                          {t("Make an appointment")}
                        </Button>
                      ) : (
                        <></>
                      )}
                    </HStack>
                  </VStack>
                </HStack>
              )}
            </CustomPaper>

            <CustomPaper w="100%">
              <Documents isGuest userId={user.id as string} />
            </CustomPaper>
          </VStack>
          {user.role && (
            <CustomPaper w="49%">
              <CommentsBlock
                usersCommentsId={user.id as string}
                visitorId={authUser.id}
                isDoctor={authUser.role === 1}
              />
            </CustomPaper>
          )}
        </>
      )}
    </CustomContainer>
  );
};
