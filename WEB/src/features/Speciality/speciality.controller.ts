import { useGetAllSpecialitiesQuery } from "@features/Speciality/specialities.service.ts";

const useSpecialityController = () => {
	const { data, isError, isLoading } = useGetAllSpecialitiesQuery();
	
	return {
		specialities: data,
		isError,
		isLoading,
	};
};

export default useSpecialityController;
