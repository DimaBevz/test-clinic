import { Container } from "@components/Container";
import { Paper } from "@components/Paper";
import {
  Box,
  Button,
  Heading,
  HStack,
  Icon,
  Image,
  Stack,
  Text,
  Tooltip,
  VStack,
} from "@chakra-ui/react";

import "./index.scss";
import { useTranslation } from "react-i18next";

import { EditIcon } from "@chakra-ui/icons";
import { Documents, StarRating } from "@components/index";
import Comments from "@components/Comments";
import { RxAvatar } from "react-icons/rx";
import { formatWorkExperience } from "@utils/formatWorkExperience.ts";
import useProfileController from "./profile.controller";

export const Profile = () => {
  const { t } = useTranslation();
  const { user, comments, rating, hiddenFileInput, handleClick, handleChange } =
    useProfileController();

  return (
    <Container flexWrap="wrap">
      <VStack gap={4} w="49%" className={!user.role ? "Profile" : ""}>
        <Paper w="100%">
          {user && (
            <HStack alignItems="flex-start" p={4}>
              <VStack pr={2} gap={10} w="100%">
                <HStack gap={4}>
                  <Tooltip shouldWrapChildren label={t("Change photo")}>
                    {user.photoUrl ? (
                      <Image
                        borderRadius="full"
                        verticalAlign="middle"
                        w="100px"
                        _hover={{ cursor: "pointer" }}
                        onClick={handleClick}
                        h="100px"
                        minW="100px"
                        objectFit="cover"
                        src={user.photoUrl}
                        alt={t("Doctor`s avatar")}
                      />
                    ) : (
                      <Icon
                        as={RxAvatar}
                        _hover={{ cursor: "pointer" }}
                        onClick={handleClick}
                        w="100px"
                        h="100px"
                        color="#F6E05E"
                      />
                    )}
                  </Tooltip>
                  <input
                    type="file"
                    onChange={handleChange}
                    accept="image/*"
                    ref={hiddenFileInput}
                    style={{ display: "none" }}
                  />
                  <Stack>
                    <Text
                      fontWeight={600}
                      fontSize="20px"
                    >{`${user.firstName} ${user.lastName}`}</Text>
                  </Stack>
                  <Button backgroundColor="transparent">
                    <EditIcon />
                  </Button>
                </HStack>
                <Box pt={2} w="100%">
                  <Heading as={"h4"} size={"md"}>
                    {t("Info")}
                  </Heading>
                  <Box>
                    {user && "rating" in user ? (
                      <HStack justifyContent="space-between" pt={2}>
                        <Text className="UserBlock__title">{t("Rating")}</Text>
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
                    {user.role ? (
                      <HStack justifyContent="space-between">
                        <Box>
                          <Text className="UserBlock__title">
                            {t("Clinic")}
                          </Text>
                          <Text pt={2}>Alta Clinic</Text>
                        </Box>
                        <Box>
                          <Text
                            className="UserBlock__title"
                            display="flex"
                            justifyContent="flex-end"
                          >
                            {t("Address")}
                          </Text>
                          <Text pt={2} display="flex" justifyContent="flex-end">
                            Келецька, 32
                          </Text>
                        </Box>
                      </HStack>
                    ) : (
                      <></>
                    )}

                    {!user.role && (
                      <>
                        <HStack justifyContent="space-between">
                          <Box>
                            <Text className="UserBlock__title">{t("Job")}</Text>
                            <Text pt={2}>
                              {"institution" in user && user.institution.length
                                ? user.institution
                                : "ВДПСУ"}
                            </Text>
                          </Box>
                          <Box>
                            <Text
                              className="UserBlock__title"
                              display="flex"
                              justifyContent="flex-end"
                            >
                              {t("Position")}
                            </Text>
                            <Text
                              pt={2}
                              display="flex"
                              justifyContent="flex-end"
                            >
                              {"position" in user && user.position.length
                                ? user.position
                                : "Operational manager"}
                            </Text>
                          </Box>
                        </HStack>
                        <HStack justifyContent="space-between">
                          <Box>
                            <Text className="UserBlock__title">
                              {t("City")}
                            </Text>
                            <Text pt={2}>
                              {"settlement" in user && user.settlement.length
                                ? user.settlement
                                : "Вінниця"}
                            </Text>
                          </Box>
                          <Box>
                            <Text
                              className="UserBlock__title"
                              display="flex"
                              justifyContent="flex-end"
                            >
                              {t("Street")}
                            </Text>
                            <Text
                              pt={2}
                              display="flex"
                              justifyContent="flex-end"
                            >
                              {"street" in user && user.street.length
                                ? user.street
                                : "Келецька"}
                            </Text>
                          </Box>
                        </HStack>
                        <HStack justifyContent="space-between">
                          <Box>
                            <Text className="UserBlock__title">
                              {t("House")}
                            </Text>
                            <Text pt={2}>
                              {"house" in user && user.house?.length
                                ? user.house
                                : 16}
                            </Text>
                          </Box>
                          <Box>
                            <Text
                              className="UserBlock__title"
                              display="flex"
                              justifyContent="flex-end"
                            >
                              {t("Apartment")}
                            </Text>
                            <Text
                              pt={2}
                              display="flex"
                              justifyContent="flex-end"
                            >
                              {"apartment" in user && user.apartment?.length
                                ? user.apartment
                                : ""}
                            </Text>
                          </Box>
                        </HStack>
                      </>
                    )}

                    <HStack justifyContent="space-between">
                      <Box>
                        <Text className="UserBlock__title">{t("Phone")}</Text>
                        <Text pt={2}>{user.phoneNumber}</Text>
                      </Box>
                      <Box>
                        <Text
                          className="UserBlock__title"
                          display="flex"
                          justifyContent="flex-end"
                        >
                          {t("Email")}
                        </Text>
                        <Text pt={2} display="flex" justifyContent="flex-end">
                          {user.email}
                        </Text>
                      </Box>
                    </HStack>
                  </Box>
                </Box>
              </VStack>
            </HStack>
          )}
        </Paper>
        <Paper w="100%">
          <Documents userId={user.id} />
        </Paper>
      </VStack>

      {user.role === 1 && (
        <Paper w="49%">
          <Comments comments={comments} isBigger={true} />
        </Paper>
      )}
    </Container>
  );
};
