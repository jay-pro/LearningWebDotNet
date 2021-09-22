import React, { useEffect, useState, useCallback } from "react";
import axios from "axios";
import { getCurrentIdUser } from "../../../../Service/AuthService";
import AuthHeader from "../../../../Service/AuthHeader";
import { toast } from "react-toastify";

function MdAssign({ setModalSelect, dataIdTeacher }) {
	const [courses, setCourses] = useState([]);
	const [classes, setClasses] = useState([]);
	const [selectedClass, setSelectedClass] = useState("");
	const [selectedCourse, setSelectedCourse] = useState("");

	const url = "https://todoapi20210730142909.azurewebsites.net/api/Instructor";
	const IdUser = getCurrentIdUser();

	const getListClasses = useCallback(
		async (course) => {
			axios
				.get(`${url}/get-class-list`, {
					params: { IDUser: IdUser, IDCourse: course },
					headers: AuthHeader(),
				})
				.then((res) => {
					setClasses(res.data.data);
					// res.data.data.length
					//   ? setSelectedClass(res.data.data[0].idClass)
					//   : setSelectedClass();
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
		setSelectedCourse(e.target.value);
		getListClasses(e.target.value);
	};

	const onSelectedClass = (e) => {
		setSelectedClass(e.target.value);
	};

	const onAssign = (e) => {
		axios
			.put(
				`${url}/assign-teacher-to-class`,
				{ headers: {} },
				{
					params: { IDTeacher: dataIdTeacher, IDClass: selectedClass },
					headers: AuthHeader(),
				}
			)
			.then((res) => {
				console.log(res.data.data);
				toast.success("success");
			})
			.catch((error) => {
				console.error(error);
				toast.error("faild");
			});
	};

	return (
		<div>
			<div className="list-course-select">
				<select
					name="course"
					className="form-create-question"
					id="course"
					onChange={(e) => onSelectCourse(e)}
				>
					<option value="" disabled selected>
						Chọn khóa học tương ứng
					</option>
					{courses.map((course, id) => {
						return (
							<option className="option-item" key={id} value={course.idCourse}>
								{course.courseName}
							</option>
						);
					})}
				</select>
			</div>
			{/* classes */}
			<div className="list-classes-select">
				<select
					name="class"
					className="form-create-question"
					id="class"
					onChange={(e) => onSelectedClass(e)}
				>
					<option value="" disabled selected>
						Chọn khóa học tương ứng
					</option>
					{classes.map((index, id) => {
						return (
							<option className="option-item" key={id} value={index.idClass}>
								{index.className}
							</option>
						);
					})}
				</select>
				<button className="check-delete" onClick={(e) => onAssign(e)}>
					Assign
				</button>{" "}
			</div>
		</div>
	);
}

export default MdAssign;
