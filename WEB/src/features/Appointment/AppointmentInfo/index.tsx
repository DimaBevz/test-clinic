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
import { CustomContainer } from "@components/Container";
import { CustomPaper } from "@components/Paper";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";
import { RxAvatar } from "react-icons/rx";
import { Preloader } from "@components/preloader/Preloader";
import { MdOutlineAccessTime } from "react-icons/md";
import useAppointmentInfoController from "./appointment-info.controller";
import { convertTo12HourFormatUTC } from "@utils/formatUTC";

export const AppointmentInfo = () => {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const { getSessionInfo } = useAppointmentInfoController();
  const { data } = getSessionInfo;

  if (getSessionInfo.isLoading) {
    return <Preloader size="xl" />;
  }

  return (
    <CustomContainer flexWrap="wrap">
      <VStack gap={4} w="49%">
        <CustomPaper w="100%">
          {data && (
            <HStack alignItems="flex-start" p={4}>
              <VStack pr={2} gap={10} w="100%">
                <HStack gap={4}>
                  <Tooltip shouldWrapChildren label={t("ViewProfile")}>
                    {data.physician.photoUrl ? (
                      <Image
                        borderRadius="full"
                        verticalAlign="middle"
                        w="100px"
                        _hover={{ cursor: "pointer" }}
                        onClick={() =>
                          navigate(`/doctors/${data.physician.id}`)
                        }
                        h="100px"
                        minW="100px"
                        objectFit="cover"
                        src={data.physician.photoUrl}
                        alt={t("Doctor`s avatar")}
                      />
                    ) : (
                      <Icon
                        as={RxAvatar}
                        _hover={{ cursor: "pointer" }}
                        onClick={() =>
                          navigate(`/doctors/${data.physician.id}`)
                        }
                        w="100px"
                        h="100px"
                        color="#DD6B20"
                      />
                    )}
                  </Tooltip>
                  <Stack display="flex" alignItems="center" gap={2}>
                    <Text
                      fontWeight={600}
                    >{`${data.physician.firstName} ${data.physician.lastName}`}</Text>
                    <Badge
                      px="10px"
                      py="2px"
                      borderRadius="full"
                      colorScheme="orange"
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
                    {data?.physician ? (
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
        </CustomPaper>
        <CustomPaper w="100%">
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
                  <Text pt={2}>{`${convertTo12HourFormatUTC(
                    `${data?.startTime}`
                  )} - ${convertTo12HourFormatUTC(`${data?.endTime}`)}
			            `}</Text>
                </Box>
                <Box>
                  <Text className="UserBlock__title">
                    {t("AppointmentStatus")}
                  </Text>
                  <Text pt={2}>
                    {data?.isDeleted && <>{t("Deleted")}</>}
                    {data?.isArchived && <>{t("Archived")}</>}
                    {!data?.isArchived && !data?.isDeleted && (
                      <>{t("Active")}</>
                    )}
                  </Text>
                </Box>
              </HStack>
              <HStack justifyContent="space-between">
                <Box>
                  <Text className="UserBlock__title">
                    {t("HighestPainScaleLastMonth")}
                  </Text>
                  <Text pt={2}>{data?.highestPainScaleLastMonth}</Text>
                </Box>
                <Box>
                  <Text className="UserBlock__title">
                    {t("AveragePainScaleLastMonth")}
                  </Text>
                  <Text pt={2}>{data?.averagePainScaleLastMonth}</Text>
                </Box>
              </HStack>
              <HStack justifyContent="space-between">
                <Box>
                  <Text className="UserBlock__title">
                    {t("CurrentPainScale")}
                  </Text>
                  <Text pt={2}>{data?.currentPainScale}</Text>
                </Box>
              </HStack>
              <HStack>
                <Box>
                  <Text className="UserBlock__title">
                    {t("ProblemDesctiption")}
                  </Text>
                  <Text pt={2}>{data?.complaints}</Text>
                </Box>
              </HStack>
              <Divider my={2} />
              <HStack>
                {data?.diagnosisTitle && (
                  <Box>
                    <Text className="UserBlock__title">
                      {t("DiagnosisTitle")}
                    </Text>
                    <Text pt={2}>{data?.diagnosisTitle}</Text>
                  </Box>
                )}
              </HStack>
              <HStack>
                {data?.recommendations && (
                  <Box>
                    <Text className="UserBlock__title">
                      {t("Recommendations")}
                    </Text>
                    <Text pt={2}>{data?.recommendations}</Text>
                  </Box>
                )}
              </HStack>
              <HStack>
                {data?.treatment && (
                  <Box>
                    <Text className="UserBlock__title">{t("Treatment")}</Text>
                    <Text pt={2}>{data?.treatment}</Text>
                  </Box>
                )}
              </HStack>
            </>
          </Box>
        </CustomPaper>
      </VStack>

      <VStack gap={4} w="49%">
        <CustomPaper w="100%">
          {data && (
            <HStack alignItems="flex-start" p={4}>
              <VStack pr={2} gap={10} w="100%">
                <HStack gap={4}>
                  <Tooltip shouldWrapChildren label={t("ViewProfile")}>
                    {data.patient.photoUrl ? (
                      <Image
                        borderRadius="full"
                        verticalAlign="middle"
                        w="100px"
                        _hover={{ cursor: "pointer" }}
                        // onClick={() => navigate(`/doctors/${data.patient.id}`)}
                        h="100px"
                        minW="100px"
                        objectFit="cover"
                        src={data.patient.photoUrl}
                        alt={t("Doctor`s avatar")}
                      />
                    ) : (
                      <Icon
                        as={RxAvatar}
                        _hover={{ cursor: "pointer" }}
                        // onClick={() => navigate(`/doctors/${data.patient.id}`)}
                        w="100px"
                        h="100px"
                        color="#DD6B20"
                      />
                    )}
                  </Tooltip>
                  <Stack display="flex" alignItems="center" gap={2}>
                    <Text
                      fontWeight={600}
                    >{`${data.patient.firstName} ${data.patient.lastName}`}</Text>
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
        </CustomPaper>
      </VStack>
    </CustomContainer>
  );
};
