import { DyteMeeting } from "@dytesdk/react-ui-kit";
import { AppointmentCallForm } from "../AppointmentCallForm";
import {
  Box,
  Divider,
  Heading,
  Stack,
  Textarea,
  useDisclosure,
} from "@chakra-ui/react";
import { Drawer } from "@components/Drawer";
import { useContext } from "react";
import { PhysicianAppointmentFormContext } from "@components/context/PhysicianAppointmentForm";
import { useTranslation } from "react-i18next";
import { Preloader } from "@components/preloader/Preloader.tsx";
import { ITestsRes } from "@features/Tests/tests.interface.ts";
import useAppointmentCallController from "@features/Appointment/AppointmentCall/appointment-call.controller.ts";

export const AppointmentCall = () => {
  const { isOpen, onOpen, onClose } = useDisclosure();
  const {
    meeting,
    testsListResult,
    isTestsListResultLoading,
    doctorId,
    config,
    drawer,
  } = useAppointmentCallController({
    onOpen,
  });
  const { t } = useTranslation();
  const {
    btnRef,
    openTestDrawer,
    testsBtnRef,
    handleToggleTestDrawer,
    handleCloseDrawer,
    openDrawer,
  } = drawer;

  if (!meeting) {
    return   <Preloader size="xl" />
  }

  return (
    <>
      {meeting && config && (
        <DyteMeeting
          mode="fill"
          meeting={meeting!}
          config={config}
        />
      )}
      <AppointmentCallForm
        doctorId={doctorId as string}
        isOpen={isOpen}
        onClose={onClose}
      />
      <Drawer
        refProp={btnRef}
        title={t("WriteRecommendations")}
        placement="right"
        isOpen={openDrawer}
        handleSubmit={handleCloseDrawer}
        handleCloseDrawer={handleCloseDrawer}
        content={<AppointmentRecomendations />}
      />
      <Drawer
        refProp={testsBtnRef}
        title={t("Tests")}
        placement="right"
        isOpen={openTestDrawer}
        handleCloseDrawer={handleToggleTestDrawer}
        content={
          <AppointmentTest
            isTestsListResultLoading={isTestsListResultLoading}
            testsListResult={testsListResult}
          />
        }
      />
    </>
  );
};

export function AppointmentRecomendations() {
  const { t } = useTranslation();
  const { value, setValue } = useContext(PhysicianAppointmentFormContext);

  return (
    <Textarea
      placeholder={t("WriteRecommendations")}
      color="#000"
      value={value}
      onChange={(e) => setValue(e.target.value)}
    />
  );
}

export function AppointmentTest({
  testsListResult,
  isTestsListResultLoading,
}: {
  isTestsListResultLoading: boolean;
  testsListResult: ITestsRes[] | undefined;
}) {
  const { t } = useTranslation();

  return (
    <Stack w="100%">
      {isTestsListResultLoading ? (
        <Preloader size="xl" />
      ) : (
        <Box pt={2} w="100%" className="AppointmentInfo">
          <Heading as={"h4"} size={"md"}>
            {t("InformationTestsPassed")}
          </Heading>
          <Box className="AppointmentInfo__test-block">
            {testsListResult?.map((test) => (
              <Box key={test.id} pt={3}>
                <Box className="AppointmentInfo__test-card-header">
                  <Box>{test.name}</Box>
                  <Box>{`${t("Score")}: ${test.totalScore}`}</Box>
                </Box>
                <Box className="AppointmentInfo__test-card-content">
                  <Box className="AppointmentInfo__conclusion">
                    <Box className="AppointmentInfo__conclusion-label">
                      {t("Conclusion")}:
                    </Box>
                    <Box>{test.verdict}</Box>
                  </Box>
                </Box>
                <Divider />
              </Box>
            ))}
          </Box>
        </Box>
      )}
    </Stack>
  );
}
