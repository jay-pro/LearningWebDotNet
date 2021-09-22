import React from "react";
import "./studentJudge.css";
import DataTable from "./Table";
import ComboBox from "./Combobox";

function StudentJudge() {
  return (
    <div className="wrapper-dashboard  ">
      <h1>LMS - Mentor - Student Judge</h1>
      {/* <CustomizedTables /> */}
      <div className="mentor-wrapper-studentjudge">
        <div className="title">Judging students about completions</div>
        <ComboBox />

        <div className="form">
          <div className="mentor-table-studentjudge">
            <DataTable />
          </div>
          <div className="inputfield">
            <input type="submit" value="A+" className="btn"></input>
            <input type="submit" value="A" className="btn"></input>
            <input type="submit" value="A-" className="btn"></input>
            <input type="submit" value="B" className="btn"></input>
            <input type="submit" value="B-" className="btn"></input>
            <input type="submit" value="C" className="btn"></input>
            <input type="submit" value="C-" className="btn"></input>
            <input type="submit" value="D" className="btn"></input>
            <input type="submit" value="D" className="btn"></input>
          </div>
        </div>
      </div>
    </div>
  );
}

export default StudentJudge;
