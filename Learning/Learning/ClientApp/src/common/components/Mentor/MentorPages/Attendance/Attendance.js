import React from "react";
import "./attendance.css";
import DataTable from "./Table";
import DatePickers from "./Datepickers";
import ComboBox from "./Combobox";

function Attendance() {
  return (
    <div className="wrapper-dashboard  ">
      <h1>LMS - Mentor - Make an attendance sheet</h1>
      {/* <CustomizedTables /> */}
      <div className="mentor-wrapper-attendance">
        <div className="title">Student Attendances</div>
        <ComboBox />
        <DatePickers />

        <div className="form">
          <div className="mentor-table-attendace">
            <DataTable />
          </div>
          <div className="inputfield">
            <input
              type="submit"
              value="Submit as attendances"
              className="btn"
            ></input>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Attendance;
