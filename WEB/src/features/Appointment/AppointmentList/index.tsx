import { Preloader } from "@components/preloader/Preloader";
import useAppointmentListController from "./appointment-list.controller";
import { useMemo } from "react";
import { CustomContainer } from "@components/Container";
import { Grid } from "@chakra-ui/react";
import { CustomPaper } from "@components/Paper";
import NoContent from "@components/NoContent";
import { useTranslation } from "react-i18next";
import AppointmentCard from "../AppointmentCard";
import AppointmentListFilters from "../AppointmentListFilters";

export const AppointmentList = () => {
  const { t } = useTranslation();
  const { sessionList } = useAppointmentListController();
  const { data } = sessionList;

  const mappedData = useMemo(
    () => data?.map((session) => <AppointmentCard session={session} />),
    [data]
  );

  if (sessionList.isLoading) {
    return <Preloader size="xl" />;
  }

  return (
    <CustomContainer>
      <CustomPaper>
        <AppointmentListFilters />
      </CustomPaper>
      {!data.length ? (
        <CustomPaper>
          <NoContent content={t("NoRecordsYet")} />
        </CustomPaper>
      ) : (
        <>
          <Grid className="grid" gap={6}>
            {mappedData}
          </Grid>
        </>
      )}
    </CustomContainer>
  );
};
