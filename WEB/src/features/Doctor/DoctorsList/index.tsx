import { Button, FormControl, Grid } from "@chakra-ui/react";
import { Preloader } from "@components/preloader/Preloader";
import { Container } from "@components/Container";
import { Paper } from "@components/Paper";

import {
  AutoComplete,
  AutoCompleteInput,
  AutoCompleteItem,
  AutoCompleteList,
} from "@choc-ui/chakra-autocomplete";
import useDoctorsListController from "./doctors-list.controller";

import "./index.scss";

const DoctorsList = () => {
  const {
    sortBySpecialty,
    setAutoCompleteValue,
    mappedData,
    onAutoCompleteClickHandler,
    autoCompleteValue,
    specialitiesList,
    doctors: { isGetDoctorsListLoading },
  } = useDoctorsListController();

  return (
    <Container>
      <Paper>
        <AutoComplete openOnFocus freeSolo={true}>
          <FormControl sx={{ display: "flex", gap: 3 }} w="100">
            <AutoCompleteInput
              onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
                setAutoCompleteValue(e.target.value)
              }
              name="specialty"
              variant="filled"
              placeholder="Введіть прізвище або спеціальність"
            />
            <AutoCompleteList>
              {sortBySpecialty(
                specialitiesList ? [...specialitiesList] : []
              ).map((speciality) => (
                <AutoCompleteItem
                  key={speciality.id}
                  value={speciality.specialty}
                  onClick={() =>
                    onAutoCompleteClickHandler(speciality.specialty)
                  }
                  textTransform="capitalize"
                >
                  {speciality.specialty}
                </AutoCompleteItem>
              ))}
            </AutoCompleteList>
            <Button onClick={() => onAutoCompleteClickHandler(autoCompleteValue)}>
              Знайти
            </Button>
          </FormControl>
        </AutoComplete>
      </Paper>
      {isGetDoctorsListLoading ? (
        <Preloader size="xl" />
      ) : (
        <Grid className="grid" gap={6}>
          {mappedData}
        </Grid>
      )}
    </Container>
  );
};

export default DoctorsList;
