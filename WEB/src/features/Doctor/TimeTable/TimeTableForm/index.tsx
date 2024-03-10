import { Controller } from "react-hook-form";
import { useTranslation } from "react-i18next";

import { Box, Button, HStack, Icon, Switch, Text, Tooltip } from "@chakra-ui/react";

import { Paper } from "@components/Paper";
import { Container } from "@components/Container";
import { Accordion } from "@components/Accordion";
import DatePicker from "@components/Datepicker";
import { Preloader } from "@components/preloader/Preloader";

import useTimeTableFormController from "./time-table-form.controller";

import { IoTrashOutline } from "react-icons/io5";
import { MdInfoOutline } from "react-icons/md";

import "./index.scss";

import i18n from "../../../../i18n.ts";

export const TimeTableForm = () => {
  const {
    control,
    errors,
    handleSubmit,
    sessionTemplates,
    onSubmit,
    addTimeSlot,
    removeTimeSlot,
    createTimetableTemplate,
    updateTimetableTemplate,
    deleteTimetable,
    getTimetable,
    changeAccordionState,
  } = useTimeTableFormController();
  const { t } = useTranslation();

  if (deleteTimetable.isLoading || getTimetable.isLoading) {
    return <Preloader size="xl" />;
  }

  const ukDateFormat = "HH:mm";
  const enDateFormat = "h:mm aa";

  return (
    <Container>
      <Paper>
        <Box
          className="TimeTableForm"
          as="form"
          onSubmit={handleSubmit(onSubmit)}
        >
          <Box className="TimeTableForm__header">
            <Box className="TimeTableForm__dates">
              <Controller
                control={control}
                name="startDate"
                render={({ field }) => (
                  <DatePicker
                    htmlFor="startDate"
                    label={`${t("StartDate")}`}
                    dateFormat="dd-MM-yyyy"
                    selectedDate={field.value}
                    onChange={field.onChange}
                    error={errors.startDate?.message}
                  />
                )}
              />
              <Controller
                control={control}
                name="endDate"
                render={({ field }) => (
                  <DatePicker
                    htmlFor="endDate"
                    label={`${t("EndDate")}`}
                    dateFormat="dd-MM-yyyy"
                    selectedDate={field.value}
                    onChange={field.onChange}
                    error={errors.endDate?.message}
                  />
                )}
              />
            </Box>
            <Box className="TimeTableForm__info">
              <Tooltip
                hasArrow
                label={
                  <p className="TimeTableForm__info-text-container">
                    <p className="TimeTableForm__info-text">
                      {t("ScheduleInfo")}
                    </p>
                    <p className="TimeTableForm__info-text">
                      {t("ScheduleInfoFirstRule")}
                    </p>
                    <p className="TimeTableForm__info-text">
                      {t("ScheduleInfoSecondRule")}
                    </p>
                    <p className="TimeTableForm__info-text">
                      {t("ScheduleInfoThirdRule")}
                    </p>
                  </p>
                }
                fontSize="10px"
                placement="bottom-start"
                bg="gray.300"
                color="black"
              >
                <Button variant="ghost">
                  <Icon as={MdInfoOutline} w={6} h={6} />
                </Button>
              </Tooltip>
            </Box>
          </Box>

          {sessionTemplates.fields.map((day, index) => (
            <Accordion
              key={`${day.dayLabel}_${index}`}
              label={day.dayLabel}
              setIsActive={() => {
                changeAccordionState(index);
              }}
              isActive={day.isOpen}
              isDisabled={!day.isActive}
              additionalProps={
                <Controller
                  control={control}
                  name={`sessionTemplates.${index}.isActive`}
                  render={({ field }) => (
                    <Switch
                      isChecked={day.isActive}
                      onChange={field.onChange}
                    />
                  )}
                />
              }
            >
              <Box className="TimeTableForm__accordion-content">
                <Text className="TimeTableForm__description">
                  {t("SessionDescription")}
                </Text>
                <Text className="TimeTableForm__description TimeTableForm__sub-description">
                  {t("SubSessionDescription")}
                </Text>
                {sessionTemplates.fields[index].timeSlots.map(
                  (timeSlot, slotIndex) => {
                    return (
                      <Box
                        className="TimeTableForm__time-slots-container"
                        key={`${timeSlot.startTime}_${slotIndex}`}
                      >
                        <Box className="TimeTableForm__time-slots">
                          <Controller
                            control={control}
                            name={`sessionTemplates.${index}.timeSlots.${slotIndex}.startTime`}
                            render={({ field }) => (
                              <DatePicker
                                dateFormat={i18n.language === "en" ? enDateFormat : ukDateFormat}
                                htmlFor={`sessionTemplates.${index}.timeSlots.${slotIndex}.startTime`}
                                label={`${t("StartTime")}`}
                                selectedDate={
                                  field.value
                                    ? new Date(field.value as any)
                                    : undefined
                                }
                                onChange={field.onChange}
                                error={errors.endDate?.message}
                                showTimeSelect
                                showTimeSelectOnly
                                timeCaption="Time"
                                timeIntervals={15}
                              />
                            )}
                          />
                          <Controller
                            control={control}
                            name={`sessionTemplates.${index}.timeSlots.${slotIndex}.endTime`}
                            render={({ field }) => (
                              <DatePicker
                                dateFormat={i18n.language === "en" ? enDateFormat : ukDateFormat}
                                htmlFor={`sessionTemplates.${index}.timeSlots.${slotIndex}.endTime`}
                                label={`${t("EndTime")}`}
                                selectedDate={
                                  field.value
                                    ? new Date(field.value as any)
                                    : undefined
                                }
                                onChange={field.onChange}
                                error={errors.endDate?.message}
                                showTimeSelect
                                showTimeSelectOnly
                                timeCaption="Time"
                                timeIntervals={15}
                              />
                            )}
                          />
                        </Box>

                        {sessionTemplates.fields[index].timeSlots.length >
                          1 && (
                          <Tooltip
                            hasArrow
                            label={t("RemoveTimeSlot")}
                            bg="gray.300"
                            color="black"
                          >
                            <Button
                              variant="outline"
                              ml={5}
                              onClick={() => removeTimeSlot(index, slotIndex)}
                            >
                              <Icon as={IoTrashOutline} />
                            </Button>
                          </Tooltip>
                        )}
                      </Box>
                    );
                  }
                )}
                <HStack justifyContent="flex-end">
                  <Button
                    maxWidth="200px"
                    onClick={() => addTimeSlot(index)}
                  >
                    {t("AddTimeSlot")}
                  </Button>
                </HStack>
              </Box>
            </Accordion>
          ))}
          <Box className="TimeTableForm__actions">
            <Button
              type="submit"
              isLoading={
                createTimetableTemplate.isLoading ||
                updateTimetableTemplate.isLoading
              }
              loadingText={t("Submitting")}
            >
              {t("Save")}
            </Button>
          </Box>
        </Box>
      </Paper>
    </Container>
  );
};
