import { Grid, HStack, Select } from "@chakra-ui/react";
import doctorsService from "@features/Doctor/doctors.service.ts";
import { Preloader } from "@components/preloader/Preloader";
import { useEffect, useMemo, useState } from "react";
import "./index.scss";
import { CustomContainer } from "@components/Container";
import DoctorCard from "@pages/DoctorsList/components";
import { CustomPaper } from "@components/Paper";
import { useTranslation } from "react-i18next";
import useSpecialityController from "@features/Speciality/speciality.controller.ts";
import { Specialty } from "@interfaces/specialty.ts";


const DoctorsList = () => {
	const { isLoading, data, refetch } = doctorsService.useGetAllDoctorsQuery();
	const { t } = useTranslation();
	const { specialities } = useSpecialityController();
	const [filters, setFilters] = useState({
		speciality: "all",
		sortBy: "all",
		sortDirection: "asc",
	});
	useEffect( () => {
		refetch();
	}, [] );
	
	const filteredData = useMemo(() => {
		let newData = [...(data || [])];
		if ( filters.speciality === 'all' ) {
			newData = (data || []);
		}
		
		if (filters.speciality !== 'all') {
			newData= newData?.filter(physician => {
				return physician.positions.find(position => position.id === filters.speciality)
			});
		}
		
		return newData;
	}, [ data, filters ] );

	function sortBySpecialty(docs: Specialty[]): Specialty[] {
		return docs.sort((a, b) => {
			if(a.specialty < b.specialty) return -1;
			if(a.specialty > b.specialty) return 1;
			return 0;
		});
	}
	
	const mappedData = useMemo( () => filteredData?.map( physician => ( <DoctorCard physician={ physician }/> ) ), [ filteredData ] );

	return (
		<CustomContainer>
			<CustomPaper>
				<HStack gap={10}>
					<Select className="Selector" _focusVisible={ { borderColor: "#DD6B20" } } onChange={(e)=> setFilters({...filters, speciality: e.target.value}) } title={t("Sort by")}>
						<option value="all">{ t("All") }</option>
						{sortBySpecialty(specialities ? [...specialities] : []).map((speciality) => (<option value={speciality.id}>{speciality.specialty}</option>))}
					</Select>
				</HStack>
			</CustomPaper>
			{ isLoading ? (
				<Preloader size="xl"/>
			) : (
				<Grid className="grid" gap={ 6 }>
					{ mappedData }
				</Grid>
			) }
		</CustomContainer>
	);
};

export default DoctorsList;
