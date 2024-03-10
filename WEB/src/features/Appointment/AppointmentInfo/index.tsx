import {
  HStack,
  VStack,
  Image,
  Icon,
  Tooltip,
  Stack,
  Text,
  Box,
  Heading,
  Badge,
  Divider,
} from "@chakra-ui/react";
import { useTranslation } from "react-i18next";
import { useParams } from "react-router-dom";
import { RxAvatar } from "react-icons/rx";
import { Preloader } from "@components/preloader/Preloader";
import { MdOutlineAccessTime } from "react-icons/md";
import i18n from "../../../i18n.ts";
import "./index.scss";
import useAppointmentInfoController from "@features/Appointment/AppointmentInfo/appointment-info.controller.ts";
import { Container } from "@components/Container";
import { Paper } from "@components/Paper";

export const AppointmentInfo = () => {
  const { t } = useTranslation();
  const { id } = useParams();
  const {
    navigate,
    session: { sessionInfoData, isSessionInfoLoading },
    tests: { testsListResult, isTestsListResultLoading },
  } = useAppointmentInfoController({ id });
  
  if (isSessionInfoLoading) {
    return <Preloader size="xl" />;
  }
  
  return (
    <Container flexWrap="wrap">
      <VStack gap={4} w="49%">
        <Paper w="100%">
          {sessionInfoData && (
            <HStack alignItems="flex-start" p={4}>
              <VStack pr={2} gap={10} w="100%">
                <HStack gap={4}>
                  <Tooltip shouldWrapChildren label={t("ViewProfile")}>
                    {sessionInfoData.physician.photoUrl ? (
                      <Image
                        borderRadius="full"
                        verticalAlign="middle"
                        w="100px"
                        _hover={{ cursor: "pointer" }}
                        onClick={() =>
                          navigate(`/doctors/${sessionInfoData.physician.id}`)
                        }
                        h="100px"
                        minW="100px"
                        objectFit="cover"
                        src={sessionInfoData.physician.photoUrl}
                        alt={t("Doctor`s avatar")}
                      />
                    ) : (
                      <Icon
                        as={RxAvatar}
                        _hover={{ cursor: "pointer" }}
                        onClick={() =>
                          navigate(`/doctors/${sessionInfoData.physician.id}`)
                        }
                        w="100px"
                        h="100px"
                        color="#ECC94B"
                      />
                    )}
                  </Tooltip>
                  <Stack display="flex" alignItems="center" gap={2}>
                    <Text
                      fontWeight={600}
                    >{`${sessionInfoData.physician.firstName} ${sessionInfoData.physician.lastName}`}</Text>
                    <Badge
                      px="10px"
                      py="2px"
                      borderRadius="full"
                      colorScheme="blue"
                    >
                      {t("Physician")}
                    </Badge>
                  </Stack>
                </HStack>
                <Box pt={2} w="100%">
                  <Heading as={"h4"} size={"md"}>
                    {t("Info")}
                  </Heading>
                  <Box>
                    {sessionInfoData?.physician ? (
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
                  </Box>
                </Box>
              </VStack>
            </HStack>
          )}
        </Paper>
        <Paper w="100%">
          <Box pt={2} w="100%">
            <Heading as={"h4"} size={"md"}>
              {t("AppointmentInfo")}
            </Heading>
            <>
              <HStack pt={3} justifyContent="space-between">
                <Box>
                  <Text className="UserBlock__title">
                    <Text display="flex" alignItems="center" gap={2}>
                      <Icon as={MdOutlineAccessTime} w="20px" h="20px" />
                      {t("ReceptionHour")}
                    </Text>
                  </Text>
                  <Text pt={2}>{`${
                    `${sessionInfoData && new Date(sessionInfoData.startTime).toLocaleTimeString(i18n.language, { hour: 'numeric', minute: 'numeric' })}`
                  } - ${
                    `${sessionInfoData && new Date(sessionInfoData.endTime).toLocaleTimeString(i18n.language, { hour: 'numeric', minute: 'numeric' })}`
                  }
			            `}</Text>
                </Box>
                <Box>
                  <Text className="UserBlock__title">
                    {t("AppointmentStatus")}
                  </Text>
                  <Text pt={2}>
                    {sessionInfoData?.isDeleted && <>{t("Deleted")}</>}
                    {sessionInfoData?.isArchived && <>{t("Archived")}</>}
                    {!sessionInfoData?.isArchived &&
                      !sessionInfoData?.isDeleted && <>{t("Active")}</>}
                  </Text>
                </Box>
              </HStack>
              <HStack justifyContent="space-between">
                <Box>
                  <Text className="UserBlock__title">
                    {t("HighestPainScaleLastMonth")}
                  </Text>
                  <Text pt={2}>
                    {sessionInfoData?.highestPainScaleLastMonth}
                  </Text>
                </Box>
                <Box>
                  <Text className="UserBlock__title">
                    {t("AveragePainScaleLastMonth")}
                  </Text>
                  <Text pt={2}>
                    {sessionInfoData?.averagePainScaleLastMonth}
                  </Text>
                </Box>
              </HStack>
              <HStack justifyContent="space-between">
                <Box>
                  <Text className="UserBlock__title">
                    {t("CurrentPainScale")}
                  </Text>
                  <Text pt={2}>{sessionInfoData?.currentPainScale}</Text>
                </Box>
              </HStack>
              <HStack>
                <Box>
                  <Text className="UserBlock__title">
                    {t("ProblemDescription")}
                  </Text>
                  <Text pt={2}>{sessionInfoData?.complaints}</Text>
                </Box>
              </HStack>
              <Divider my={2} />
              <HStack>
                {sessionInfoData?.diagnosisTitle && (
                  <Box>
                    <Text className="UserBlock__title">
                      {t("DiagnosisTitle")}
                    </Text>
                    <Text pt={2}>{sessionInfoData?.diagnosisTitle}</Text>
                  </Box>
                )}
              </HStack>
              <HStack>
                {sessionInfoData?.recommendations && (
                  <Box>
                    <Text className="UserBlock__title">
                      {t("Recommendations")}
                    </Text>
                    <Text pt={2}>{sessionInfoData?.recommendations}</Text>
                  </Box>
                )}
              </HStack>
              <HStack>
                {sessionInfoData?.treatment && (
                  <Box>
                    <Text className="UserBlock__title">{t("Treatment")}</Text>
                    <Text pt={2}>{sessionInfoData?.treatment}</Text>
                  </Box>
                )}
              </HStack>
            </>
          </Box>
        </Paper>
      </VStack>
      
      <VStack gap={4} w="49%">
        <Paper w="100%">
          {sessionInfoData && (
            <HStack alignItems="flex-start" p={4}>
              <VStack pr={2} gap={10} w="100%">
                <HStack gap={4}>
                  <Tooltip shouldWrapChildren label={t("ViewProfile")}>
                    {sessionInfoData.patient.photoUrl ? (
                      <Image
                        borderRadius="full"
                        verticalAlign="middle"
                        w="100px"
                        _hover={{ cursor: "pointer" }}
                        // onClick={() => navigate(`/doctors/${sessionInfoData.patient.id}`)}
                        h="100px"
                        minW="100px"
                        objectFit="cover"
                        src={sessionInfoData.patient.photoUrl}
                        alt={t("Doctor`s avatar")}
                      />
                    ) : (
                      <Icon
                        as={RxAvatar}
                        _hover={{ cursor: "pointer" }}
                        // onClick={() => navigate(`/doctors/${sessionInfoData.patient.id}`)}
                        w="100px"
                        h="100px"
                        color="#ECC94B"
                      />
                    )}
                  </Tooltip>
                  <Stack display="flex" alignItems="center" gap={2}>
                    <Text
                      fontWeight={600}
                    >{`${sessionInfoData.patient.firstName} ${sessionInfoData.patient.lastName}`}</Text>
                    <Badge
                      px="10px"
                      py="2px"
                      borderRadius="full"
                      colorScheme="purple"
                    >
                      {t("Patient")}
                    </Badge>
                  </Stack>
                </HStack>
                <Box pt={2} w="100%">
                  <Heading as={"h4"} size={"md"}>
                    {t("Info")}
                  </Heading>
                  <HStack justifyContent="space-between">
                    <Box>
                      <Text className="UserBlock__title">{t("Job")}</Text>
                      <Text pt={2}>ВДПСУ</Text>
                    </Box>
                    <Box>
                      <Text
                        className="UserBlock__title"
                        display="flex"
                        justifyContent="flex-end"
                      >
                        {t("Position")}
                      </Text>
                      <Text pt={2} display="flex" justifyContent="flex-end">
                        Operational manager
                      </Text>
                    </Box>
                  </HStack>
                </Box>
              </VStack>
            </HStack>
          )}
        </Paper>
        <Paper w="100%">
          {isTestsListResultLoading ? (
            <Preloader size="xl" />
          ) : (
            <Box pt={2} w="100%" className="AppointmentInfo">
              <Heading as={"h4"} size={"md"}>
                {t("InformationTestsPassed")}
              </Heading>
              <Box className="AppointmentInfo__test-block">
                {testsListResult?.map((test) => (
                  <Box
                    key={test.id}
                    pt={3}
                    className="AppointmentInfo__test-card-container"
                  >
                    <Box className="AppointmentInfo__test-card-header">
                      <Box>{test.name}</Box>
                      <Box>{`${t("Score")}: ${test.totalScore}`}</Box>
                    </Box>
                    <Divider />
                    <Box className="AppointmentInfo__test-card-content">
                      <Box>{test.description}</Box>
                      <Box className="AppointmentInfo__conclusion">
                        <Box className="AppointmentInfo__conclusion-label">
                          {t("Conclusion")}:
                        </Box>
                        <Box>{test.verdict}</Box>
                      </Box>
                    </Box>
                  </Box>
                ))}
              </Box>
            </Box>
          )}
        </Paper>
      </VStack>
    </Container>
  );
};
