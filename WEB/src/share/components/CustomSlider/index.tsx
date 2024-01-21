import { Carousel } from "react-responsive-carousel";
import "react-responsive-carousel/lib/styles/carousel.min.css";
import "./index.scss";

const CustomSlider = ( { customStyles, children }: {customStyles?: string,children: JSX.Element[]} ) => {
	
	return (
		<Carousel
			infiniteLoop
			className={customStyles}
			showStatus={ false }
			autoPlay
			emulateTouch
		>
			{ children }
		</Carousel>
	);
};

export default CustomSlider;
