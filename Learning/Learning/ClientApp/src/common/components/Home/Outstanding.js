import React from "react";
import "./Home.css";
import course1 from "../../../images/slide3.jpg";
function Outstanding() {
	return (
		<div className="outstanding">
			<div className="block">
				<span className="block-caption"> outstanding </span>
				<h2 className="block-heading">
					Outstanding
					<br />
					Course
				</h2>
			</div>
			<div className="list-outstand">
				<div className="item-course">
					<div className="item-course__img">
						<img alt="" src={course1} />
					</div>
					<div className="item-course__info">
						<h1 className="info-header">HTML CSS</h1>
						<p className="info-desc">
							On our daily life, we make decisions on daily basis. We make
							decisions not by checking one or two conditions instead we make
						</p>
						<h1 className="info-price">$18</h1>
						<div className="item-course-btn">
							<button className="btn-item-detail">Detail</button>
							<button className="btn-item-study">Study</button>
						</div>
					</div>
				</div>
				<div className="item-course">
					<div className="item-course__img">
						<img alt="" src={course1} />
					</div>
					<div className="item-course__info">
						<h1 className="info-header">HTML CSS</h1>
						<p className="info-desc">
							On our daily life, we make decisions on daily basis. We make
							decisions not by checking one or two conditions instead we make
						</p>
						<h1 className="info-price">$18</h1>
						<div className="item-course-btn">
							<button className="btn-item-detail">Detail</button>
							<button className="btn-item-study">Study</button>
						</div>
					</div>
				</div>
				<div className="item-course">
					<div className="item-course__img">
						<img alt="" src={course1} />
					</div>
					<div className="item-course__info">
						<h1 className="info-header">HTML CSS</h1>
						<p className="info-desc">
							On our daily life, we make decisions on daily basis. We make
							decisions not by checking one or two conditions instead we make
						</p>
						<h1 className="info-price">$18</h1>
						<div className="item-course-btn">
							<button className="btn-item-detail">Detail</button>
							<button className="btn-item-study">Study</button>
						</div>
					</div>
				</div>
				<div className="item-course">
					<div className="item-course__img">
						<img alt="" src={course1} />
					</div>
					<div className="item-course__info">
						<h1 className="info-header">HTML CSS</h1>
						<p className="info-desc">
							On our daily life, we make decisions on daily basis. We make
							decisions not by checking one or two conditions instead we make
						</p>
						<h1 className="info-price">$18</h1>
						<div className="item-course-btn">
							<button className="btn-item-detail">Detail</button>
							<button className="btn-item-study">Study</button>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
}

export default Outstanding;
