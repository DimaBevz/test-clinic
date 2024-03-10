import { HStack, Icon, Text } from "@chakra-ui/react";
import { MdStar, MdStarHalf, MdStarOutline } from "react-icons/md";

interface IStarRating {
  value: number;
  onChange?: (value: number) => void;
}
export default function StarRating({ value, onChange }: IStarRating) {
  const fullStars = Math.floor(value)
  const halfStars = (value - fullStars) >= 0.5 ? 1 : 0
  const emptyStars = 5 - fullStars - halfStars
  
  const handleClick = (index: number) => {
    if (onChange) {
      onChange(index + 1);
    }
  }
  
  return (
    <HStack spacing={"2px"}>
      
      {[...Array(fullStars)].map((_, i) => (
        <Icon
          key={i}
          boxSize={6}
          color="#ECC94B"
          _hover={onChange && {transform: "scale(1.2)"}}
          as={MdStar}
          onClick={() => handleClick(i)}
        />
      ))}
      
      {halfStars > 0 && (
        <Icon
          boxSize={6}
          as={MdStarHalf}
          color="#ECC94B"
        />
      )}
      
      {[...Array(emptyStars)].map((_, i) => (
        <Icon
          key={5-i-1}
          boxSize={6}
          color="gray"
          _hover={onChange && {transform: "scale(1.2)"}}
          as={MdStarOutline}
          onClick={() => handleClick(fullStars + halfStars + i)}
        />
      ))}
      
      <Text fontSize={16}>{+value.toFixed(2)}</Text>
    </HStack>
  );
}
