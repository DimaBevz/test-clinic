import { Box, Icon } from "@chakra-ui/react";
import { useTranslation } from "react-i18next";
import { useLocation, useNavigate } from "react-router-dom";

import { FaHandHoldingMedical, FaRegListAlt } from "react-icons/fa";
import { MdChecklistRtl, MdHelpOutline, MdOutlineBugReport } from "react-icons/md";
import { GrSchedule } from "react-icons/gr";

import { useAppSelector } from "@store/index.ts";
import authSelectors from "../../../store/auth/auth.selectors.ts";

import "./index.scss";

export const Navigation = () => {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const location = useLocation();
  const user = useAppSelector(authSelectors.getAuthUser);
  const isSchedule = location.pathname === "/schedule";
  const isHelp = location.pathname === "/help";
  const isBug = location.pathname === "/report-bug";
  const isAppointments = location.pathname.includes("appointments");
  const isDoctors = location.pathname.includes("doctors");
  const isTests = location.pathname.includes("tests");

  return (
    <Box className="Navigation">
      <Box
        onClick={() => navigate("/doctors")}
        className={`Navigation__item ${isDoctors ? "active" : ""}`}
      >
        <Icon as={FaHandHoldingMedical} w={4} h={4} />
        {t("Doctors")}
      </Box>
      {user?.role ? (
        <Box
          className={`Navigation__item ${isSchedule ? "active" : ""}`}
          onClick={() => navigate("/schedule")}
        >
          <Icon as={GrSchedule} w={4} h={4} />
          {t("Schedule")}
        </Box>
      ) : (
        <></>
      )}
      <Box
        className={`Navigation__item ${isAppointments ? "active" : ""}`}
        onClick={() => navigate("/appointments")}
      >
        <Icon as={FaRegListAlt} w={4} h={4} />
        {t("Records")}
      </Box>
      {user?.role === 0 && (
        <Box
          className={`Navigation__item ${isTests ? "active" : ""}`}
          onClick={() => navigate("/tests")}
        >
          <Icon as={MdChecklistRtl} w={4} h={4} />
          {t("Tests")}
        </Box>
      )}
      <Box
        onClick={() => navigate("/help")}
        className={`Navigation__item ${isHelp ? "active" : ""}`}
      >
        <Icon as={MdHelpOutline} w={4} h={4} />
        {t("Help")}
      </Box>
      <Box
        onClick={() => navigate("/report-bug")}
        className={`Navigation__item ${isBug ? "active" : ""}`}
      >
        <Icon as={MdOutlineBugReport} w={4} h={4} />
        {t("BugReport")}
      </Box>
    </Box>
  );
};
