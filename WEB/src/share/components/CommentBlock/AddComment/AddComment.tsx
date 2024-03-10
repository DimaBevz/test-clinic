import { Box, Button, Heading, HStack, Stack, Textarea } from "@chakra-ui/react";
import { useAppSelector } from "@store/index.ts";
import { authSelectors } from "@store/auth";
import { IAuthData } from "@interfaces/IAuth.ts";
import { useState } from "react";
import { StarRating } from "@components/index.ts";
import { useTranslation } from "react-i18next";
import { useCreateCommentMutation } from "@api/comments.service";

interface AddCommentProps {
  refetch?: () => void;
  doctorId: string;
  onClose?: () => void;
}

export const AddComment = ({refetch, doctorId, onClose}: AddCommentProps) => {
  const [text, setText] = useState("");
  const user = useAppSelector(authSelectors.getAuthUser) as IAuthData;
  const { t } = useTranslation();
  const [ sendComment, { isLoading } ] = useCreateCommentMutation()
  const [rating, setRating] = useState(0);
  const onSubmit = async () => {
    try {
      await sendComment({
        physicianId: doctorId as string,
        commentText: text,
        rating: rating,
      })
      onClose && onClose();
      refetch && refetch();
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
                colorScheme="yellow"
                onChange={(e) => setText(e.target.value)}
              />
              <HStack justifyContent="space-between">
                <Box/>
                <Button
                  size='sm'
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
