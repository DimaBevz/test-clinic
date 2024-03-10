import { useGetSpecialitiesListQuery } from "@api/specialities.service";
import { ISpecialty } from "@interfaces/specialty";

export const useGetSpecialities = (): {
  specialitiesList?: ISpecialty[];
  isSpecialitiesListLoading: boolean;
} => {
  const { isLoading, data } = useGetSpecialitiesListQuery();

  return {
    specialitiesList: data,
    isSpecialitiesListLoading: isLoading,
  };
};
