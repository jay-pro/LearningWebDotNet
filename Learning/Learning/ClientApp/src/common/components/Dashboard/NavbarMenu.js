import React, { useState } from "react";
import { Link } from "react-router-dom";
import * as AiIcons from "react-icons/ai";
import { DashboardData } from "./DashboardData";
import btnHome from "../../../images/home.png";

import "./Dashboard.css";

import { IconContext } from "react-icons";

function NavbarMenu({ id }) {
	const [sidebar, setSidebar] = useState(false);
	const showSidebar = () => setSidebar(!sidebar);

	return (
		<div className="box">
			<div className="nav-menu-header">
				<div className="dashboard-header">
					<img className="dashboard-header-img" src={btnHome} alt="" />
				</div>
			</div>
			<IconContext.Provider value={{ color: "#d5d5d5" }}>
				<nav className={sidebar ? "nav-menu active" : "nav-menu"}>
					<ul className="nav-menu-items">
						<li className="navbar-toggle">
							<Link to="#" className="menu-bars">
								<AiIcons.AiOutlineMenu onClick={showSidebar} />
							</Link>
						</li>
						{DashboardData.map((item, index) => {
							return (
								<li key={index} className={item.cName}>
									<Link to={`/users/${id}/${item.path}`}>
										<div className="item-icon">{item.icon}</div>
										<span> {item.title}</span>
									</Link>
								</li>
							);
						})}
					</ul>
				</nav>
			</IconContext.Provider>
		</div>
	);
}

export default NavbarMenu;
