/* eslint-disable jsx-a11y/alt-text */
import React from "react";
import StudentJudge from "../../../../images/studentjudge.png";
import AccountUpdated from "../../../../images/accountupdated.png";
import Attendance from "../../../../images/attendance.png";
import AnswerQuestions from "../../../../images/conversation.png";
import Logout from "../../../../images/logout.png";

export const MentorData = [
  {
    title: "Account Update",
    path: "/mentor/accountupdate",
    icon: <img src={AccountUpdated} />,
    cName: "nav-text",
  },
  {
    title: "Attendance",
    path: "/mentor/attendance",
    icon: <img src={Attendance} />,
    cName: "nav-text",
  },
  {
    title: "Student Judge",
    path: "/mentor/studentjudge",
    icon: <img src={StudentJudge} />,
    cName: "nav-text",
  },
  {
    title: "Answer Questions",
    path: "/mentor/answerquestions",
    icon: <img src={AnswerQuestions} />,
    cName: "nav-text",
  },
  {
    title: "Log out",
    path: "/mentor/logout",
    icon: <img src={Logout} />,
    cName: "nav-text",
  },
];
