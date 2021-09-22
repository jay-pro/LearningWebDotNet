import axios from "axios";
import React, { useState } from "react";
// import { Link } from "react-router-dom";
import "./forgotPassword.css";

function ForgotPassword() {
	const [email, setEmail] = useState("");
	const [sended, setSended] = useState(false);
	const url =
		"https://todoapi20210730142909.azurewebsites.net/api/Auth/forget-password";
	const sendEmail = () => {
		console.log(email);
		if (email === "") return;
		axios
			.post(
				url,
				{},
				{
					params: {
						email: email,
					},
				}
			)
			.then((res) => {
				console.log(res.data);
				setSended(true);
			})
			.catch((err) => {
				console.log(err.response);
			});
	};
	if (sended)
		return (
			<div className="form-forget">
				<div className="forget-content">
					<span className="title-forget">
						Check your email to reset your password
					</span>
				</div>
			</div>
		);
	else
		return (
			<div className="form-forget">
				<div className="forget-content">
					<span className="title-forget">Enter Email</span>
					<input
						className="input-forget"
						type="email"
						placeholder="Enter your email"
						onChange={(e) => setEmail(e.target.value)}
					/>
					<button to="" className="send-email" onClick={sendEmail}>
						Send
					</button>
				</div>
			</div>
		);
}

export default ForgotPassword;
