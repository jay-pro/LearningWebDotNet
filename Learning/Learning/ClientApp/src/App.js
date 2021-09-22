import React from "react";
import { BrowserRouter as Router, Route } from "react-router-dom";
import { Switch } from "react-router-dom";
import "./App.css";
import Home from "./common/components/Home/Home";
import SignUp from "./common/components/SignUp/SignUp";
import Login from "./common/components/Login/login";
import Detail from "./common/components/DetailProfile/DetailProfile";
import NavbarRoute from "./common/components/Dashboard/NavbarRoute";
import Search from "./common/components/Search/Search";
import SystemAdminRouters from "./common/components/SystemAdmin/SystemAdminRouters/SystemAdminRouters";
import MentorRouters from "./common/components/Mentor/MentorRouters/MentorRouters";
import ClassAdminRouters from "./common/components/ClassAdmin/ClassAdminRouters/ClassAdminRouters";
import TeacherRoute from "./common/components/DashboardTeacher/TeacherMenu/TeacherRoute";
import InstructorRoute from "./common/components/Instructor/InstructorMenu/InstructorRoute";
import ForgotPassword from "./common/components/ForgotPassword/ForgotPassword";
import ModalCourse from "./common/components/DetailCourse/ModalCourse";
import DetailCourse from "./common/components/DetailCourse/DetailCourse";
import ResetPassword from "./common/components/ResetPassword/ResetPassword";
import ClassCourse from "./common/components/ClassCourse/ClassCourse";
import CourseManage from "./common/components/SystemAdmin/SystemAdminPages/CourseManage/CourseManage";
import RegisteringResult from "./common/components/RegisteredClass/RegisteringResult";
import { PrivateRoute } from "./common/Routes/PrivateRoute.";
import { Role } from "./common/Routes/Role";
import ClassTeacher from "./common/components/DashboardTeacher/PageTeacher/ClassTeacher/ClassTeacher";
// import ManagerClass from "./common/components/Instructor/PageInstructor/CourseInstructor/ManagerClass";

function App() {
	return (
		<Router>
			<div className="App">
				<Switch>
					<Route exact path="/" component={Home} />
					<Route path="/login" component={Login} />
					<Route path="/forget" component={ForgotPassword} />
					<Route
						path="/reset-password/:userId/:token"
						component={ResetPassword}
					/>
					<Route path="/signup" component={SignUp} />
					<Route path="/profile" component={Detail} />
					<Route path="/account/detail" component={Detail} />
					<Route exact path="/Course/study" component={ClassCourse} />
					<Route exact path="/course/study/:id" component={ModalCourse} />
					<Route exact path="/course/detail/:id" component={DetailCourse} />
					<Route exact path="/classTeacher" component={ClassTeacher} />
					<Route path="/registeredclass" component={RegisteringResult} />

					<Route
						exact
						path="/systemadmin/coursemanage/:id"
						component={CourseManage}
					/>
					<PrivateRoute
						path="/users/:id"
						// render={() => (
						//   <div>
						//     <NavbarRoute />
						//   </div>
						// )}
						component={NavbarRoute}
					/>

					<Route
						exact
						path="/systemadmin/coursemanage/:id"
						component={CourseManage}
					/>
					<PrivateRoute path="/users/:id" component={NavbarRoute} />

					<PrivateRoute
						path="/systemadmin"
						roles={[Role.SystemAdmin]}
						component={SystemAdminRouters}
					/>
					<PrivateRoute
						path="/mentor"
						roles={[Role.Mentor]}
						component={MentorRouters}
					/>
					<PrivateRoute
						path="/teacher/:id/:idClass"
						roles={[Role.Teacher]}
						component={TeacherRoute}
					/>
					<PrivateRoute
						path="/instructor"
						roles={[Role.Instructor]}
						component={InstructorRoute}
					/>
					<PrivateRoute
						path="/classadmin"
						roles={[Role.ClassAdmin]}
						component={ClassAdminRouters}
					/>
					<Route path="/search" component={Search} />
					<Route path="/course/detail/:id" component={DetailCourse} />
					<Route path="/course/study/:id" component={ModalCourse} />
				</Switch>
			</div>
		</Router>
	);
}
export default App;
