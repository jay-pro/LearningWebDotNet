/* eslint-disable jsx-a11y/alt-text */
import React, { useState } from "react";
import { useHistory } from "react-router-dom";

import "./Login.css";
import Facebookimage from "../../../images/facebook.png";
import Googleplusimage from "../../../images/google-plus.png";
import Pinterestimage from "../../../images/pinterest.png";
import Twitterimage from "../../../images/twitter.png";
import Youtubeimage from "../../../images/youtube.png";
import Login_img from "../../../images/login_img.png";
import { login } from "../../Service/AuthService";
import { Link } from "react-router-dom";

function Login() {
	const [userName, setUserName] = useState();
	const [password, setPassword] = useState();
	const history = useHistory();

	const onSubmit = async (e) => {
		const formData = { userName, password };
		e.preventDefault();
		try {
			const res = await login(formData);
			console.log(res);
			history.push("/");
		} catch (err) {
			console.log(err);
		}
		// axios({
		// 	method: "POST",
		// 	url: "https://todoapi20210730142909.azurewebsites.net/api/Auth/login",
		// 	data: {
		// 		useName: userName,
		// 		password: password,
		// 	},
		// })
		// 	.then((res) => {
		// 		console.log(res);
		// 	})
		// 	.catch((err) => {
		// 		console.log(err);
		// 	});
		// axios
		// 	.post("https://todoapi20210730142909.azurewebsites.net/api/Auth/login", {
		// 		userName: userName,
		// 		password: password,
		// 	})
		// 	.then((res) => {
		// 		// alert(res.data.message);
		// 		console.log(res.data);
		// 	})
		// 	.catch((err) => {
		// 		console.log(err);
		// 	});
		// AuthService.login(userName, password).then(() => {
		// 	console.log("thanh cong");
		// },
		// 	(error) => {
		// 	const resMessage =
		// 		(error.response &&
		// 			error.response.data &&
		// 			error.response.data.message) ||
		// 		error.message ||
		// 		error.toString();
		// 	console.log("shit" + error.message);
		// 	alert(error.response.message);
		// })
	};
	return (
		<div className="container-signup">
			<div className="SignUp">
				<section>
					<div className="imgBx">
						<img src={Login_img} />
					</div>
					<div className="contentBx">
						<div className="formBx">
							<h2>Login</h2>
							<form>
								<div className="inputBx">
									<span>Username</span>
									<input
										type="text"
										onChange={(e) => setUserName(e.target.value)}
									/>
								</div>
								<div className="inputBx">
									<span>Password</span>
									<input
										type="password"
										onChange={(e) => setPassword(e.target.value)}
									/>
								</div>
								<div className="forgotPassword">
									<Link to="/forget">Forgot Password</Link>
								</div>
								<div className="inputBx input-center">
									<button
										className="buttonLogin"
										type="submit"
										defaultValue="Sign in"
										onClick={onSubmit}
									>
										Login
									</button>
								</div>
								<div className="inputBx">{/* <Button/> */}</div>
							</form>
							<h3>Login with social media</h3>
							<ul className="sci">
								<li>
									<img src={Facebookimage} />
								</li>
								<li>
									<img src={Googleplusimage} />
								</li>
								<li>
									<img src={Pinterestimage} />
								</li>
								<li>
									<img src={Twitterimage} />
								</li>
								<li>
									<img src={Youtubeimage} />
								</li>
							</ul>
						</div>
					</div>
				</section>
			</div>
		</div>
	);
}

export default Login;
