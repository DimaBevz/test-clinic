import api from '@store/rtk.config';
import { Specialty } from "@interfaces/specialty.ts";


const ROOT_PATH = 'Position';

const specialitiesService = api.injectEndpoints( {
	endpoints: ( builder ) => ( {
		getAllSpecialities: builder.query<Specialty[],void>( {
			query: () => ( {
				url: `${ ROOT_PATH }/GetPositions`,
				method: 'GET',
			} ),
			providesTags: [ 'specialities' ],
			transformResponse: (response: any) => response.data.positions,
		} ),
	} ),
} );

export const { useGetAllSpecialitiesQuery } = specialitiesService
