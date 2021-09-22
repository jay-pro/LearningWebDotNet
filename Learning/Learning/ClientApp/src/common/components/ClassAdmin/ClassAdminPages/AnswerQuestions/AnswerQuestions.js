import React from "react";
import "./answerQuestions.css";
//import Blackcatavatar from "../../../../../images/blackcatavatar.png";
//import ClassAdminchat from "../../../../../images/mentorchat.png";

function AnswerQuestions() {
  return (
    <div className="wrapper-dashboard">
      <h1>LMS - Class Admin - Answer Questions</h1>
      <div className="classadmin-form-contact">
        {/* <img className="classadmin-chat-img" alt="" src={ClassAdminchat} /> */}
        <div className="classadmin-form-chat">
          {/* <div className="classadmin-chat-content">
            <div className="classadmin-chat-classadmin">
              <img className="classadmin-ask-img" alt="" src={Blackcatavatar} />
              <span>
                Can I ask you some questions about the coming HTML course?
              </span>
            </div>
          </div> */}
          <div className="classadmin-chat-bar">
            <input
              className="classadmin-chat-input"
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
