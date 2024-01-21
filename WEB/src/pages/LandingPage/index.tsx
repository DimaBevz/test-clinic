import { Button, Heading, HStack, Image, ListItem, Stack, UnorderedList, VStack } from "@chakra-ui/react";
import "./index.scss";
import { Header } from "@components/Header";
import { useTranslation } from "react-i18next";
import CustomSlider from "@components/CustomSlider";
import bgLand1 from "@assets/img/bgLand1.svg";
import bgLand2 from "@assets/img/bgLand2.svg";
import onPhone from "@assets/img/women-on-phone.jpg";
import { useNavigate } from "react-router-dom";

const LandingPage = () => {
	const { t } = useTranslation();
	const navigate = useNavigate();
	return (
		<VStack w="100%" h="100%" gap={ 0 }>
			<Header isLanding/>
			<Stack className="Landing" alignItems={ "center" } flexDirection={ "column" } display={ "flex" }
						 justifyContent={ "space-between" }>
				<CustomSlider>
					<HStack bgColor="blue.700" w="100%" h="500px" bgImage={ bgLand2 } flexDirection="column" gap={10} bgSize="cover" justifyContent="center">
						<Heading as="h1" size="2xl" color="white">
							{ t( "WelcomeText" ) }
						</Heading>
						<Button onClick={ () => navigate( "/login" ) }>{ t( "Make an appointment" ) }</Button>
					</HStack>
					<HStack bgColor="blue.700" w="100%" h="500px" bgImage={ bgLand1 } bgSize="cover"
									justifyContent="space-evenly">
						<Image
							borderRadius="full"
							verticalAlign="middle"
							w="200px !important"
							h="200px"
							objectFit="cover"
							src={ onPhone }
						/>
						<Stack maxW="400px" gap="10px">
							<Heading as="h1" size="xl" color="white">
								{ t( "SaveTimeForAppointment" ) }
							</Heading>
							<Stack gap="10px">
								<UnorderedList>
									<ListItem fontSize="20px">
										{ t( "Choose a doctor and a date" ) }
									</ListItem>
									<ListItem fontSize="20px">
										{ t( "Determine a convenient time for you" ) }
									</ListItem>
									<ListItem fontSize="20px">
										{ t( "Come to an online appointment" ) }
									</ListItem>
								</UnorderedList>
								<Button onClick={ () => navigate( "/login" ) }>{ t( "Make an appointment" ) }</Button>
							</Stack>
						
						</Stack>
					</HStack>
				</CustomSlider>
			</Stack>
		</VStack>
	);
};

export default LandingPage;
