import {
  Accordion,
  AccordionItem,
  AccordionButton,
  AccordionPanel,
  AccordionIcon,
  Box,
  Button,
  FormControl,
  FormLabel,
  Select,
  Avatar,
  Textarea,
} from "@chakra-ui/react";
import { Controller } from "react-hook-form";
import { CustomContainer } from "@components/Container";
import { CustomPaper } from "@components/Paper";
import useAppointmentFormController from "./appointment-form.controller";
import { Preloader } from "@components/preloader/Preloader";

import "./index.scss";
import { useTranslation } from "react-i18next";
import dayjs from "dayjs";
import { formatUTC } from "@utils/formatUTC";

const AppointmentForm = () => {
  const { t } = useTranslation();
  const { availableTimetable, createSessionObj, form, painScale } =
    useAppointmentFormController();
  const {
    control,
    watch,
    handleSubmit,
    activeIndex,
    onSubmit,
    handleDateChange,
    handleTimeSlotChange,
    handleDescriptionChange,
    handlePainScaleChange,
    setActiveIndex,
  } = form;

  if (availableTimetable.isLoading) {
    return <Preloader size="xl" />;
  }

  if (
    !availableTimetable.data?.availableSessions ||
    availableTimetable.isError
  ) {
    return <div>Error loading data...</div>;
  }

  const availableDates = Object.keys(availableTimetable.data.availableSessions);

  return (
    <CustomContainer>
      <CustomPaper>
        <Box
          as="form"
          onSubmit={handleSubmit(onSubmit)}
          className="AppointmentForm"
        >
          <Box className="AppointmentForm__doctor-info">
            <Avatar src={availableTimetable.data.physician.photoUrl} />
            <Box>
              <p className="AppointmentForm__doctor-name">{`${availableTimetable.data.physician.firstName} ${availableTimetable.data.physician.patronymic ? availableTimetable.data.physician.patronymic : ""} ${availableTimetable.data.physician.lastName}`}</p>
              <p className="AppointmentForm__text">Санітаролог</p>
            </Box>
          </Box>
          <Accordion allowMultiple index={activeIndex}>
            <AccordionItem>
              <AccordionButton onClick={() => setActiveIndex(0)}>
                <Box flex="1" textAlign="left">
                  <p className="AppointmentForm__text">{t("SelectDate")}</p>
                </Box>
                <AccordionIcon />
              </AccordionButton>
              <AccordionPanel pb={4}>
                <FormControl isRequired>
                  <FormLabel htmlFor="date">{t("Date")}</FormLabel>
                  <Controller
                    name="date"
                    control={control}
                    render={({ field }) => (
                      <Select
                        id="date"
                        placeholder={t("SelectDate")}
                        colorScheme="orange"
                        {...field}
                        onChange={(e) => handleDateChange(e.target.value)}
                      >
                        {availableDates.map((date) => (
                          <option key={date} value={date}>
                            {date}
                          </option>
                        ))}
                      </Select>
                    )}
                  />
                </FormControl>
              </AccordionPanel>
            </AccordionItem>

            <AccordionItem isDisabled={!watch("date")}>
              <AccordionButton onClick={() => setActiveIndex(1)}>
                <Box flex="1" textAlign="left">
                  <p className="AppointmentForm__text">{t("SelectTimeSlot")}</p>
                </Box>
                <AccordionIcon />
              </AccordionButton>
              <AccordionPanel pb={4}>
                <FormControl isRequired>
                  <FormLabel htmlFor="timeSlot">{t("TimeSlot")}</FormLabel>
                  <Controller
                    name="timeSlot"
                    control={control}
                    render={({ field }) => (
                      <Select
                        id="timeSlot"
                        placeholder={t("SelectTimeSlot")}
                        {...field}
                        onChange={(e) => handleTimeSlotChange(e.target.value)}
                      >
                        {(availableTimetable.data as any).availableSessions[
                          watch("date")
                        ]?.map((slot: any, index: number) => (
                          <option key={index} value={JSON.stringify(slot)}>
                            {`${new Date(
                              formatUTC(slot.startTime)
                            ).toLocaleTimeString()} - ${new Date(
                              formatUTC(slot.endTime)
                            ).toLocaleTimeString()}`}
                          </option>
                        ))}
                      </Select>
                    )}
                  />
                </FormControl>
              </AccordionPanel>
            </AccordionItem>

            <AccordionItem isDisabled={!watch("timeSlot")}>
              <AccordionButton onClick={() => setActiveIndex(2)}>
                <Box flex="1" textAlign="left">
                  <p className="AppointmentForm__text">
                    {t("ProblemDesctiption")}
                  </p>
                </Box>
                <AccordionIcon />
              </AccordionButton>
              <AccordionPanel pb={4}>
                <Box className="AppointmentForm__pain-scale">
                  <FormControl isRequired>
                    <FormLabel htmlFor="currentPainScale">
                      {t("CurrentPainScale")}
                    </FormLabel>
                    <Controller
                      name="currentPainScale"
                      control={control}
                      render={({ field }) => (
                        <Select
                          id="currentPainScale"
                          {...field}
                          onChange={(e) => handlePainScaleChange("currentPainScale", +e.target.value)}
                        >
                          {painScale.map((value: number) => (
                            <option key={value} value={value}>
                              {value}
                            </option>
                          ))}
                        </Select>
                      )}
                    />
                  </FormControl>
                  <FormControl isRequired>
                    <FormLabel htmlFor="averagePainScaleLastMonth">
                      {t("AveragePainScaleLastMonth")}
                    </FormLabel>
                    <Controller
                      name="averagePainScaleLastMonth"
                      control={control}
                      render={({ field }) => (
                        <Select
                          id="averagePainScaleLastMonth"
                          {...field}
                          onChange={(e) => handlePainScaleChange("averagePainScaleLastMonth", +e.target.value)}
                        >
                          {painScale.map((value: number) => (
                            <option key={value} value={value}>
                              {value}
                            </option>
                          ))}
                        </Select>
                      )}
                    />
                  </FormControl>
                  <FormControl isRequired>
                    <FormLabel htmlFor="highestPainScaleLastMonth">
                      {t("HighestPainScaleLastMonth")}
                    </FormLabel>
                    <Controller
                      name="highestPainScaleLastMonth"
                      control={control}
                      render={({ field }) => (
                        <Select
                          id="highestPainScaleLastMonth"
                          {...field}
                          onChange={(e) => handlePainScaleChange("highestPainScaleLastMonth", +e.target.value)}
                        >
                          {painScale.map((value: number) => (
                            <option key={value} value={value}>
                              {value}
                            </option>
                          ))}
                        </Select>
                      )}
                    />
                  </FormControl>
                </Box>
                <FormControl isRequired>
                  <Controller
                    name="description"
                    control={control}
                    render={({ field }) => (
                      <Textarea
                        id="description"
                        placeholder={t("ProblemDesctiption")}
                        value={field.value}
                        onChange={(e) => handleDescriptionChange(e.target.value)}
                      />
                    )}
                  />
                </FormControl>
              </AccordionPanel>
            </AccordionItem>

            <AccordionItem isDisabled={!watch("description")}>
              <AccordionButton onClick={() => setActiveIndex(3)}>
                <Box flex="1" textAlign="left">
                  <p className="AppointmentForm__text">
                    {t("ConfirmAppointment")}
                  </p>
                </Box>
                <AccordionIcon />
              </AccordionButton>
              <AccordionPanel pb={4}>
                <Box>
                  <p className="AppointmentForm__text">
                    {t("Date")}: {watch("date")}
                  </p>
                  <p className="AppointmentForm__text">
                    {t("TimeSlot")}:{" "}
                    {watch("timeSlot") &&
                      dayjs(JSON.parse(watch("timeSlot")).startTime).format(
                        "HH:mm:ss"
                      )}
                    -
                    {watch("timeSlot") &&
                      dayjs(JSON.parse(watch("timeSlot")).endTime).format(
                        "HH:mm:ss"
                      )}
                  </p>
                  <p className="AppointmentForm__text">
                    {t("CurrentPainScale")}: {watch("currentPainScale")}
                  </p>
                  <p className="AppointmentForm__text">
                    {t("AveragePainScaleLastMonth")}: {watch("averagePainScaleLastMonth")}
                  </p>
                  <p className="AppointmentForm__text">
                    {t("HighestPainScaleLastMonth")}: {watch("highestPainScaleLastMonth")}
                  </p>
                  <p className="AppointmentForm__text">
                    {t("ProblemDesctiption")}: {watch("description")}
                  </p>
                </Box>
                <Button
                  isLoading={createSessionObj.isLoading}
                  loadingText={t("Submitting")}
                  colorScheme="orange"
                  mt={4}
                  type="submit"
                >
                  {t("Confirm")}
                </Button>
              </AccordionPanel>
            </AccordionItem>
          </Accordion>
        </Box>
      </CustomPaper>
    </CustomContainer>
  );
};

export default AppointmentForm;
