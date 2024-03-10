import {
  Accordion,
  
  AccordionButton,
  AccordionItem,
  AccordionPanel, Badge,
  Box,
  Button,
  FormControl,
  FormLabel,
  Grid,
  HStack,
  Icon,
  Image,
  Select,
  Stack,
  Textarea,
  Text,
} from "@chakra-ui/react";

import { Controller } from "react-hook-form";
import useAppointmentFormController from "./appointment-form.controller";
import { Preloader } from "@components/preloader/Preloader";

import "./index.scss";
import { useTranslation } from "react-i18next";
import dayjs from "dayjs";

import { RxAvatar } from "react-icons/rx";
import { CheckIcon, CloseIcon } from "@chakra-ui/icons";
import { Container } from "@components/Container";
import { Paper } from "@components/Paper";

import i18n from "../../../i18n.ts";

const AppointmentForm = () => {
  const ukDateFormat = "HH:mm";
  const enDateFormat = "h:mm A";

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
    <Container>
      <Paper>
        <Box
          as="form"
          onSubmit={handleSubmit(onSubmit)}
          className="AppointmentForm"
        >
          <Box className="AppointmentForm__doctor-info">
            { availableTimetable.data.physician.photoUrl ? (
              <Image
                borderRadius="full"
                verticalAlign="middle"
                w="60px"
                h="60px"
                objectFit="cover"
                src={ availableTimetable.data.physician.photoUrl }
                alt={ t( "Doctor`s avatar" ) }
              />
            ) : (
              <Icon as={ RxAvatar } w="60px" h="60px" color="#ECC94B"/>
            ) }
            <Box>
              <Text className="AppointmentForm__doctor-name">
                { `
                ${ availableTimetable.data.physician.firstName }
                ${ availableTimetable.data.physician.patronymic ? availableTimetable.data.physician.patronymic : "" }
                ${ availableTimetable.data.physician.lastName }
              ` }
              </Text>
              <Stack
                direction="row"
                flexWrap="wrap"
                alignItems="baseline"
                spacing={2}
              >
                {availableTimetable.data.physician.positions.map((position, index) => (
                  <Badge
                    key={index}
                    px="2"
                    py="1"
                    borderRadius="full"
                    colorScheme="yellow"
                    flexShrink={1}
                    isTruncated
                    maxWidth="100%"
                  >
                    {position.specialty}
                  </Badge>
                ))}
              </Stack>
            </Box>
          </Box>
          <Accordion allowMultiple index={activeIndex}>
            <AccordionItem>
              <AccordionButton onClick={() => setActiveIndex(0)}>
                <Box flex="1" textAlign="left">
                  <Text className="AppointmentForm__text">{ t( "SelectDate" ) }</Text>
                </Box>
                { watch( "date" ) ? (
                  <CheckIcon color="green" fontSize="12px"/>
                ) : (
                  <CloseIcon color="red" fontSize="12px"/>
                ) }
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
                        colorScheme="yellow"
                        {...field}
                        onChange={(e) => handleDateChange(e.target.value)}
                      >
                        {availableDates.map((date: string) => (
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
                  <Text className="AppointmentForm__text">{ t( "SelectTimeSlot" ) }</Text>
                </Box>
                { watch( "timeSlot" ) ? (
                  <CheckIcon color="green" fontSize="12px"/>
                ) : (
                  <CloseIcon color="red" fontSize="12px"/>
                ) }
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
                          ]?.map( ( slot: any, index: number ) => (
                          <option key={index} value={JSON.stringify(slot)}>
                            {`${new Date(slot.startTime
                            ).toLocaleTimeString(i18n.language, { hour: 'numeric', minute: 'numeric' })} - ${new Date(
                              slot.endTime
                            ).toLocaleTimeString(i18n.language, { hour: 'numeric', minute: 'numeric' })}`}
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
                  <Text className="AppointmentForm__text">
                    { t( "ProblemDescription" ) }
                  </Text>
                </Box>
                { watch( "description" ) ? (
                  <CheckIcon color="green" fontSize="12px"/>
                ) : (
                  <CloseIcon color="red" fontSize="12px"/>
                ) }
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
                          onChange={(e) =>
                            handlePainScaleChange(
                              "currentPainScale",
                              +e.target.value
                            )
                          }
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
                          onChange={(e) =>
                            handlePainScaleChange(
                              "averagePainScaleLastMonth",
                              +e.target.value
                            )
                          }
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
                          onChange={(e) =>
                            handlePainScaleChange(
                              "highestPainScaleLastMonth",
                              +e.target.value
                            )
                          }
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
                        placeholder={ t( "ProblemDescription" ) }
                        value={field.value}
                        onChange={(e) =>
                          handleDescriptionChange(e.target.value)
                        }
                      />
                    )}
                  />
                </FormControl>
              </AccordionPanel>
            </AccordionItem>
            
            <AccordionItem isDisabled={!watch("description")}  borderBottomWidth="0px !important">
              <AccordionButton onClick={() => setActiveIndex(3)}>
                <Box flex="1" textAlign="left">
                  <Text className="AppointmentForm__text">
                    {t("ConfirmAppointment")}
                  </Text>
                </Box>
                { watch( "description" ) ? (
                  <CheckIcon color="green" fontSize="12px"/>
                ) : (
                  <CloseIcon color="red" fontSize="12px"/>
                ) }
              </AccordionButton>
              <AccordionPanel pb={4} borderRadius="15px">
                <Stack gap="6px">
                  <Grid className="grid">
                    <HStack justifyContent="space-between" gap={2} maxW="450px">
                      <Text className="AppointmentForm__text">
                        { t( "Date" ) }:
                      </Text>
                      <Text className="AppointmentForm__text">
                        { watch( "date" ) }
                      </Text>
                    </HStack>
                    <HStack justifyContent="space-between" gap={2} maxW="450px">
                      <Text className="AppointmentForm__text">
                        { t( "TimeSlot" ) }:
                      </Text>
                      <Text className="AppointmentForm__text">
                        { watch( "timeSlot" ) &&
                          dayjs( JSON.parse( watch( "timeSlot" ) ).startTime ).format(
                            i18n.language === "en" ? enDateFormat : ukDateFormat,
                          ) }
                        -
                        { watch( "timeSlot" ) &&
                          dayjs( JSON.parse( watch( "timeSlot" ) ).endTime ).format(
                            i18n.language === "en" ? enDateFormat : ukDateFormat,
                          ) }
                      </Text>
                    </HStack>
                    <HStack justifyContent="space-between" gap={2} maxW="450px">
                      <Text className="AppointmentForm__text">
                        { t( "CurrentPainScale" ) }:
                      </Text>
                      <Text className="AppointmentForm__text">
                        { watch( "currentPainScale" ) }
                      </Text>
                    </HStack>
                    <HStack justifyContent="space-between" gap={2} maxW="450px">
                      <Text className="AppointmentForm__text">
                        { t( "AveragePainScaleLastMonth" ) }:
                      </Text>
                      <Text className="AppointmentForm__text">
                        { watch( "averagePainScaleLastMonth" ) }
                      </Text>
                    </HStack>
                  </Grid>
                  <HStack justifyContent="space-between" gap={2} maxW="450px">
                    <Text className="AppointmentForm__text">
                      {t("HighestPainScaleLastMonth")}:
                    </Text>
                    <Text className="AppointmentForm__text">
                      {watch("highestPainScaleLastMonth")}
                    </Text>
                  </HStack>
                  
                  <Text className="AppointmentForm__text">
                    { t( "ProblemDescription" ) }:
                  </Text>
                  <Text className="AppointmentForm__text">
                    { watch( "description" ) }
                  </Text>
                </Stack>
                <Button
                  isLoading={createSessionObj.isLoading}
                  loadingText={t("Submitting")}
                  mt={4}
                  type="submit"
                >
                  {t("Confirm")}
                </Button>
              </AccordionPanel>
            </AccordionItem>
          </Accordion>
        </Box>
      </Paper>
    </Container>
  );
};

export default AppointmentForm;
