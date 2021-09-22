import React from "react";
import "./Home.css";
import "../../../App.css";
import imgBanner from "../../../images/img-baner.png";

function Banner() {
	return (
		<div className="banner">
			<div className="banner-info">
				<h1 className="banner-heading">
					The best way
					<br />
					to organize
					<br />
					your work
				</h1>
				<p className="banner-desc text">
					When You Think It’s Too late, The Truth Is, It’s Still Early.
				</p>
				<div className="banner-links">
					<span className="button button--primary">Get Started</span>
					<span className="button button--outline">How it works</span>
				</div>
			</div>
			<img src={imgBanner} alt="" className="banner-image" />
		</div>
	);
}

export default Banner;
