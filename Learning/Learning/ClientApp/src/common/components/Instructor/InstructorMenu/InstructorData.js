/* eslint-disable jsx-a11y/alt-text */
import React from "react";
import ManagerCourse from "../../../../images/managercourse.png";
import ManagerLesson from "../../../../images/managerlesson.png";
import Course from "../../../../images/Course.png";
import ManagerTeacher from "../../../../images/managementteacher.png";
import statiscal from "../../../../images/statistical.png";
import video from "../../../../images/video.png";
import classManage from "../../../../images/class.png";

export const InstructorData = [
	{
		title: "Create Course ",
		path: "/instructor/ManagerCourse",
		icon: <img src={ManagerCourse} />,
		cName: "nav-text",
	},
	{
		title: "Lesson Manage",
		path: "/instructor/CourseLesson",
		icon: <img src={video} />,
		cName: "nav-text",
	},
	{
		title: "Class Manage",
		path: "/instructor/ManagerClass",
		icon: <img src={classManage} />,
		cName: "nav-text",
	},
	{
		title: "Quizz ",
		path: "/instructor/ManagerLesson",
		icon: <img src={ManagerLesson} />,
		cName: "nav-text",
	},
	{
		title: "Course Manage",
		path: "/instructor/Course",
		icon: <img src={Course} />,
		cName: "nav-text",
	},
	{
		title: "Teacher Manage ",
		path: "/instructor/ManagerTeacher",
		icon: <img src={ManagerTeacher} />,
		cName: "nav-text",
	},
	{
		title: "Statistical",
		path: "/instructor/StatisticalInstructor",
		icon: <img src={statiscal} />,
		cName: "nav-text",
	},
];
