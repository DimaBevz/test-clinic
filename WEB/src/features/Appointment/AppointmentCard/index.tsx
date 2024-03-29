import { Badge, Box, Button, Divider, HStack, Icon, Stack, Text } from "@chakra-ui/react";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";
import { ISessionModel } from "../AppointmentList/appointment-list.interface";
import dayjs from "dayjs";
import { MdOutlineAccessTime } from "react-icons/md";
import { IoMdCalendar } from "react-icons/io";
import useAuthUser from "@hooks/general/useAuthUser";
import { useMeetingState } from "@hooks/general/useMeetingState";
import { useCountdown } from "@hooks/general/useCountdown";
import i18n from "../../../i18n.ts";

const AppointmentCard = ({ session }: { session: ISessionModel }) => {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const { user } = useAuthUser();
  
  const { isMeetingOnGoing, isMeetingAboutToStart, isMeetingFinished, untilMeetingStarts } = useMeetingState(session?.startTime as unknown as string, session?.endTime as unknown as string);
  const [ minutes, seconds ] = useCountdown( untilMeetingStarts, isMeetingAboutToStart );
  
  return (
    <Stack
      key={session.sessionId}
      borderWidth="1px"
      style={{
        boxShadow: "2px 2px 8px 0 rgba(13, 21, 55, 0.12)",
      }}
      p={6}
      justifyContent="space-between"
      borderRadius="lg"
      overflow="hidden"
      bgColor="white"
    >
      <HStack>
        <Box p="6" w="100%">
          <Stack isInline alignItems="center">
            <Text
              fontSize="2xl"
              fontWeight="semibold"
              lineHeight="tight"
              isTruncated
            >
              {user.role === 0
                ? `${
                    session.physician.firstName
                      ? session.physician.firstName
                      : ""
                  } ${
                    session.physician.patronymic
                      ? session.physician.patronymic
                      : ""
                  } ${
                    session.physician.lastName ? session.physician.lastName : ""
                  }`
                : `${
                    session.patient.firstName ? session.patient.firstName : ""
                  } ${
                    session.patient.patronymic ? session.patient.patronymic : ""
                  } ${
                    session.patient.lastName ? session.patient.lastName : ""
                  }`}
            </Text>
            <Text display="flex" alignItems="center" gap={2}>
              {session.isDeleted && (
                <Badge
                  px="10px"
                  py="2px"
                  mb={"6px"}
                  borderRadius="full"
                  colorScheme="red"
                >
                  {t("Deleted")}
                </Badge>
              )}
              {session.isArchived && (
                <Badge
                  px="10px"
                  py="2px"
                  mb={"6px"}
                  borderRadius="full"
                  colorScheme="yellow"
                >
                  {t("Archived")}
                </Badge>
              )}
            </Text>
          </Stack>
          <Stack direction="row" align="center" justify="space-between" mt={2}>
            <Box>
              <Text display="flex" alignItems="center" gap={2}>
                <Icon as={MdOutlineAccessTime} w="20px" h="20px" />
                {t("ReceptionHour")}
              </Text>

              <Text pt={2}>{`${
                `${new Date(session?.startTime).toLocaleTimeString(i18n.language, { hour: 'numeric', minute: 'numeric' })}`
              } - ${`${new Date(session?.endTime).toLocaleTimeString(i18n.language, { hour: 'numeric', minute: 'numeric' })}`}`}</Text>
            </Box>
            <Box>
              <Text display="flex" alignItems="center" gap={2}>
                <Icon as={IoMdCalendar} w="20px" h="20px" />
                {t("ReceptionDate")}
              </Text>

              <Text pt={2}>{`${dayjs(session?.startTime).format(
                "DD-MM-YYYY"
              )}`}</Text>
            </Box>
          </Stack>
          <Divider my={2} />
          <Stack direction="row" align="center" justify="space-between" mt={2}>
            <Text>{`${t("CurrentPainScale")}: ${
              session.currentPainScale
            }`}</Text>
            <Text>{`${t("AveragePainScaleLastMonth")}: ${
              session.averagePainScaleLastMonth
            }`}</Text>
          </Stack>
          <Stack direction="row" align="center" justify="space-between" mt={2}>
            <Text></Text>
            <Text>{`${t("HighestPainScaleLastMonth")}: ${
              session.highestPainScaleLastMonth
            }`}</Text>
          </Stack>
        </Box>
      </HStack>
      <Box />
      <HStack justifyContent="end" w="100%">
        <HStack gap="20px">
          <Button
            variant="outline"
            onClick={() => navigate(`/appointments/${session.sessionId}`)}
          >
            {t("View")}
          </Button>
        </HStack>
        {!session.isArchived && (
          <HStack gap="20px">
            <Button isDisabled={!isMeetingOnGoing} onClick={ () => navigate( `/appointments/${ session.sessionId }/call` ) }>
              { !isMeetingFinished && isMeetingAboutToStart && !isMeetingOnGoing && `${ minutes }:${ seconds }` } {t("Call")}
            </Button>
          </HStack>
        )}
      </HStack>
    </Stack>
  );
};

export default AppointmentCard;
