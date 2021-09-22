import React, { useState, useEffect } from "react";
import "./Home.css";
import Logo from "../../../images/logo.png";
// import { BrowserRouter as Router, Link } from "react-router-dom";
import { Link } from "react-router-dom";
import {
	getCurrentUser,
	logout,
	getCurrentRole,
} from "../../Service/AuthService";
function Header() {
	const [currentUser, setCurrentUser] = useState(null);
	const user = getCurrentUser();
	const roleUser = getCurrentRole();

	useEffect(() => {
		if (user) {
			setCurrentUser(user);
		} // 	getCurrentUser() {
		// return JSON.parse(sessionStorage.getItem("aud"));
	}, [user]);
	const logOut = async () => {
		await logout();
		setCurrentUser(null);
	};
	return (
		<div className="home-header">
			<div className="header-logo">
				<Link to="/">
					<img src={Logo} alt="" className="header-logo-image" />
				</Link>
			</div>
			{roleUser === "Student" ? (
				<ul className="header-menu">
					<li className="header-menu-item">
						<Link to="/" className="header-menu-link">
							Home
						</Link>
					</li>
					<li className="header-menu-item">
						<Link to="/search" className="header-menu-link">
							Course
						</Link>
					</li>
					<li className="header-menu-item">
						<Link to="/registeredclass" className="header-menu-link">
							My Class
						</Link>
					</li>
					<li className="header-menu-item">
						<Link to="/account/detail" className="header-menu-link">
							Account
						</Link>
					</li>
				</ul>
			) : roleUser === "Teacher" ? (
				<ul className="header-menu">
					<li className="header-menu-item">
						<Link to="/" className="header-menu-link">
							Home
						</Link>
					</li>
					<li className="header-menu-item">
						<Link to="/classTeacher" className="header-menu-link">
							Course Teacher
						</Link>
					</li>

					<li className="header-menu-item">
						<Link to="/account/detail" className="header-menu-link">
							Account
						</Link>
					</li>
				</ul>
			) : roleUser === "Instructor" ? (
				<ul className="header-menu">
					<li className="header-menu-item">
						<Link to="/" className="header-menu-link">
							Home
						</Link>
					</li>

					<li className="header-menu-item">
						<Link to="/instructor" className="header-menu-link">
							Dashboard
						</Link>
					</li>
					<li className="header-menu-item">
						<Link to="/account/detail" className="header-menu-link">
							Account
						</Link>
					</li>
				</ul>
			) : roleUser === "SystemAdmin" ? (
				<ul className="header-menu">
					<li className="header-menu-item">
						<Link to="/" className="header-menu-link">
							Home
						</Link>
					</li>
					<li className="header-menu-item">
						<Link to="/systemadmin" className="header-menu-link">
							Dashboard
						</Link>
					</li>
					<li className="header-menu-item">
						<Link to="/account/detail" className="header-menu-link">
							Account
						</Link>
					</li>
				</ul>
			) : (
				<ul className="header-menu">
					<li className="header-menu-item">
						<Link to="/" className="header-menu-link">
							Home
						</Link>
					</li>
					<li className="header-menu-item">
						<Link to="/search" className="header-menu-link">
							Course
						</Link>
					</li>
				</ul>
			)}

			{currentUser ? (
				<div className="header-auth">
					<Link to="/" className="button button--transparent">
						{currentUser}
					</Link>
					<Link onClick={logOut} to="" className="button button--primary">
						Log out
					</Link>
				</div>
			) : (
				<div className="header-auth">
					<Link to="/login" className="button button--transparent">
						Log in
					</Link>
					<Link to="/signup" className="button button--primary">
						Sign Up
					</Link>
				</div>
			)}
		</div>
	);
}

export default Header;
