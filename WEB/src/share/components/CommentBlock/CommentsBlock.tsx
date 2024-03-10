import { Stack } from "@chakra-ui/react";
import Comments from "@components/Comments";
import { AddComment } from "./AddComment/AddComment";
import { useParams } from "react-router-dom";
import { useGetComments } from "@hooks/comment/useGetComments";

interface ICommentsBlock {
  usersCommentsId: string;
  visitorId: string;
  isDoctor: boolean;
}

export const CommentsBlock = ({
  usersCommentsId,
  visitorId,
  isDoctor,
}: ICommentsBlock) => {
  const { comments, refetch } = useGetComments({
    id: usersCommentsId,
  });
  const { id } = useParams();
  return (
    <Stack>
      {visitorId === usersCommentsId || isDoctor ? (
        <></>
      ) : (
        <AddComment refetch={refetch} doctorId={id as string} />
      )}
      <Comments
        comments={comments}
        isBigger={visitorId === usersCommentsId || isDoctor}
        visitorId={visitorId}
      />
    </Stack>
  );
};
