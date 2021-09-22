import React from "react";
import { DatePicker, Space } from "antd";
import "./notifyStudents.css";

function NotifyStudents() {
  function onChange(date, dateString) {
    console.log(date, dateString);
  }

  return (
    <div className="wrapper-dashboard  ">
      <h1>LMS - Class Admin - Notify Students</h1>
      <div className="classadmin-wrapper">
        <div className="title">Make a notification</div>
        <div className="form-notification">
          <div>
            <Space direction="vertical">
              <DatePicker onChange={onChange} />
            </Space>
          </div>
          <input
            className="input-notification"
            type="text"
            placeholder="Title"
          ></input>
          <input
            className="input-notification"
            type="text"
            placeholder="Title"
          ></input>

          <input
            className="input-notification"
            type="text"
            placeholder="To"
          ></input>
        </div>
        <div className="btn-save-notify">
          <button className="button-notify">Save</button>
        </div>
      </div>
    </div>
  );
}

export default NotifyStudents;
