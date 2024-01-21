import api from '@store/rtk.config';
import { PatientsPartialModel } from "@interfaces/patient.ts";
import { IAuthData } from "@interfaces/IAuth.ts";

const ROOT_PATH = 'Patient';

const patientsService = api.injectEndpoints( {
	endpoints: ( builder ) => ( {
		getPatientById: builder.query<PatientsPartialModel, IAuthData['id']>( {
			query: ( id ) => ( {
				url: `${ ROOT_PATH }/GetPatientData/${ id }`,
				method: 'GET',
			} ),
			providesTags: [ 'patient' ],
			transformResponse: (response: any) => response.data,
		} ),
	} ),
} );

export default patientsService;
