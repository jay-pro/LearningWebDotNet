import * as React from "react";
import { DataGrid } from "@material-ui/data-grid";

const columns = [
  { field: "id", headerName: "ID", width: 90 },
  {
    field: "studentName",
    headerName: "Student Name",
    width: 150,
    editable: true,
  },
  {
    field: "className",
    headerName: "Class Name",
    width: 150,
    editable: true,
  },
  {
    field: "age",
    headerName: "Age",
    type: "number",
    width: 110,
    editable: true,
  },
  {
    field: "reportContent",
    headerName: "Report Content",
    width: 150,
    editable: true,
  },
];

const rows = [
  {
    id: 1,
    className: "HTML_01",
    studentName: "Cong Chien",
    age: 21,
    reportContent: "Can't submit assignment.",
  },
  {
    id: 2,
    className: "HTML_01",
    studentName: "Van Hoan",
    age: 21,
    reportContent: "The notification wasn't directed to my email.",
  },
  {
    id: 3,
    className: "HTML_01",
    studentName: "Ha Nhi",
    age: 21,
    reportContent: "Can't join class HTML_01.",
  },
  {
    id: 4,
    className: "HTML_01",
    studentName: "Anh Quoc",
    age: 21,
    reportContent: "My name in profile is wrong.",
  },
  {
    id: 5,
    className: "HTML_01",
    studentName: "Hoai Thu",
    age: 21,
    reportContent: "Can't submit assignment.",
  },
];

export default function DataTable() {
  return (
    <div style={{ height: 400, width: "100%" }}>
      <DataGrid
        rows={rows}
        columns={columns}
        pageSize={5}
        checkboxSelection
        disableSelectionOnClick
      />
    </div>
  );
}
