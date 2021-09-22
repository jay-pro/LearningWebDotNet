/* eslint-disable no-use-before-define */
import React from "react";
import TextField from "@material-ui/core/TextField";
import Autocomplete from "@material-ui/lab/Autocomplete";

export default function ComboBox() {
  return (
    <Autocomplete
      id="combo-box-demo"
      options={courses}
      getOptionLabel={(option) => option.courseName}
      style={{ width: 300 }}
      renderInput={(params) => (
        <TextField {...params} label="Choose a Course" variant="outlined" />
      )}
    />
  );
}

const courses = [
  { courseName: "Learn HTML5 Programming From Scratch" },
  { courseName: "HTML & CSS Tutorial and Projects Course" },
  { courseName: "React Tutorial and Projects Course" },
  { courseName: "Full-Stack Web Development For Beginners" },
  { courseName: "Intro To PHP For Web Developement" },
  { courseName: "Building web APIs with Rust (beginners)" },
  { courseName: "The Complete ASP.NET MVC 5 Course" },
  { courseName: "Design Patterns in C# and .NET" },
  { courseName: "Introduction to Microsoft Azure IOT" },
];
