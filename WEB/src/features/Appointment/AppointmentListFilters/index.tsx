import { Controller } from "react-hook-form";
import { useTranslation } from "react-i18next";

import { Box, Button, FormControl, FormLabel, Select } from "@chakra-ui/react";
import { DateRangePicker } from "@components/DateRangePicker";

import useAppointmentListFiltersController from "./appointments-list-filters.controller";

import "./index.scss";

const AppointmentListFilters: React.FC = () => {
  const { t } = useTranslation();
  const { sessionStatuses, onReset, onSubmit, form } =
    useAppointmentListFiltersController();
  const { control, handleSubmit } = form;

  return (
    <form className="AppointmentListFilters" onSubmit={handleSubmit(onSubmit)}>
      <Box className="AppointmentListFilters__filters">
          <Controller
            control={control}
            name="sessionDateRange"
            render={({ field }) => (
              <DateRangePicker
                label={t("SelectDateRange")}
                dateFormat="yyyy-MM-dd"
                value={field.value}
                onChange={field.onChange}
                size="lg"
                placeholder="yyyy-mm-dd ~ yyyy-mm-dd"
                defaultCalendarValue={[new Date(), new Date()]}
              />
            )}
          />
        <Box>
          <FormControl>
            <FormLabel
              className="AppointmentListFilters__select-label"
              htmlFor="sessionStatus"
            >
              {t("SelectStatus")}
            </FormLabel>
            <Controller
              name="sessionStatus"
              control={control}
              render={({ field }) => (
                <Select id="sessionStatus" {...field} onChange={field.onChange} minW={200} minH={43}>
                  {sessionStatuses.map((value: string) => (
                    <option key={value} value={value}>
                      {value}
                    </option>
                  ))}
                </Select>
              )}
            />
          </FormControl>
        </Box>
      </Box>
      <Box className="AppointmentListFilters__actions">
        <Button colorScheme="orange" type="submit">
          {t("ApplyFilters")}
        </Button>
        <Button colorScheme="orange" variant="outline" onClick={onReset}>
          {t("ClearFilters")}
        </Button>
      </Box>
    </form>
  );
};

export default AppointmentListFilters;
