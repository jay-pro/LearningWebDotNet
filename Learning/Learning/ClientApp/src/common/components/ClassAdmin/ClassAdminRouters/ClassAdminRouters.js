import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import ClassAdminMenu from "../ClassAdminMenu/ClassAdminMenu";
import AccountUpdate from "../ClassAdminPages/AccountUpdate/AccountUpdate";
import AnswerQuestions from "../ClassAdminPages/AnswerQuestions/AnswerQuestions";
import NotifyStudents from "../ClassAdminPages/NotifyStudents/NotifyStudents";
import SummarizeReports from "../ClassAdminPages/SummarizeReports/SummarizeReports";
import AlertDialogSlide from "../ClassAdminPages/Logout/Logout";

function ClassAdminRouters() {
  return (
    <Router>
      <ClassAdminMenu />
      <Switch>
        {/* <Route path="/users/course-information" exact component={CourseInfo} /> */}
        <Route
          path="/classadmin/accountupdate"
          exact
          component={AccountUpdate}
        />
        <Route
          path="/classadmin/answerquestions"
          exact
          component={AnswerQuestions}
        />
        <Route
          path="/classadmin/summarizereports"
          exact
          component={SummarizeReports}
        />
        <Route
          path="/classadmin/notifystudents"
          exact
          component={NotifyStudents}
        />
        <Route path="/classadmin/logout" exact component={AlertDialogSlide} />
      </Switch>
    </Router>
  );
}
export default ClassAdminRouters;
