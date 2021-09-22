import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import MentorMenu from "../MentorMenu/MentorMenu";
import AccountUpdate from "../MentorPages/AccountUpdate/AccountUpdate";
import AnswerQuestions from "../MentorPages/AnswerQuestions/AnswerQuestions";
import Attendance from "../MentorPages/Attendance/Attendance";
import StudentJudge from "../MentorPages/StudentJudge/StudentJudge";
import AlertDialogSlide from "../MentorPages/Logout/Logout";

function MentorRouters() {
  return (
    <Router>
      <MentorMenu />
      <Switch>
        {/* <Route path="/users/course-information" exact component={CourseInfo} /> */}
        <Route path="/mentor/accountupdate" exact component={AccountUpdate} />
        <Route
          path="/mentor/answerquestions"
          exact
          component={AnswerQuestions}
        />
        <Route path="/mentor/attendance" exact component={Attendance} />
        <Route path="/mentor/studentjudge" exact component={StudentJudge} />
        <Router path="/mentor/logout" excact component={AlertDialogSlide} />
      </Switch>
    </Router>
  );
}
export default MentorRouters;
