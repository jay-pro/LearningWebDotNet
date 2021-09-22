import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import NavbarMenu from "./NavbarMenu";
// import CourseInfo from "./Page/CourseInformation";
import Forum from "./Page/Forum/Forum";
import Contact from "./Page/Contact/Contact";
import Submit from "./Page/Submit/Submit";
import Feedback from "./Page/Feedback/Feedback";
import Statistical from "./Page/Statistical/Statistical";
import Point from "./Page/Point/Point";
import Learn from "./Page/Learn/Learn";
import { useParams } from "react-router";

function NavbarRoute() {
	const { id } = useParams();
	console.log(id + "123");
	return (
		<Router>
			<NavbarMenu id={id} />
			<Switch>
				<Route path="/users/:id/learn" exact component={Learn} />
				<Route path="/users/:id/forum" exact component={Forum} />
				<Route path="/users/:id/cotact" exact component={Contact} />
				<Route path="/users/:id/submit" exact component={Submit} />
				<Route path="/users/:id/feedback" exact component={Feedback} />
				<Route path="/users/:id/statistical" exact component={Statistical} />
				<Route path="/users/:id/point" exact component={Point} />
			</Switch>
		</Router>
	);
}
export default NavbarRoute;
