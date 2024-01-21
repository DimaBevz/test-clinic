import api from '@store/rtk.config';
import { CommentModel, CreateCommentModel } from "@interfaces/comment.ts";

const ROOT_PATH = 'Comment';

const commentsService = api.injectEndpoints( {
	endpoints: ( builder ) => ( {
		getCommentsById: builder.query<CommentModel[], string>( {
			query: ( id ) => ( {
				url: `${ ROOT_PATH }/GetComments`,
				params: { physicianId: id },
				method: 'GET',
			} ),
			providesTags: [ 'comments-list' ],
			transformResponse: (response: any) => response.data,
		} ),
		createComment: builder.mutation<void, CreateCommentModel>( {
			query: ( body ) => ( {
				url: `${ ROOT_PATH }/CreateComment`,
				method: 'POST',
				body,
			} ),
		} ),
	} ),
} );

export default commentsService;
