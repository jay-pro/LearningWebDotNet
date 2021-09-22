/* eslint-disable jsx-a11y/alt-text */
import React from "react";
import { Carousel } from "antd";
import "antd/dist/antd.css";
import "./Home.css";
// import slide1 from "../../../images/slide1.jpg";
import slide2 from "../../../images/slide2.jpg";
import slide3 from "../../../images/slide3.jpg";
import slide4 from "../../../images/slide4.jpg";

function SliderImg() {
	return (
		<div>
			<Carousel autoplay className="slider">
				<div>
					<div className="contentStyle"></div>
				</div>
				<div>
					<img className="contentStyle" src={slide2} />
				</div>
				<div>
					<img className="contentStyle" src={slide3} />
				</div>
				<div>
					<img className="contentStyle" src={slide4} />
				</div>
			</Carousel>
		</div>
	);
}

export default SliderImg;
