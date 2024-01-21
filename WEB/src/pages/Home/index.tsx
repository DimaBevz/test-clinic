import { CustomContainer } from "@components/Container";
import { CustomPaper } from "@components/Paper";
import { Heading, Text } from "@chakra-ui/react";
import { useTranslation } from "react-i18next";

const Home = () => {
	const { t } = useTranslation();
	return (
		<CustomContainer >
			<CustomPaper h="250px" alignItems={"center"} flexDirection={"column"} display={"flex"} justifyContent={"space-between"}>
				<Heading>
					{t("WelcomeText")}
				</Heading>
				<Text fontSize="18px">{t("Main welcome text")}</Text>
			</CustomPaper>
		</CustomContainer>
	);
};

export default Home;
