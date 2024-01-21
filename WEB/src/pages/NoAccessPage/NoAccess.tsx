import { Box, Icon, Text } from '@chakra-ui/react';

import { TbLock } from "react-icons/tb";
import "./index.scss"

interface NoAccessProps {
	text: string;
}

export const NoAccess = ( { text }: NoAccessProps ) => {
	return (
		<Box className="Container">
			<Text className="Container__LockIconBlock">
				<Icon as={ TbLock } className="Container__LockIcon"/>
			</Text>
			<Text className="Container__Description">{ text }</Text>
		</Box>
	)
}
