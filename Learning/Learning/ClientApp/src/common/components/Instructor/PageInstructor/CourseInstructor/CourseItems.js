import React, { useState } from "react";
import Modal from "react-modal";
import { Link } from "react-router-dom";
import MdUpdateCourse from "./MdUpdateCourse";
function CourseItems({ item }) {
	const [modalUpdate, setModalUpdate] = useState(false);
	return (
		<div className="item-course">
			<div className="item-course__img">
				<img alt="" src={item.image} />
			</div>
			<div className="item-course__info">
				<h1 className="info-header">{item.courseName}</h1>
				<p className="info-desc">{item.description}</p>
				<h1 className="info-price">Free</h1>
				<div className="item-course-btn">
					<button
						className="btn-item-update"
						onClick={() => setModalUpdate(true)}
					>
						Update
					</button>
				</div>
			</div>
			<Modal
				isOpen={modalUpdate}
				onRequestClose={() => setModalUpdate(false)}
				style={{
					overlay: {
						backgroundColor: "rgba(0,0,0,0.4)",
					},
					content: {
						width: "60%",
						margin: "auto",
						height: "60%",
					},
				}}
			>
				<MdUpdateCourse
					// dataIdTeacher={selectedTeacher}
					item={item}
					setModalUpdate={setModalUpdate}
					idCourse={item.idCourse}
				/>
			</Modal>
		</div>
	);
}

export default CourseItems;
