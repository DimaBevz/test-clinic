import { useParams } from "react-router-dom";
import { Box, Button, Heading, HStack, Stack, Textarea } from "@chakra-ui/react";
import { useAppSelector } from "@store/index.ts";
import { authSelectors } from "@features/auth";
import { IAuthData } from "@interfaces/IAuth.ts";
import { useState } from "react";
import { StarRating } from "@components/index.ts";
import commentsService from "@features/Comments/comments.service.ts";
import { useTranslation } from "react-i18next";

export const AddComment = ({refetch}: {refetch: () => any}) => {
  const [text, setText] = useState("");
  const user = useAppSelector(authSelectors.getAuthUser) as IAuthData;
  const { t } = useTranslation();
  const { id } = useParams();
  const [ sendComment, { isLoading } ] = commentsService.useCreateCommentMutation()
  const [rating, setRating] = useState(0);
  const onSubmit = async () => {
    try {
      await sendComment({
        physicianId: id as string,
        commentText: text,
        rating: rating,
      })
      refetch();
      setText("");
      setRating(0);
    } catch (error) {
      console.error(error);
    }
  };
  return (
    <>
      {user  && (
          <Box>
            <Heading as={ "h4" } size={ "md" } pb={ 3 }>{ t( "Leave a comment" ) }</Heading>
            <Stack gap={5}>
              <StarRating value={ rating } onChange={setRating}/>
              <Textarea
                required
                rows={3}
                variant="filled"
                placeholder="Залишити коментар..."
                value={text}
                disabled={isLoading}
                colorScheme="orange"
                onChange={(e) => setText(e.target.value)}
              />
              <HStack justifyContent="space-between">
                <Box/>
                <Button
                  size='sm'
                  colorScheme="orange"
                  isDisabled={isLoading || text === "" || rating === 0}
                  onClick={onSubmit}
                >
                  {t("Submit")}
                </Button>
              </HStack>
              
            </Stack>
            
          </Box>
      )}
    </>
  );
};
