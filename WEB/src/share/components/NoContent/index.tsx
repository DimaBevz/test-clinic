import { FunctionComponent } from "react";
import { Center, Text } from "@chakra-ui/react";

interface INoContentProps {
	content: string;
}

const NoContent: FunctionComponent<INoContentProps> = ({content}) => {
	return (
		<Center
			h="150px"
			w="100%"
			borderRadius={5}
			borderWidth="2px"
			borderColor="orange.200"
		>
			<Text fontSize="16px">{content}</Text>
		</Center>
	);
};
export default NoContent;
