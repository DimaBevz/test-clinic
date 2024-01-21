import commentsService from "@features/Comments/comments.service.ts";

export default function useCommentController(id: string) {
	const { isLoading, data, refetch} = commentsService.useGetCommentsByIdQuery(id);

	return {
		comments: data,
		refetch,
		isLoading,
	}
}
