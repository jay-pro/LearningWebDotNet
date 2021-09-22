import React, { useState, useEffect } from "react";
import Header from "../Home/Header";
import "./DetailCourse.css";
import axios from "axios";
import { useParams } from "react-router";
import { Link } from "react-router-dom";
import AuthHeader from "../../Service/AuthHeader";
import { getCurrentIdUser } from "../../Service/AuthService";
import Modal from "react-modal";
import ModalCourse from "../DetailCourse/ModalCourse";

function DetailCourse() {
	const paramUrl = useParams();
	const [idCourse, setIdCourse] = useState("");
	const IdUser = getCurrentIdUser();
	const url = `https://todoapi20210730142909.azurewebsites.net/api/Courses/course/${paramUrl.id}/course-detail-list`;
	useEffect(() => {
		async function getData() {
			const res = await axios.get(url, {
				headers: AuthHeader(),
			});
			console.log(res.data.data);
			setIdCourse(res.data.data.course);
		}
		getData();
	}, [url, IdUser]);

	const [modalIsOpen, setModalIsOpen] = useState(false);

	return (
		<div className="wrapp-full">
			<div className="wrapper-container">
				<Header />
				<div className="form-detail-course">
					<h1>CHI TIẾT KHÓA HỌC</h1>
					<div className="course-detail-item">
						<h2 className="title-course-item">{idCourse.courseName}</h2>
						<div className="item-course-info">
							<img className="item-course-img" src={idCourse.image} alt="" />
							<div className="course-info-right">
								<div className="fill-course-item">
									<h2>Name Create:</h2>
									<span>Instructor</span>
								</div>
								<div className="fill-course-item">
									<h2>Description:</h2>
									<span className="detail-course-desc">
										{idCourse.description}
									</span>
								</div>
								<div className="fill-course-item">
									<h2>Duration:</h2>
									<span>{idCourse.duration} hours</span>
								</div>

								<Link
									className="button-participate"
									to={`course/study/${paramUrl.id}`}
								>
									Enroll
								</Link>
								<Modal
									isOpen={modalIsOpen}
									onRequestClose={() => setModalIsOpen(false)}
									style={{
										overlay: {
											backgroundColor: "rgba(0,0,0,0.4)",
										},
										content: {
											width: "40%",
											margin: "auto",
											height: "70%",
										},
									}}
								>
									<ModalCourse setModalIsOpen={setModalIsOpen} />
								</Modal>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
}

export default DetailCourse;
