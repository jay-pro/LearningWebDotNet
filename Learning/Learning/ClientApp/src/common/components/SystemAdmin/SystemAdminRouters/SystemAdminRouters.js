import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import SystemAdminMenu from "../SystemAdminMenu/SystemAdminMenu";
import CourseManage from "../SystemAdminPages/CourseManage/CourseManage";
import Statistic from "../SystemAdminPages/Statistic/Statistic";
import AlertDialogSlide from "../SystemAdminPages/Logout/Logout";
import UserManage from "../SystemAdminPages/UserManage/UserManage";

function SystemAdminRouters() {
  return (
    <Router>
      <SystemAdminMenu />
      <Switch>
        <Route
          path="/systemadmin/coursemanage"
          exact
          component={CourseManage}
        />
        <Route path="/systemadmin/usermanage" exact component={UserManage} />
        <Route path="/systemadmin/statistic" exact component={Statistic} />
        <Route path="/systemadmin/logout" exact component={AlertDialogSlide} />
      </Switch>
    </Router>
  );
}
export default SystemAdminRouters;
