import React, { useEffect, useState } from "react";
import "./DetailProfile.css";
import { Link } from "react-router-dom";
import axios from "axios";
import { getCurrentIdUser } from "../../Service/AuthService";
import AuthHeader from "../../Service/AuthHeader";
import Modal from "react-modal";
import ModalProfile from "./ModalProfile";

function DetailProfile() {
	const [modalIsOpen, setModalIsOpen] = useState(false);
	// const [currentIdUser, setCurrentIdUser] = useState("");
	const [data, setData] = useState("");
	const IdUser = getCurrentIdUser();

	// useEffect(() => {
	// 	if (IdUser) {
	// 		setCurrentIdUser(IdUser);
	// 	}
	// }, [IdUser]);
	const url = `https://todoapi20210730142909.azurewebsites.net/api/Users/${IdUser}`;
	useEffect(() => {
		const loadProfile = () => {
			if (IdUser == null) return 0;
			axios
				.get(url, { headers: AuthHeader() })
				.then((res) => {
					setData(res.data.data);
					console.log(res.data.data);
				})
				.catch((err) => console.log(err));
		};
		loadProfile();
	}, [url, IdUser]);

	return (
		<div className="form-profile">
			<div className="profile">
				<div className="profile-left">
					<img className="img-left" src={data.avatar} alt="" />
					<div className="link">
						<Link className="link-home" to="/" href="">
							Home
						</Link>
					</div>
				</div>
				<div className="profile-right">
					<h1 className="title-profile-right">Personal information</h1>
					<div className="profile-item">
						<span className="item-info">Email:</span>
						<span className="info-detail">{data.email}</span>
					</div>
					<div className="profile-item">
						<span className="item-info">Name:</span>
						<span className="info-detail">{data.userName}</span>
					</div>

					<div className="profile-item">
						<span className="item-info">Full name:</span>
						<span className="info-detail">{data.fullname}</span>
					</div>
					<div className="profile-item">
						<span className="item-info">Phone Number:</span>
						<span className="info-detail">{data.phoneNumber}</span>
					</div>
					<div className="profile-item">
						<span className="item-info">Address</span>
						<span className="info-detail">{data.address}</span>
					</div>
					<button
						onClick={() => setModalIsOpen(true)}
						className="btn-update-account"
					>
						Update Account
					</button>
				</div>
			</div>
			<Modal
				isOpen={modalIsOpen}
				onRequestClose={() => setModalIsOpen(false)}
				style={{
					overlay: {
						backgroundColor: "rgba(0,0,0,0.4)",
					},
					content: {
						width: "80%",
						margin: "auto",
						height: "70%",
					},
				}}
			>
				<ModalProfile data={data} setModalIsOpen={setModalIsOpen} />
			</Modal>
		</div>
	);
}

export default DetailProfile;
