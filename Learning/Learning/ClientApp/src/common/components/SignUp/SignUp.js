/* eslint-disable jsx-a11y/alt-text */
import React, { useState } from "react";
import "../../../App.css";
import SignUpImg from "../../../images/SignUp.png";
import CalenderImg from "../../../images/Calender.png";
import PointImg from "../../../images/poin.png";
import axios from "axios";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { Link, Redirect } from "react-router-dom";

import "./signUp.css";

toast.configure();

function SignUp() {
	const url =
		"https://todoapi20210730142909.azurewebsites.net/api/Auth/register-student";
	const [name, setName] = useState("");
	const [email, setEmail] = useState("");
	const [password, setPassword] = useState("");
	const [confirm, setConfirm] = useState("");
	const [error, setError] = useState("");
	const [success, setSuccess] = useState(false);

	function onSignUp() {
		if (name === "" || email === "" || password === "") {
			setError("Please enter your info");
			return;
		}
		if (password !== confirm) {
			setError("Passwords did not match");
			return;
		}
		axios
			.post(url, {
				userName: name,
				email: email,
				password: password,
			})
			.then((res) => {
				console.log(res.data);
				setSuccess(true);
				toast.success("success");
			})
			.catch((err) => {
				console.log(err.response.data.data);
				setError(err.response.data.data[0].description);
			});
	}
	if (success) {
		return <Redirect to="./login" />;
	} else
		return (
			<>
				<div className="container-signup">
					<div className="SignUp">
						<div className="SignUp-left">
							<div className="btn-LogIn">
								<button className="button-login">LOGIN</button>
							</div>
							<div className="wrapper-signup">
								<h1>SIGN UP</h1>
								<div className="hadding">
									<div className="fill-users">
										<span className="text-SignUp">Name</span>
										<input
											onChange={(e) => setName(e.target.value)}
											type="text"
											placeholder="Enter Name"
											className="text-input"
										/>
									</div>
									<div className="fill-email">
										<span className="text-SignUp">Email</span>
										<input
											onChange={(e) => setEmail(e.target.value)}
											type="email"
											placeholder="Enter Email"
											className="text-input"
										/>
									</div>
									<div className="fill-password">
										<span className="text-SignUp">Password</span>
										<input
											onChange={(e) => setPassword(e.target.value)}
											type="password"
											placeholder="Enter Password"
											className="text-input"
										/>
									</div>
									<div className="confirm-password">
										<span className="text-SignUp">Confirm password</span>
										<input
											onChange={(e) => setConfirm(e.target.value)}
											type="password"
											placeholder="Enter Confirm Password"
											className="text-input"
										/>
									</div>
								</div>
								<div>
									<span className="error">{error}</span>
								</div>
								<div>
									<span className="error"></span>
								</div>
							</div>
							{/* <Button onClick={submit} /> */}
							<div className="btn-SignUp">
								<Link
									className="button-signup"
									to="./signup"
									onClick={onSignUp}
								>
									SIGN UP
								</Link>
							</div>
						</div>
						<div className="SignUp-right">
							<div className="overlay">
								<img className="img-SignUp" src={SignUpImg} />
								<img className="img-Calender" src={CalenderImg} />
								<img className="img-Point" src={PointImg} />
							</div>
						</div>
					</div>
				</div>
			</>
		);
}

export default SignUp;
