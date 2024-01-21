import { CustomContainer } from "@components/Container";
import { CustomPaper } from "@components/Paper";
import { Image, Text } from "@chakra-ui/react";
import { useTranslation } from "react-i18next";
import comingSoon from "@assets/img/coming-soon.png";

const BugPage = () => {
	const { t } = useTranslation();
	return (
		<CustomContainer >
			<CustomPaper h="300px" alignItems={"center"} display={"flex"} flexDirection="column" justifyContent={"center"}>
				<Image src={comingSoon}/>
				<Text fontSize="16px">{t("Coming Soonv2")}...</Text>
			</CustomPaper>
		</CustomContainer>
	);
};

export default BugPage;
