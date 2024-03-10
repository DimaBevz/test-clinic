import { useEffect, useMemo, useState } from "react";
import { IGetDoctorsListRequest } from "../doctor.interfaces";
import { useGetDoctorsByParams } from "@hooks/doctor/useGetDoctorsByParams";
import { useGetSpecialities } from "@hooks/general/useGetSpecialities";
import { ISpecialty } from "@interfaces/specialty";
import DoctorCard from "../DoctorCard";

function useDoctorsListController() {
  const [params, setParams] = useState<IGetDoctorsListRequest>({
    page: 1,
    limit: 10,
  });

  const { getDoctorsByParams, isGetDoctorsListLoading, doctorsList } =
    useGetDoctorsByParams();
  const { specialitiesList } = useGetSpecialities();

  const [filters] = useState({
    speciality: "all",
    sortBy: "all",
    sortDirection: "asc",
  });

  const [autoCompleteValue, setAutoCompleteValue] = useState<string>("");

  useEffect(() => {
    getDoctorsByParams(params);
  }, [params]);

  const filteredData = useMemo(() => {
    let newData = [...(doctorsList || [])];
    if (filters.speciality === "all") {
      newData = doctorsList || [];
    }

    if (filters.speciality !== "all") {
      newData = newData?.filter((physician) => {
        return physician.positions.find(
          (position) => position.id === filters.speciality
        );
      });
    }

    return newData;
  }, [doctorsList, filters]);

  function sortBySpecialty(docs: ISpecialty[]): ISpecialty[] {
    return docs.sort((a, b) => {
      if (a.specialty < b.specialty) return -1;
      if (a.specialty > b.specialty) return 1;
      return 0;
    });
  }

  const mappedData = useMemo(
    () =>
      filteredData?.map((physician) => <DoctorCard physician={physician} />),
    [filteredData]
  );

  const onAutoCompleteClickHandler = (value: string) => {
    setAutoCompleteValue(value);
    setParams({ ...params, search: value });
  };

  return {
    sortBySpecialty,
    setAutoCompleteValue,
    mappedData,
    onAutoCompleteClickHandler,
    autoCompleteValue,
    specialitiesList,
    doctors: {
      isGetDoctorsListLoading
    }
  };
}

export default useDoctorsListController;
