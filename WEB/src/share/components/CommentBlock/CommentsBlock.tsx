import { Stack } from "@chakra-ui/react";
import Comments from "@components/Comments";
import { AddComment } from "./AddComment/AddComment";
import useCommentController from "@features/Comments/useCommentController.tsx";

interface ICommentsBlock {
	usersCommentsId: string;
	visitorId: string;
	isDoctor: boolean;
}

export const CommentsBlock = ( { usersCommentsId, visitorId, isDoctor }: ICommentsBlock ) => {
	const { comments, refetch } = useCommentController( usersCommentsId );
	return (
		<Stack>
			{ (visitorId === usersCommentsId || isDoctor) ? <></> : <AddComment refetch={refetch}/> }
			<Comments comments={comments} isBigger={visitorId === usersCommentsId || isDoctor} visitorId={ visitorId }/>
		</Stack>
	);
};
