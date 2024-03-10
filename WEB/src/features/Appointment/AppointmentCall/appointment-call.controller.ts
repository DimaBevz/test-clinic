import { useEffect, useRef, useState } from "react";
import { useParams } from "react-router-dom";
import { useDyteClient } from "@dytesdk/react-web-core";
import CustomControlbarButton from "@dytesdk/ui-kit-addons/custom-controlbar-button";
import { registerAddons } from "@dytesdk/react-ui-kit";
import { useTranslation } from "react-i18next";
import { useAppointmentInfo } from "@hooks/appointment/useAppointmentInfo";
import useAuthUser from "@hooks/general/useAuthUser.ts";
import { useTestsResultList } from "@hooks/tests/useTestsResultList.ts";
import { useToast } from "@hooks/general/useToast.ts";
import { useGetCallTokenMutation } from "@api/appointment-call.service.ts";

interface useAppointmentCallControllerProps {
  onOpen: () => void;
}
function useAppointmentCallController({
  onOpen,
}: useAppointmentCallControllerProps) {
  const { t } = useTranslation();
  const { id } = useParams();
  const { isDoctor } = useAuthUser();
  const { sessionInfoData } = useAppointmentInfo({
    appointmentId: id as string,
  });
  const { testsListResult, isTestsListResultLoading } = useTestsResultList({
    patientId: sessionInfoData?.patient.id,
  });
  const [config, setConfig] = useState<any>();
  const [meeting, initMeeting] = useDyteClient();
  const btnRef = useRef();
  const testsBtnRef = useRef();
  const [openDrawer, setOpenDrawer] = useState<boolean>(false);
  const [openTestDrawer, setOpenTestDrawer] = useState<boolean>(false);
  const { errorToast } = useToast();

  const handleOpenDrawer = () => {
    setOpenDrawer(true);
  };

  const handleCloseDrawer = () => {
    setOpenDrawer(false);
  };

  const handleToggleTestDrawer = () => {
    setOpenTestDrawer((prev) => !prev);
  };

  useEffect(() => {
    if (meeting) {
      const myButton = new CustomControlbarButton({
        position: "center",
        label: t("Recommendations"),
        icon: `<svg stroke="currentColor" fill="currentColor" stroke-width="0" viewBox="0 0 1024 1024" height="150px" width="150px" xmlns="http://www.w3.org/2000/svg"><path d="M799.344 960.288h-736v-800h449.6l64.704-62.336-1.664-1.664H63.344c-35.344 0-64 28.656-64 64v800c0 35.344 28.656 64 64 64h736c35.344 0 64-28.656 64-64V491.632l-64 61.088v407.568zM974.224 41.44C945.344 13.76 913.473-.273 879.473-.273c-53.216 0-92.032 34.368-102.592 44.897-14.976 14.784-439.168 438.353-439.168 438.353-3.328 3.391-5.76 7.535-7.008 12.143-11.488 42.448-69.072 230.992-69.648 232.864-2.976 9.664-.32 20.193 6.8 27.217a26.641 26.641 0 0 0 18.913 7.84c2.752 0 5.52-.4 8.239-1.248 1.952-.656 196.496-63.569 228.512-73.12 4.224-1.248 8.048-3.536 11.216-6.624 20.208-19.936 410.112-403.792 441.664-436.384 32.624-33.664 48.847-68.657 48.223-104.097-.591-35.008-17.616-68.704-50.4-100.128zm-43.791 159.679c-17.808 18.368-157.249 156.16-414.449 409.536l-19.68 19.408c-29.488 9.12-100.097 31.808-153.473 49.024 17.184-56.752 37.808-125.312 47.008-157.743C444.8 466.464 808.223 103.6 822.03 89.968c2.689-2.689 27.217-26.257 57.44-26.257 17.153 0 33.681 7.824 50.465 23.92 20.065 19.248 30.4 37.744 30.689 55.024.32 17.792-9.84 37.456-30.191 58.464z"></path></svg>`,
        onClick: handleOpenDrawer,
      });
      const myTestButton = new CustomControlbarButton({
        position: "center",
        label: t("Tests"),
        icon: `<svg stroke="currentColor" fill="currentColor" stroke-width="0" viewBox="0 0 24 24" focusable="false" class="chakra-icon css-1kxx4bz" height="1em" width="1em" xmlns="http://www.w3.org/2000/svg"><path fill="none" d="M0 0h24v24H0z"></path><path d="M11 7H2v2h9V7zm0 8H2v2h9v-2zm5.34-4L12.8 7.46l1.41-1.41 2.12 2.12 4.24-4.24L22 5.34 16.34 11zm0 8l-3.54-3.54 1.41-1.41 2.12 2.12 4.24-4.24L22 13.34 16.34 19z"></path></svg>`,
        onClick: handleToggleTestDrawer,
      });
      const newConfig = registerAddons(
        isDoctor ? [myTestButton, myButton] : [],
        meeting
      );
      setConfig(newConfig);
    }
  }, [meeting]);

  meeting?.self?.once("roomLeft", async (e) => {
    if (e.state === "left") {
      try {
        meeting?.self?.audioTrack?.stop();
        meeting?.self?.videoTrack?.stop();
        onOpen();
      } catch (error: any) {
        errorToast(error.data.title);
      }
    }
  });
  const [getCallToken, { data: getCallTokenData }] = useGetCallTokenMutation();

  useEffect(() => {
    if (sessionInfoData?.id) {
      getCallToken(sessionInfoData?.id);
    }
  }, [sessionInfoData?.id]);

  useEffect(() => {
    if (getCallTokenData) {
      initMeeting({
        authToken: getCallTokenData,
      });
    }
  }, [getCallTokenData]);

  return {
    config,
    meeting,
    doctorId: sessionInfoData?.physician.id,
    testsListResult,
    isTestsListResultLoading,
    drawer: {
      btnRef,
      handleCloseDrawer,
      openDrawer,
      handleToggleTestDrawer,
      openTestDrawer,
      testsBtnRef,
    },
  };
}

export default useAppointmentCallController;
