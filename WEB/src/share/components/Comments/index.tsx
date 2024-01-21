import { Divider, Heading, Stack, Text, VStack } from "@chakra-ui/react";
import "./index.scss";
import { CommentModel } from "@interfaces/comment.ts";
import { StarRating } from "@components/index.ts";
import { useTranslation } from "react-i18next";
import NoContent from "@components/NoContent";

const Comment = ( { comment, visitorId }: { comment: CommentModel, visitorId?: string } ) => {
	console.log(visitorId);
	return (
		<Stack className="Comment">
			<Stack className="Comment__header" flexDirection="row" justifyContent="flex-start">
				<Stack className="Comment__header__name">
					<Text>{comment.firstName} </Text>
				</Stack>
				<Stack className="Comment__header__rating">
					<StarRating value={comment.rating}/>
				</Stack>
			</Stack>
			<Stack className="Comment__body">
				<Text>
					{ comment.commentText }
				</Text>
			</Stack>
		</Stack>
	);
}

const Comments = ( { comments, isBigger, visitorId }: { comments: CommentModel[] | undefined, isBigger: boolean, visitorId?: string } ) => {
	const { t } = useTranslation();
	
	return (
		<Stack className="Comments">
			<Heading as={ "h4" } size={ "md" } pb={ 3 }>{ t( "Comments" ) }</Heading>
			<Stack maxH={ isBigger? "700px" :"500px" } overflowY="auto">
				{comments?.length ? (
					comments.map((comment, index) => (
						<VStack key={comment.id} align="stretch">
							<Comment comment={ comment } visitorId={ visitorId }/>
							
							{index < comments.length - 1 && <Divider borderBottomWidth="2px" />}
						</VStack>
					))
				) : (
					<NoContent content={t("No comments yet")}/>
				)}
			</Stack>
			
		</Stack>
	);
};

export default Comments;
  
