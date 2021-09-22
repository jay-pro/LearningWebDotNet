/* eslint-disable jsx-a11y/accessible-emoji */
import React from "react";
import "./Home.css";
import imgStudy from "../../../images/study.png";
import imgStudy2 from "../../../images/study2.png";

function Introl() {
	return (
		<div>
			<div className="intro">
				<div className="intro-content">
					<span className="block-caption block-caption--left">
						study my Home
					</span>
					<h2 className="block-heading">How to learn ?</h2>
					<p className="text intro-desc">
						When students participate in online classes, they can fully grasp
						their own knowledge. Besides, you can choose from a variety of study
						programs to meet all your specific needs.
					</p>
					<button className="button button--primary">Explore more</button>
				</div>
				<img src={imgStudy} alt="" className="intro-image" />
			</div>
			<div className="intro introl-top">
				<img src={imgStudy2} alt="" className="intro-image" />
				<div className="intro-content">
					<span className="block-caption block-caption--left">
						study my Home
					</span>
					<h2 className="block-heading">How to focus on studying ?</h2>
					<p className="text intro-desc">
						Meditation is a method that can significantly increase your focus,
						science has proven! Not only that, it also helps you to relax,
						reduce stress, limit some diseases…with just 20-30 minutes of
						meditation per day, the quality of your spiritual life will be
						improved. Every time you find yourself losing focus, meditate for 5
						minutes and the feeling of focus will return
					</p>
					<button className="button button--primary button-right">
						Explore more
					</button>
				</div>
			</div>
		</div>
	);
}

export default Introl;
