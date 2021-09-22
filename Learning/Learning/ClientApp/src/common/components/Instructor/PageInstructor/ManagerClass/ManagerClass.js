import React, { useState, useEffect, useCallback } from "react";
import MdAddClass from "./MdAddClass";
import Modal from "react-modal";
import "./ManagerClass.css";
import axios from "axios";
import { getCurrentIdUser } from "../../../../Service/AuthService";
import AuthHeader from "../../../../Service/AuthHeader";
import { toast } from "react-toastify";
import MdAssignStudent from "./MdAssignStudent";

function ManagerClass() {
	const [courses, setCourses] = useState([]);
	const [classes, setClasses] = useState([]);
	// const [selectedClass, setSelectedClass] = useState("");
	const url = "https://todoapi20210730142909.azurewebsites.net/api/Instructor";
	const IdUser = getCurrentIdUser();
	const [modalAddClass, setModalAddClass] = useState(false);
	const [modalDeleteTeacher, setModalDeleteTeacher] = useState(false);
	const [selectedClass, setSelectedClass] = useState();

	const getListClasses = useCallback(
		async (course) => {
			axios
				.get(`${url}/get-class-list`, {
					params: { IDUser: IdUser, IDCourse: course },
					headers: AuthHeader(),
				})
				.then((res) => {
					setClasses(res.data.data);
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
		getListClasses(e.target.value);
	};
	const onGetIdClass = (id) => {
		setSelectedClass(id);
		setModalDeleteTeacher(true);
	};
	const onDeleteTeacher = (idRemove, a) => {
		axios
			.put(
				`${url}/remove-teacher-to-class`,
				{},
				{
					params: { IDClass: idRemove },
					headers: AuthHeader(),
				}
			)
			.then((res) => {
				console.log(res.data.data);
				toast.success("succress");
				window.location.reload();
			})
			.catch((error) => {
				console.error(error);
				toast.error("Remove faild");
			});
	};
	return (
		<div className="wrapper-dashboard ">
			<div className="wrapper-flex">
				<div className="form-create-class">
					<button
						onClick={() => setModalAddClass(true)}
						className="btn-add-class"
					>
						Create Class
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
					{classes.map((item, id) => (
						<div key={id} className="item-class-form">
							<h1 className="info-header">{item.className}</h1>
							<p className="info-starttime">Bắt đầu: {item.startTime}</p>
							<p className="info-finishtime">Kết thúc: {item.finishTime}</p>
							{item.teacherName ? (
								<p className="info-teacher-name">
									Teacher Name: {item.teacherName}
								</p>
							) : (
								<p className="info-no-teacher">No Teacher</p>
							)}
							<div className="button-handel-class">
								<div className="item-class-btn-assign ">
									<button
										className="Assign-Student"
										onClick={() => onGetIdClass(item.idClass)}
									>
										Assign Student
									</button>
								</div>
								<div className="item-class-btn-remove">
									<button
										className="btn-remove-teacher"
										onClick={() => onDeleteTeacher(item.idClass, item)}
									>
										Remove Teacher
									</button>
								</div>
							</div>
						</div>
					))}
				</div>
			</div>

			<Modal
				isOpen={modalAddClass}
				onRequestClose={() => setModalAddClass(false)}
				style={{
					overlay: {
						backgroundColor: "rgba(0,0,0,0.4)",
					},
					content: {
						width: "30%",
						margin: "auto",
						height: "50%",
					},
				}}
			>
				<MdAddClass
					// dataIdTeacher={selectedTeacher}
					// item={item}
					setModalAddClass={setModalAddClass}
					// idCourse={item.idCourse}
				/>
			</Modal>
			<Modal
				isOpen={modalDeleteTeacher}
				onRequestClose={() => setModalDeleteTeacher(false)}
				style={{
					overlay: {
						backgroundColor: "rgba(0,0,0,0.4)",
					},
					content: {
						width: "60%",
						margin: "auto",
						height: "50%",
					},
				}}
			>
				<MdAssignStudent
					// dataIdTeacher={selectedTeacher}
					// item={item}
					setModalDeleteTeacher={setModalDeleteTeacher}
					idClass={selectedClass}
				/>
			</Modal>
		</div>
	);
}
export default ManagerClass;
