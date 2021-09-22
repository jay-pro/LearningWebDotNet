import { Rate } from "antd";
import React, { useEffect, useState } from "react";
import { useParams } from "react-router";
import "antd/dist/antd.css";
import "./Feedback.css";
import courseCurrent from "../../../../../images/slide1.jpg";
import axios from "axios";
import { getCurrentIdUser } from "../../../../Service/AuthService";

import { toast } from "react-toastify";
import AuthHeader from "../../../../Service/AuthHeader";

toast.configure();

function Feedback() {
	const params = useParams;
	const IdUser = getCurrentIdUser();

	const [currentValue, setCurrentValue] = useState(1);
	const [content, setContent] = useState("");
	const [error, setError] = useState("");
	// const url = "https://todoapi20210730142909.azurewebsites.net/api/Student/create-coursefeedback"
	const url = "https://localhost:44396/api/Student/create-coursefeedback";
	const rating = (value) => {
		setCurrentValue(value);
	};

	const paramUrl = useParams();
	const [idCourse, setIdCourse] = useState("");
	const url1 = `https://todoapi20210730142909.azurewebsites.net/api/Courses/course/${paramUrl.id}/course-detail-list`;
	useEffect(() => {
		async function getData() {
			const res = await axios.get(url1, {
				headers: AuthHeader(),
			});
			console.log(res.data.data);
			setIdCourse(res.data.data.course);
		}
		getData();
	}, [url, IdUser]);

	const sendFeedback = () => {
		if (content === "") {
			setError("Vui lòng nhập nội dung");
		}
		axios
			.post(
				url,
				{
					Content: content,
					Star: currentValue,
					CreatedAt: new Date(),
				},
				{ params: { IDUser: IdUser } }
			)
			.then(() => {
				toast.success("Đã gửi feedback");
				// window.location.reload();
			})
			.catch((err) => {
				toast.success("Đã xảy ra lỗi khi gửi feedback");
				console.log(err);
			});
	};
	return (
		<div className="wrapper-dashboard">
			<h1 className="feedback-title">Feedback Course</h1>
			<div className="form-feedback">
				<img
					className="feedback-info feedback-img"
					src={courseCurrent}
					alt=""
				/>
				<div className="feedback-info">
					<h1 className="feedback-name-course">{idCourse.courseName}</h1>

					<div className="feedback-fill-info">
						<h1 className="feedback-info-desc">Description:</h1>
						<span className="feedback-desc">{idCourse.description}</span>
					</div>
					<div className="feedback-fill-info">
						<h1 className="feedback-info-time">Time:</h1>
						<span className="feedback-time">{idCourse.duration} minute</span>
					</div>
					<input
						placeholder="comment...??"
						className="feedback-comment"
						onChange={(e) => setContent(e.target.value)}
					></input>
					<div className="feedback-rating">
						<Rate onChange={rating} value={currentValue} />
						<br />
						{/* current rating:{currentValue} */}
					</div>
					<button className="btn-feedback-save" onClick={sendFeedback}>
						Save
					</button>
				</div>
			</div>
		</div>
	);
}

export default Feedback;
