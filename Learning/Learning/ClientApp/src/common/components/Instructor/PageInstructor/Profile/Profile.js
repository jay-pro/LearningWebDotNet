import React from "react";
import "./Profile.css";
import avt from "../../../../../images/avt.jpeg";

function Profile() {
	return (
		<div className="wrapper-dashboard ">
			<h1 className="title-profile-user">Thông tin cá nhân</h1>
			<div className="wrapper-flex profile-flex ">
				<div className="profile-left">
					<img src={avt} alt="" />
				</div>
				<div className="profile-right">
					<div className="profile-item">
						<div className="profile-name">
							<span className="title-profile">Name</span>
							<input
								className="profile-userName"
								placeholder="name"
								type="text"
							/>
						</div>
						<div className="profile-email">
							<span className="title-profile">Email</span>
							<input
								className="profile-userName"
								type="email"
								placeholder="email"
							/>
						</div>
					</div>
					<div className="profile-item">
						<div className="profile-name">
							<span className="title-profile">Phone</span>
							<input
								className="profile-userName"
								type="text"
								placeholder="phone"
							/>
						</div>
						<div className="profile-email">
							<span className="title-profile">Address</span>
							<input
								className="profile-userName"
								type="email"
								placeholder="email"
							/>
						</div>
					</div>
					<div className="profile-item">
						<div className="profile-name">
							<span className="title-profile">Birth</span>
							<input
								className="profile-userName"
								type="text"
								placeholder="Date"
							/>
						</div>
						<div className="profile-email">
							<span className="title-profile">Password</span>
							<input
								className="profile-userName"
								type="password"
								placeholder="password"
							/>
						</div>
					</div>
				</div>
			</div>
			<div className="btn-update-profile">
				<button className="update-profile">Update Profile</button>
			</div>
		</div>
	);
}

export default Profile;
