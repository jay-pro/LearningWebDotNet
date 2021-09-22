import React, { useState } from "react";
// import { toast } from "react-toastify";
import axios from "axios";
import { getCurrentIdUser } from "../../../../Service/AuthService";
import AuthHeader from "../../../../Service/AuthHeader";
import { toast } from "react-toastify";

function MdUpdateLesson({ data, idCourse }) {
	const [lessonName, setLessonName] = useState(data.lessonName);
	const [lessonLink, setLessonLink] = useState(data.lessonLink);
	const [material, setMaterial] = useState(data.material);
	const [duration, setDuration] = useState(data.lessonDuration);
	// const IdUser = getCurrentIdUser();
	const idDetail = data.idCourseDetail;
	console.log(data.idCourseDetail);
	const url = `https://todoapi20210730142909.azurewebsites.net/api​/Instructor​/course​/${data.idCourseDetail}​/update-course-detail`;

	function onUpdate() {
		axios
			.put(
				url,
				{
					// idCourse: idCourse,
					// idCourseDetail: data.idCourseDetail,
					lessonName: lessonName,
					lessonLink: lessonLink,
					material: material,
					lessonDuration: duration,
				},
				{
					params: { IDCourseDetail: idDetail },
					headers: AuthHeader(),
				}
			)
			.then((res) => {
				console.log(res.data);
				toast.success("success");
				// window.location.reload();
			})
			.catch((err) => {
				console.log("Lỗi." + err);
				toast.error("faild");
			});
	}

	return (
		<div className="wrapp-flex">
			<h1 className="title-update">UPDATE</h1>
			<div className="list-update-lesson">
				<div className="mdupddate-info">
					<div className="item-update">
						<span>Lesson Name:</span>
						<input
							onChange={(e) => setLessonName(e.target.value)}
							type="text"
							value={lessonName}
						/>
					</div>
					<div className="item-update">
						<span>Link:</span>
						<input
							onChange={(e) => setLessonLink(e.target.value)}
							type="text"
							placeholder="Link..."
							value={lessonLink}
						/>
					</div>
					<div className="item-update">
						<span>material:</span>
						<input
							onChange={(e) => setMaterial(e.target.value)}
							type="text"
							placeholder="Material..."
							value={material}
						/>
					</div>
					<div className="item-update">
						<span>Duration:</span>
						<input
							value={duration}
							onChange={(e) => setDuration(e.target.value)}
							type="text"
							placeholder="Duration..."
						/>
					</div>
				</div>
			</div>

			<div className="btn-save-profile">
				<button onClick={onUpdate} className="btn-save">
					Save
				</button>
			</div>
		</div>
	);
}

export default MdUpdateLesson;
