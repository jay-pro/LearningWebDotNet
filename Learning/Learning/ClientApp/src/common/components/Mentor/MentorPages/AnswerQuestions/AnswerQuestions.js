import React from "react";
import "./answerQuestions.css";
//import Blackcatavatar from "../../../../../images/blackcatavatar.png";
//import Mentorchat from "../../../../../images/mentorchat.png";

function AnswerQuestions() {
  return (
    <div className="wrapper-dashboard">
      <h1>LMS - Mentor - Answer Questions</h1>
      <div className="mentor-form-contact">
        {/*         <img className="mentor-chat-img" alt="" src={Mentorchat} />
         */}{" "}
        <div className="mentor-form-chat">
          {/* <div className="mentor-chat-content">
            <div className="mentor-chat-mentor">
              <img className="mentor-ask-img" alt="" src={Blackcatavatar} />
              <span>
                Can I ask you some questions about the coming HTML course?
              </span>
            </div>
  </div> */}
          <div className="mentor-chat-bar">
            <input
              className="mentor-chat-input"
              type="text"
              placeholder="Type here..."
            ></input>
            <button className="search-btn chat-margin">Send</button>
          </div>
        </div>
      </div>
    </div>
  );
}
export default AnswerQuestions;
