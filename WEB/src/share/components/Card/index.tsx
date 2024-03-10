import {
  CardBody,
  Card as ChakraUiCard,
  Heading,
  Image,
  Stack,
  Text,
} from "@chakra-ui/react";

interface ICardProps {
  src: string;
  alt: string;
  cardText?: string;
  cardLabel?: string;
}

export const Card = ({ src, alt, cardLabel, cardText }: ICardProps) => {
  return (
    <ChakraUiCard maxW="sm">
      <CardBody>
        <Image src={src} alt={alt} borderRadius="lg" />
        <Stack mt="6" spacing="3">
          {cardLabel && <Heading size="md">{cardLabel}</Heading>}
          {cardText && <Text>{cardText}</Text>}
        </Stack>
      </CardBody>
    </ChakraUiCard>
  );
};
