/* eslint-disable jsx-a11y/alt-text */
import React from "react";
// import Information from "../../../images/infor.png";
import Score from "../../../images/score.png";
import Submit from "../../../images/submit.png";
import Contact from "../../../images/contact.png";
import Forum from "../../../images/forum.png";
import Feedback from "../../../images/feedback.png";
import Statistical from "../../../images/statistical.png";
import reading from "../../../images/reading.png";

export const DashboardData = [
	{
		title: "Learn",
		path: "learn",
		icon: <img src={reading} />,
		cName: "nav-text",
	},
	{
		title: "Point",
		path: "point",
		icon: <img src={Score} />,
		cName: "nav-text",
	},
	{
		title: "Forum",
		path: "forum",
		icon: <img src={Forum} />,
		cName: "nav-text",
	},

	{
		title: "Submit",
		path: "submit",
		icon: <img src={Submit} />,
		cName: "nav-text",
	},
	{
		title: "Feedback",
		path: "feedback",
		icon: <img src={Feedback} />,
		cName: "nav-text",
	},
	{
		title: "Statistical",
		path: "statistical",
		icon: <img src={Statistical} />,
		cName: "nav-text",
	},
];
