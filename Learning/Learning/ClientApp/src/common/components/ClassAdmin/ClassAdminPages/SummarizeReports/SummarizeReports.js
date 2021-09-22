import React from "react";
import "./summarizeReports.css";
import DataTable from "./Table";
import DatePickers from "./Datepicker";

function SummarizeReports() {
  return (
    <div className="wrapper-dashboard  ">
      <h1>LMS - Class Admin - Make a reportment sheet</h1>
      {/* <CustomizedTables /> */}
      <div className="classadmin-wrapper-attendance">
        <div className="title">Student Reports</div>
        <DatePickers />

        <div className="form">
          <div className="classadmin-table-attendace">
            <DataTable />
          </div>
          <div className="inputfield">
            <input type="submit" value="Accept report" className="btn"></input>
            <input type="submit" value="Confuse report" className="btn"></input>
          </div>
        </div>
      </div>
    </div>
  );
}

export default SummarizeReports;
