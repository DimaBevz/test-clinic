import { Preloader } from "@components/preloader/Preloader";
import useAppointmentListController from "./appointment-list.controller";
import { useMemo } from "react";
import { Container } from "@components/Container";
import { Grid } from "@chakra-ui/react";
import { Paper } from "@components/Paper";
import NoContent from "@components/NoContent";
import { useTranslation } from "react-i18next";
import AppointmentCard from "../AppointmentCard";
import AppointmentListFilters from "../AppointmentListFilters";
import { Calendar } from "@features/Calendar";

export const AppointmentList = () => {
  const { t } = useTranslation();
  const { sessionList } = useAppointmentListController();
  const { data, isLoading, isListType } = sessionList;
  
  const mappedData = useMemo(
    () => data?.map((session) => <AppointmentCard session={session} />),
    [data]
  );

  if (isLoading) {
    return <Preloader size="xl" />;
  }

  return (
    <Container>
      <Paper>
        <AppointmentListFilters />
      </Paper>
      {!data.length && isListType ? (
        <Paper>
          <NoContent content={t("NoRecordsYetAppointmentsList")} />
        </Paper>
      ) : (
        <>
          {isListType ? (
            <Grid className="grid" gap={6}>
              {mappedData}
            </Grid>
          ) : (
            <Paper>
              <Calendar data={data} />
            </Paper>
          )}
        </>
      )}
    </Container>
  );
};
