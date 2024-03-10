import { Box, HStack, Icon, Text } from "@chakra-ui/react";
import { useTranslation } from "react-i18next";
import { MdOutlineAccessTime } from "react-icons/md";
import i18n from "../../../i18n.ts";

import "../AppointmentInfo/index.scss";
import { ISessionModel } from "../AppointmentList/appointment-list.interface";

interface IAppointmentModalInfoProps {
    sessionInfo: ISessionModel;
}

export const AppointmentModalInfo = ({ sessionInfo }: IAppointmentModalInfoProps) => {
    const { t } = useTranslation();

    return (
        <>
            <Text>{`${sessionInfo.patient.firstName} ${sessionInfo.patient.lastName}`}</Text>
            <HStack pt={3} justifyContent="space-between">
                <Box>
                    <Text className="UserBlock__title">
                        <Text display="flex" alignItems="center" gap={2}>
                            <Icon as={MdOutlineAccessTime} w="20px" h="20px" />
                            {t("ReceptionHour")}
                        </Text>
                    </Text>
                    <Text pt={2}>{`${new Date(sessionInfo?.startTime).toLocaleTimeString(i18n.language, {
                        hour: "numeric",
                        minute: "numeric",
                    })} - ${new Date(sessionInfo?.endTime).toLocaleTimeString(i18n.language, { hour: "numeric", minute: "numeric" })}
		    `}</Text>
                </Box>
                <Box>
                    <Text className="UserBlock__title">{t("AppointmentStatus")}</Text>
                    <Text pt={2}>
                        {sessionInfo?.isDeleted && <>{t("Deleted")}</>}
                        {sessionInfo?.isArchived && <>{t("Archived")}</>}
                        {!sessionInfo?.isArchived && !sessionInfo?.isDeleted && <>{t("Active")}</>}
                    </Text>
                </Box>
            </HStack>
            <HStack justifyContent="space-between">
                <Box>
                    <Text className="UserBlock__title">{t("HighestPainScaleLastMonth")}</Text>
                    <Text pt={2}>{sessionInfo?.highestPainScaleLastMonth}</Text>
                </Box>
                <Box>
                    <Text className="UserBlock__title">{t("AveragePainScaleLastMonth")}</Text>
                    <Text pt={2}>{sessionInfo?.averagePainScaleLastMonth}</Text>
                </Box>
            </HStack>
            <HStack justifyContent="space-between">
                <Box>
                    <Text className="UserBlock__title">{t("CurrentPainScale")}</Text>
                    <Text pt={2}>{sessionInfo?.currentPainScale}</Text>
                </Box>
            </HStack>
        </>
    );
};
