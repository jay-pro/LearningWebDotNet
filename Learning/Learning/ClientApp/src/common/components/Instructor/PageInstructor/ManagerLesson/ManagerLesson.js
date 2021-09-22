import React, { useEffect, useState } from "react";
import { Radio, Input } from "antd";
import "./ManagerLesson.css";
import Modal from "react-modal";
import axios from "axios";
import { toast } from "react-toastify";
import AuthHeader from "../../../../Service/AuthHeader";
import ListQuizz from "./ListQuizz";
import { getCurrentIdUser } from "../../../../Service/AuthService";

Modal.setAppElement("#root");
function ManagerLesson() {
	const url =
		"https://todoapi20210730142909.azurewebsites.net/api/Instructor/course";
	const IdUser = getCurrentIdUser();
	const [value, setValue] = useState(1);
	const [modalIsOpen, setModalIsOpen] = useState(false);
	const onChange = (e) => {
		console.log("radio checked", e.target.value);
		setValue(e.target.value);
	};
	const [courses, setCourses] = useState([]);
	useEffect(() => {
		axios
			.get(url, { params: { IDUser: IdUser }, headers: AuthHeader() })
			.then((res) => {
				setCourses(res.data.data);
				console.log(res.data.data);
			})
			.catch((error) => {
				console.error(error);
				toast.error("faile");
			});
	}, [url, IdUser]);
	return (
		<div className="wrapper-dashboard ">
			<button className="add-quizz" onClick={() => setModalIsOpen(true)}>
				Create Quizz
			</button>
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
						height: "80%",
					},
				}}
			>
				<div className="wrapper-flex ">
					<h1 className="title-create-test">Create Quizz</h1>
					<div className="form-create-test">
						<select name="course" className="form-create-question" id="course">
							{courses.map((course, id) => {
								return (
									<option
										className="option-item"
										key={id}
										data={course.idCourse}
									>
										{course.courseName}
									</option>
								);
							})}
						</select>
						<input
							type="text"
							className="form-create-question"
							placeholder="Thời lượng"
						/>
						<input
							type="text"
							className="form-create-question"
							placeholder="Nhập câu hỏi"
						/>
						<Radio.Group onChange={onChange} value={value}>
							<Radio className="radio-custom" value={1}>
								A
								<Input style={{ width: 100, marginLeft: 10 }} />
							</Radio>
							<Radio className="radio-custom" value={2}>
								B
								<Input style={{ width: 100, marginLeft: 10 }} />
							</Radio>
							<Radio className="radio-custom" value={3}>
								C
								<Input style={{ width: 100, marginLeft: 10 }} />
							</Radio>
							<Radio className="radio-custom" value={4}>
								D
								<Input style={{ width: 100, marginLeft: 10 }} />
							</Radio>
						</Radio.Group>
					</div>

					<div className="button-save-test">
						<button className="save-create-test">Save</button>
					</div>
				</div>
			</Modal>
			<ListQuizz />
		</div>
	);
}

export default ManagerLesson;
