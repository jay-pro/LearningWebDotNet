import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import ManagerCourse from "../PageInstructor/ManagerCourse/ManagerCourse";
import ManagerLesson from "../PageInstructor/ManagerLesson/ManagerLesson";
import ManagerTeacher from "../PageInstructor/ManagerTeacher/ManagerTeacher";
import InstructorMenu from "./InstructorMenu";
import StatisticalInstructor from "../PageInstructor/StatisticalInstructor/StatisticalInstructor";
import CourseInstructor from "../PageInstructor/CourseInstructor/CourseInstructor";
import ManagerClass from "../PageInstructor/ManagerClass/ManagerClass";
import CourseLesson from "../PageInstructor/CourseLesson/CourseLesson";

function InstructorRoute() {
	return (
		<Router>
			<InstructorMenu />
			<Switch>
				<Route
					path="/instructor/ManagerCourse"
					exact
					component={ManagerCourse}
				/>
				<Route path="/instructor/Course" exact component={CourseInstructor} />
				<Route
					path="/instructor/ManagerLesson"
					exact
					component={ManagerLesson}
				/>
				<Route
					path="/instructor/ManagerTeacher"
					exact
					component={ManagerTeacher}
				/>
				<Route
					path="/instructor/StatisticalInstructor"
					exact
					component={StatisticalInstructor}
				/>
				<Route path="/instructor/ManagerClass" exact component={ManagerClass} />
				<Route path="/instructor/CourseLesson" exact component={CourseLesson} />
			</Switch>
		</Router>
	);
}
export default InstructorRoute;
