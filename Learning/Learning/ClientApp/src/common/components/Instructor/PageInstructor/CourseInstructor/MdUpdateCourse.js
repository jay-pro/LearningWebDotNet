import React, { useState } from "react";
import axios from "axios";
// import { getCurrentIdUser } from "../../../../Service/AuthService";
import AuthHeader from "../../../../Service/AuthHeader";
import { Image } from "cloudinary-react";
import { toast } from "react-toastify";

function MdUpdateCourse({ setModalUpdate, idCourse, item }) {
	// console.log(item.image);
	// const [imageSelected, setImageSelected] = useState("");
	// const [dataimage, setDataImage] = useState(item.image);
	// const uploadImage = () => {
	// 	const formData = new FormData();
	// 	formData.append("file", imageSelected);
	// 	formData.append("upload_preset", "xwfcqasw");
	// 	axios
	// 		.post("https://api.cloudinary.com/v1_1/dbml4nd68/image/upload", formData)
	// 		.then((res) => {
	// 			console.log(res.data.url);
	// 			setDataImage(res.data.url);
	// 		})
	// 		.catch((err) => console.log(err));
	// };
	const url =
		"https://todoapi20210730142909.azurewebsites.net/api/Instructor/course";
	const [name, setName] = useState(item.courseName);
	const [description, setDescription] = useState(item.description);
	const [field, setField] = useState(item.field);
	const [duration, setDuration] = useState(item.duration);
	function onUpdate() {
		axios
			.put(
				`${url}/${idCourse}`,
				{
					courseName: name,
					description: description,
					field: field,
					// image: dataimage,
					duration: duration,
				},
				{ params: { IDCourse: idCourse }, headers: AuthHeader() }
			)
			.then((res) => {
				console.log(res.data);
				toast.success("update succes");
				window.location.reload();
			})
			.catch((err) => {
				console.log(err);
				toast.error("update faild");
			});
	}

	return (
		<div className="profile-notflex">
			<div className="wrapper-flex ">
				<h1 className="title-name-create">UPDATE COURSE</h1>
				<div className="manager-course">
					<div className="manager-course-left">
						<div className="create-course-item">
							<Image
								style={{ width: 200 }}
								cloudName="dbml4nd68"
								publicId={item.image}
								src={item.image}
							/>
							{/* <input
								onChange={(e) => setImageSelected(e.target.files[0])}
								className="course-desc"
								type="file"
							/>
							<button className="btn-loadImage" onClick={uploadImage}>
								upload Image
							</button> */}
						</div>
						<div className="create-course-item">
							<input
								onChange={(e) => setName(e.target.value)}
								className="title-course"
								type="text"
								placeholder="Name Course"
								value={name}
							/>
							<input
								onChange={(e) => setDescription(e.target.value)}
								type="text"
								placeholder="description"
								className="course-create-description"
								value={description}
							/>
							<div className="course-material">
								<div className="create-course-upload">
									<h1>Field:</h1>
									<input
										value={field}
										className="create-course-desc"
										type="text"
										placeholder="..."
										onChange={(e) => setField(e.target.value)}
									/>
								</div>
								<div className="create-course-upload">
									<h1>Duration</h1>
									<input
										value={duration}
										className="create-course-desc"
										onChange={(e) => setDuration(e.target.value)}
										type="number"
										placeholder="time"
									></input>
								</div>
							</div>
						</div>
					</div>
				</div>
				<div className="button-update-exit">
					<button onClick={onUpdate} className="btn-back">
						UPDATE
					</button>
					{/* <button onClick={() => setModalUpdate(false)} className="btn-back">
						Exit
					</button> */}
				</div>
			</div>
		</div>
	);
}

export default MdUpdateCourse;
