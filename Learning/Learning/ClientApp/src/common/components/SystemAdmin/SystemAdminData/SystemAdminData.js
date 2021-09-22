/* eslint-disable jsx-a11y/alt-text */
import React from "react";
import Statistic from "../../../../images/statistic.png";
import CourseManage from "../../../../images/onlinecourse.png";
import UserManage from "../../../../images/user.png";
import Logout from "../../../../images/logout.png";

export const SystemAdminData = [
  {
    title: "Course Manage",
    path: "/systemadmin/coursemanage",
    icon: <img src={CourseManage} />,
    cName: "nav-text",
  },
  {
    title: "User Manage",
    path: "/systemadmin/usermanage",
    icon: <img src={UserManage} />,
    cName: "nav-text",
  },
  {
    title: "Statistic",
    path: "/systemadmin/statistic",
    icon: <img src={Statistic} />,
    cName: "nav-text",
  },
  {
    title: "Log out",
    path: "/systemadmin/logout",
    icon: <img src={Logout} />,
    cName: "nav-text",
  },
];
