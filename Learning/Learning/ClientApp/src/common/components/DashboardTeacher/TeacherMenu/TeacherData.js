/* eslint-disable jsx-a11y/alt-text */
import React from "react";
// import Information from "../../../images/infor.png";
import Plan from "../../../../images/plan.png";
import Contact from "../../../../images/contact.png";
import Manager from "../../../../images/dashboard.png";
import Statistical from "../../../../images/statistical.png";

export const TeacherData = [
  {
    title: "Manager",
    path: "managerStudent",
    icon: <img src={Manager} />,
    cName: "nav-text",
  },

  {
    title: "Chat",
    path: "contactUser",
    icon: <img src={Contact} />,
    cName: "nav-text",
  },
  {
    title: "CheckPoint",
    path: "checkpoint",
    icon: <img src={Plan} />,
    cName: "nav-text",
  },

  {
    title: "Assignment",
    path: "assignment",
    icon: <img src={Statistical} />,
    cName: "nav-text",
  },
];
