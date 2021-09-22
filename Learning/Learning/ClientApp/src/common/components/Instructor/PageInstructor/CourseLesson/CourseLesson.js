import React, { useState, useEffect, useCallback } from "react";
import axios from "axios";
import { getCurrentIdUser } from "../../../../Service/AuthService";
import AuthHeader from "../../../../Service/AuthHeader";
import { toast } from "react-toastify";
import Modal from "react-modal";
import MdAddLesson from "./MdAddLesson";
import ReactPlayer from "react-player";
import "./CourseLesson.css";
import MdUpdateLesson from "./MdUpdateLesson";

function CourseLesson() {
	const [modalAddLesson, setModalAddLesson] = useState(false);
	const [courses, setCourses] = useState([]);
	const [modalUpdateLesson, setModalUpdateLesson] = useState(false);
	const [idLesson, setIdlesson] = useState([]);
	const [idCourse, setIdCourse] = useState([]);
	const [courseLesson, setCourseLesson] = useState([]);
	const url = "https://todoapi20210730142909.azurewebsites.net/api/Instructor";
	const IdUser = getCurrentIdUser();
	const getListLesson = useCallback(
		async (course) => {
			axios
				.get(`${url}/course/${course}/course-detail-list`, {
					params: { IDUser: IdUser, IDCourse: course },
					headers: AuthHeader(),
				})
				.then((res) => {
					console.log(res.data.data.course.idCourse);
					setCourseLesson(res.data.data.courseDetailList);
					setIdCourse(res.data.data.course.idCourse);
				})
				.catch((error) => {
					console.error(error);
					toast.error("fail");
				});
		},
		[IdUser, url]
	);

	const getListCourses = useCallback(async () => {
		axios
			.get(`${url}/course`, {
				params: { IDUser: IdUser },
				headers: AuthHeader(),
			})
			.then((res) => {
				setCourses(res.data.data);
			})
			.catch((error) => {
				console.error(error);
				toast.error("fail");
			});
	}, [IdUser, url]);

	useEffect(() => {
		getListCourses();
	}, [getListCourses]);

	const onSelectCourse = (e) => {
		getListLesson(e.target.value);
	};
	const onGetIdLesson = (item) => {
		setModalUpdateLesson(true);
		setIdlesson(item);
	};

	return (
		<div className="wrapper-dashboard">
			<div className="wrapper-flex">
				<div className="form-create-class">
					<button
						onClick={() => setModalAddLesson(true)}
						className="btn-add-class"
					>
						Create Lesson
					</button>
				</div>
			</div>
			<div className="show-list-Class">
				<div className="list-course-select">
					<select
						name="course"
						className="form-create-question"
						id="course"
						onChange={(e) => onSelectCourse(e)}
					>
						{courses.map((course, id) => {
							return (
								<option
									className="option-item"
									key={id}
									value={course.idCourse}
								>
									{course.courseName}
								</option>
							);
						})}
					</select>
				</div>
				<div className="item-class-course">
					{courseLesson.map((item, id) => (
						<div key={id} className="simple-card">
							<div className="simple-card-image">
								<ReactPlayer url={item.lessonLink} className="item-video" />
							</div>
							<div className="simple-card-content">
								<h3 className="simple-card-title">{item.lessonName}</h3>
								<p className="simple-card-desc">{item.material}</p>
								<div className="simple-card-line"></div>
								<button
									onClick={() => onGetIdLesson(item)}
									className="btn-update-lesson"
								>
									Update
								</button>
							</div>
						</div>
					))}
				</div>
			</div>

			<Modal
				isOpen={modalAddLesson}
				onRequestClose={() => setModalAddLesson(false)}
				style={{
					overlay: {
						backgroundColor: "rgba(0,0,0,0.4)",
					},
					content: {
						width: "30%",
						margin: "auto",
						height: "60%",
					},
				}}
			>
				<MdAddLesson
					// dataIdTeacher={selectedTeacher}
					// item={item}
					setModalAddClass={setModalAddLesson}
					// idCourse={item.idCourse}
				/>
			</Modal>
			<Modal
				isOpen={modalUpdateLesson}
				onRequestClose={() => setModalUpdateLesson(false)}
				style={{
					overlay: {
						backgroundColor: "rgba(0,0,0,0.4)",
					},
					content: {
						width: "50%",
						margin: "auto",
						height: "60%",
					},
				}}
			>
				<MdUpdateLesson
					setModalDeleteTeacher={setModalUpdateLesson}
					data={idLesson}
					idCourse={idCourse}
				/>
			</Modal>
		</div>
	);
}

export default CourseLesson;
