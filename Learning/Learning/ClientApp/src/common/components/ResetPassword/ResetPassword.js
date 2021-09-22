import axios from "axios";
import React, { useState } from "react";
import { useParams } from "react-router-dom";
// import { Link } from "react-router-dom";
import "./resetPassword.css";

function ResetPassword() {
	const [newPassword, setNewPassword] = useState("")
	const [confirm, setConfirm] = useState("")
	const [error, setError] = useState("")
	const params = useParams();
	const url =
		"https://todoapi20210730142909.azurewebsites.net/api/Auth/reset-password";
	 const Reset = () => {
		if(newPassword ===""){
			setError("Please enter your password");
			return;
	 	}
		if(confirm !== newPassword){
			setError("Passwords did not match");
			return;
		}
		axios
			.post(url,{},{params: {
				userId: params.userId,
				token: params.token,
				newPassword: newPassword
			}})
			.then((res) => {
				console.log(res.data)
				alert("Success");
			})
			.catch((err) => {
				console.log(err.response.data.data);
				setError(err.response.data.data)
			});
	}
	return (
		<div>
			<div className="form-forget">
			<div className="forget-content">
				<span className="title-forget">Reset password</span>
				<input
					className="input-forget"
					type="password"
					placeholder="Enter your new password"
					onChange={(e) => setNewPassword(e.target.value)}
				/>				
				<input
					className="input-forget"
					type="password"
					placeholder="Confirm your new password"
					onChange={(e) => setConfirm(e.target.value)}
				/>	
				<label className="error-label">{error}</label>
				<button to="" className="send-email" onClick={Reset}>
					Change password
				</button>
			</div>
		</div>
		</div>
		
	);
}

export default ResetPassword;
