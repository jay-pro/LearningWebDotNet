import React from "react";
import "./Home.css";
import avt1 from "../../../images/avt1.jpg";
import avt2 from "../../../images/avt2.jpg";
import avt3 from "../../../images/av3.jpg";

function Feedback() {
	return (
		<div className="client">
			<div className="block">
				<span className="block-caption"> What people say </span>
				<h2 className="block-heading">
					Feedback from
					<br />
					student
				</h2>
			</div>
			<div className="client-list">
				<div className="client-item">
					<img src={avt1} alt="" className="client-image" />
					<h3 className="client-name">Edwin Reye</h3>
					<span className="client-position"> Student </span>
					<div className="client-content">
						I believe that with the knowledge the teachers have taught me, I
						will apply it well at my company in the present and in the future.
						Thank you center, thank you teachers for teaching wholeheartedly.
					</div>
					<div className="client-rate">
						<i className="fa fa-star"></i>
						<i className="fa fa-star"></i>
						<i className="fa fa-star"></i>
						<i className="fa fa-star"></i>
						<i className="fa fa-star-half"></i>
					</div>
				</div>
				<div className="client-item">
					<img src={avt2} alt="" className="client-image" />
					<h3 className="client-name">Jaylay</h3>
					<span className="client-position"> Student </span>
					<div className="client-content">
						The course follows real-life experiences to help students easily
						visualize the work and learn. Instructors convey concise, easy to
						understand to help students absorb quickly. During the learning
						process, group discussions and practical problems are given to help
						them easily grasp, reason and work in groups better.
					</div>
					<div className="client-rate">
						<i className="fa fa-star"></i>
						<i className="fa fa-star"></i>
						<i className="fa fa-star"></i>
						<i className="fa fa-star"></i>
						<i className="fa fa-star-half"></i>
					</div>
				</div>
				<div className="client-item">
					<img src={avt3} alt="" className="client-image" />
					<h3 className="client-name">Royal</h3>
					<span className="client-position"> Student </span>
					<div className="client-content">
						Help me understand more about new standard system management and
						give me a lot of knowledge
					</div>
					<div className="client-rate">
						<i className="fa fa-star"></i>
						<i className="fa fa-star"></i>
						<i className="fa fa-star"></i>
						<i className="fa fa-star"></i>
						<i className="fa fa-star-half"></i>
					</div>
				</div>
			</div>
		</div>
	);
}

export default Feedback;
