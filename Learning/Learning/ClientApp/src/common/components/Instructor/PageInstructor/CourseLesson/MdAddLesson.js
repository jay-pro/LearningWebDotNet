import React, { useEffect, useState } from "react";
// import { toast } from "react-toastify";
import axios from "axios";
import { getCurrentIdUser } from "../../../../Service/AuthService";
import AuthHeader from "../../../../Service/AuthHeader";
import { toast } from "react-toastify";
function MdAddLesson() {
	const [lessonName, setLessonName] = useState("");
	const [lessonLink, setLessonLink] = useState("");
	const [material, setMaterial] = useState("");
	const [duration, setDuration] = useState("");
	const [courses, setCourses] = useState([]);
	const [selectCourse, setSelectCourse] = useState("");
	const url = "https://todoapi20210730142909.azurewebsites.net/api/Instructor";
	const IdUser = getCurrentIdUser();

	useEffect(() => {
		axios
			.get(`${url}/course`, {
				params: { IDUser: IdUser },
				headers: AuthHeader(),
			})
			.then((res) => {
				setCourses(res.data.data);
				setSelectCourse(res.data.data[0].idCourse);
			})
			.catch((error) => {
				console.error(error);
				// toast.error("fail");
			});
	}, [url, IdUser]);

	const onSelectCourse = (e) => {
		setSelectCourse(e.target.value);
	};

	const onAddLesson = () => {
		axios
			.post(
				`${url}/course/${selectCourse}/create-course-detail`,
				{
					lessonName: lessonName,
					lessonLink: lessonLink,
					material: material,
					lessonDuration: duration,
				},
				{
					params: { IDCourse: selectCourse },
					headers: AuthHeader(),
				}
			)
			.then((res) => {
				console.log(res.data);
				toast.success("Create success");
				window.location.reload();
			})
			.catch((err) => {
				console.log(err);
				toast.error("faild");
			});
	};
	return (
		<div className="list-course-select">
			<select
				name="course"
				className="form-create-question"
				id="course"
				onChange={(e) => onSelectCourse(e)}
			>
				{courses.map((course, id) => {
					return (
						<option className="option-item" key={id} value={course.idCourse}>
							{course.courseName}
						</option>
					);
				})}
			</select>
			<div className="form-create-class">
				<input
					onChange={(e) => setLessonName(e.target.value)}
					placeholder="Lesson Name"
					className="desc-name-class"
				></input>
				<input
					onChange={(e) => setMaterial(e.target.value)}
					placeholder="description"
					className="desc-name-class"
				></input>
				<input
					onChange={(e) => setDuration(e.target.value)}
					placeholder="Duration ...."
					className="desc-name-class"
				></input>
				<input
					onChange={(e) => setLessonLink(e.target.value)}
					placeholder="Link video ...."
					className="desc-name-class"
				></input>

				<button onClick={onAddLesson} className="button-create-class">
					Add
				</button>
			</div>
		</div>
	);
}

export default MdAddLesson;
