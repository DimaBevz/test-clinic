import { useGetCommentsByIdQuery } from "@api/comments.service";
import { ICommentModel } from "@interfaces/comment";

interface IProps {
  id: string;
}

export const useGetComments = ({
  id,
}: IProps): {
  comments?: ICommentModel[];
  refetch: any;
  isCommentsDataLoading: boolean;
} => {
  const {
    isLoading: isCommentsDataLoading,
    data: commentsData,
    refetch,
  } = useGetCommentsByIdQuery(id);

  return {
    comments: commentsData,
    refetch,
    isCommentsDataLoading,
  };
};
