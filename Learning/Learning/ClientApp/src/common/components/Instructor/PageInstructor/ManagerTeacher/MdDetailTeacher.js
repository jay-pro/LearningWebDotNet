import React, { useEffect, useState } from "react";
import axios from "axios";
import { getCurrentIdUser } from "../../../../Service/AuthService";
import AuthHeader from "../../../../Service/AuthHeader";

function MdDetailTeachert({ setModalIsOpen, dataIdTeacher }) {
	const [info, setInfo] = useState([]);
	const IdUser = getCurrentIdUser();

	const url = `https://todoapi20210730142909.azurewebsites.net/api/Instructor/get-teacher/${dataIdTeacher}`;
	useEffect(() => {
		const loadProfile = () => {
			axios
				.get(url, { params: { IDUser: IdUser }, headers: AuthHeader() })
				.then((res) => {
					setInfo(res.data.data.teacherDetail);
					console.log(res.data.data.teacherDetail);
				})
				.catch((err) => console.log(err));
		};
		loadProfile();
	}, [url, IdUser]);
	return (
		<div className="profile-notflex">
			{info.map((item, id) => (
				<div key={id} className="profile-right">
					<h1 className="title-profile-right">Personal information</h1>
					<img className="avt-teacher" src={item.avatar} alt="" />
					<div className="profile-item">
						<span className="item-info">Name:</span>
						<span className="info-detail">{item.userName}</span>
					</div>
					<div className="profile-item">
						<span className="item-info">Email:</span>
						<span className="info-detail">{item.email}</span>
					</div>
					<div className="profile-item">
						<span className="item-info">Date Of Birth:</span>
						<span className="info-detail">{item.dateOfBirth}</span>
					</div>
					<div className="profile-item">
						<span className="item-info">Phone Number:</span>
						<span className="info-detail">{item.phoneNumber}</span>
					</div>
					<div className="profile-item">
						<span className="item-info">Address</span>
						<span className="info-detail">{item.address}</span>
					</div>
					<div className="button-exit">
						<button onClick={() => setModalIsOpen(false)} className="btn-exit">
							Exit
						</button>
					</div>
				</div>
			))}
		</div>
	);
}

export default MdDetailTeachert;
