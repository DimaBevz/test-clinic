import api from '@store/rtk.config';
import { DoctorsPartialModel, IDoctorListModel } from "@interfaces/doctor.ts";
import { IAuthData } from "@interfaces/IAuth.ts";

const ROOT_PATH = 'Physician';

const doctorsService = api.injectEndpoints( {
	endpoints: ( builder ) => ( {
		getDoctorById: builder.query<DoctorsPartialModel, IAuthData['id']>( {
			query: ( id ) => ( {
				url: `${ ROOT_PATH }/GetPhysicianData/${ id }`,
				method: 'GET',
			} ),
			providesTags: [ 'doctor' ],
			transformResponse: (response: any) => response.data,
		} ),
		getAllDoctors: builder.query<IDoctorListModel[], void>({
			query: () => ({
				url: `${ROOT_PATH}/GetPhysicians`,
				method: 'GET'
			}),
			providesTags: ['doctors-list'],
			transformResponse: (response: any) => response.data.physicians,
		}),
	} ),
} );

export default doctorsService;
