import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import ManagerStudent from "../PageTeacher/ManagerStudent/ManagerStudent";
import ContactUser from "../PageTeacher/ContactUser/ContactUser";
import Assignment from "../PageTeacher/Assignment/Assignment";
import TeacherMenu from "./TeacherMenu";
import CheckPoint from "../PageTeacher/CheckPoint/CheckPoint";
import { useParams } from "react-router";

function TeacherRoute() {
  const { id } = useParams();
  const { idClass } = useParams();
  console.log(idClass);
  console.log(id);
  return (
    <Router>
      <TeacherMenu id={id} idClass={idClass} />
      <Switch>
        <Route
          exact
          path="/teacher/:id/:idClass/managerStudent"
          component={ManagerStudent}
        />
        <Route
          exact
          path="/teacher/:id/:idClass/contactUser"
          component={ContactUser}
        />
        <Route
          exact
          path="/teacher/:id/:idClass/checkpoint"
          component={CheckPoint}
        />
        <Route
          exact
          path="/teacher/:id/:idClass/assignment"
          component={Assignment}
        />
      </Switch>
    </Router>
  );
}
export default TeacherRoute;
