import React, { useState, useEffect } from "react";
import "./ManagerCourse.css";
import axios from "axios";
import { getCurrentIdUser } from "../../../../Service/AuthService";
import AuthHeader from "../../../../Service/AuthHeader";
import { Image } from "cloudinary-react";
import { toast } from "react-toastify";

function ManagerCourse() {
	const [currentIdUser, setCurrentIdUser] = useState(null);
	const IdUser = getCurrentIdUser();

	useEffect(() => {
		if (IdUser) {
			setCurrentIdUser(IdUser);
		}
	}, [IdUser]);

	const url =
		"https://todoapi20210730142909.azurewebsites.net/api/Instructor/create-course";
	const [name, setName] = useState();
	const [description, setDescription] = useState();
	const [field, setField] = useState();
	// const [image, setImage] = useState();
	const [duration, setDuration] = useState();
	// const [nameCreate, setNameCreate] = useState();
	const [imageSelected, setImageSelected] = useState("");
	const [dataimage, setDataImage] = useState();

	const uploadImage = () => {
		const formData = new FormData();
		formData.append("file", imageSelected);
		formData.append("upload_preset", "xwfcqasw");

		axios
			.post("https://api.cloudinary.com/v1_1/dbml4nd68/image/upload", formData)
			.then((res) => {
				console.log(res.data.url);
				setDataImage(res.data.url);
			})
			.catch((err) => console.log(err));
	};

	function onSubmit() {
		axios
			.post(
				url,
				{
					courseName: name,
					description: description,
					field: field,
					image: dataimage,
					duration: duration,
					// instructor: nameCreate,
				},
				{ params: { IDUser: currentIdUser }, headers: AuthHeader() }
			)
			.then((res) => {
				console.log(res.data);
				toast.success("create success");
				window.location.reload();
			})
			.catch((err) => console.log(err));
	}

	return (
		<div className="wrapper-dashboard ">
			<div className="wrapper-flex ">
				<h1 className="title-name-create">CREATE COURSE </h1>
				<div className="manager-course">
					<div className="manager-course-left">
						<div className="create-course-item">
							<Image
								style={{ width: 200 }}
								cloudName="dbml4nd68"
								publicId={dataimage}
							/>
							<input
								onChange={(e) => setImageSelected(e.target.files[0])}
								className="course-desc"
								type="file"
							/>
							<button className="btn-loadImage" onClick={uploadImage}>
								upload Image
							</button>
						</div>
						<div className="create-course-item">
							<input
								onChange={(e) => setName(e.target.value)}
								className="title-course"
								type="text"
								placeholder="Name Course"
							/>
							<input
								onChange={(e) => setDescription(e.target.value)}
								type="text"
								placeholder="description"
								className="course-create-description"
							/>
							<div className="course-material">
								<div className="create-course-upload">
									<h1>Field:</h1>
									<input
										className="create-course-desc"
										type="text"
										placeholder="..."
										onChange={(e) => setField(e.target.value)}
									/>
								</div>
								<div className="create-course-upload">
									<h1>Duration</h1>
									<input
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
				<div className="btn-create-sourse">
					{IdUser == null ? (
						<button onClick={onSubmit} className="Create-course">
							Xuat ban khoa hoc
						</button>
					) : (
						<button onClick={onSubmit} className="Create-course">
							Xuat ban khoa hoc
						</button>
					)}
				</div>
			</div>
		</div>
	);
}

export default ManagerCourse;
