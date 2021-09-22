/* eslint-disable jsx-a11y/alt-text */
import React from "react";
import AccountUpdated from "../../../../images/accountupdated.png";
import Notify from "../../../../images/notification.png";
import AnswerQuestions from "../../../../images/conversation.png";
import Logout from "../../../../images/logout.png";
import Report from "../../../../images/report.png";

export const ClassAdminData = [
  {
    title: "Account Update",
    path: "/classadmin/accountupdate",
    icon: <img src={AccountUpdated} />,
    cName: "nav-text",
  },
  {
    title: "Answer Questions",
    path: "/classadmin/answerquestions",
    icon: <img src={AnswerQuestions} />,
    cName: "nav-text",
  },
  {
    title: "Summarize reports",
    path: "/classadmin/summarizereports",
    icon: <img src={Report} />,
    cName: "nav-text",
  },
  {
    title: "Notify students",
    path: "/classadmin/notifystudents",
    icon: <img src={Notify} />,
    cName: "nav-text",
  },
  {
    title: "Log out",
    path: "/classadmin/logout",
    icon: <img src={Logout} />,
    cName: "nav-text",
  },
];
