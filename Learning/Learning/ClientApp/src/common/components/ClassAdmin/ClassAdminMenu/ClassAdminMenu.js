import React, { useState } from "react";
import { Link } from "react-router-dom";
import * as AiIcons from "react-icons/ai";
import { ClassAdminData } from "../ClassAdminData/ClassAdminData";
import btnHome from "../../../../images/home.png";
import bell from "../../../../images/notification.png";
import avt from "../../../../images/slide1.jpg";

import { IconContext } from "react-icons";

function ClassAdminMenu() {
  const [sidebar, setSidebar] = useState(false);
  const showSidebar = () => setSidebar(!sidebar);

  return (
    <div className="box">
      <div className="nav-menu-header">
        <div className="dashboard-header">
          <img className="dashboard-header-img" src={btnHome} alt="" />
        </div>
        <div className="dashboard-header-event">
          <img className="event-bell" src={bell} alt="" />
          <img className="event-avt" src={avt} alt="" />
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
            {ClassAdminData.map((item, index) => {
              return (
                <li key={index} className={item.cName}>
                  <Link to={item.path}>
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

export default ClassAdminMenu;
