import React, { useState } from "react";
import axios from "axios";
import "./accountUpdate.css";

function AccountUpdate() {
  const url = "https://todoapi20210730142909.azurewebsites.net/api/Users";

  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmpassword, setConfirmPassword] = useState("");
  const [phone, setPhone] = useState("");
  const [birth, setBirth] = useState("");
  const [address, setAddress] = useState("");
  const [gender, setGender] = useState("");

  function onSubmit() {
    console.log([
      name,
      email,
      password,
      confirmpassword,
      phone,
      birth,
      address,
      gender,
    ]);
    axios
      .post(url, {
        userName: name,
        email: email,
        password: password,
        phone: phone,
        dateOfBirth: birth,
        address: address,
      })
      .then((res) => {
        console.log(res.data);
      });
  }

  return (
    <div className="wrapper-dashboard">
      <h1>LMS - Mentor - Account Update</h1>
      <div className="mentor-wrapper">
        <div className="title">Update Mentor Account</div>
        <div className="form">
          <div className="inputfield">
            <label>Name</label>
            <input
              onChange={(e) => setName(e.target.value)}
              type="text"
              className="input"
            ></input>
          </div>
          <div className="inputfield">
            <label>Email Address</label>
            <input
              onChange={(e) => setEmail(e.target.value)}
              type="text"
              className="input"
            ></input>
          </div>
          <div className="inputfield">
            <label>Password</label>
            <input
              onChange={(e) => setPassword(e.target.value)}
              type="password"
              className="input"
            ></input>
          </div>
          <div className="inputfield">
            <label>Confirm Password</label>
            <input
              onChange={(e) => setConfirmPassword(e.target.value)}
              type="password"
              className="input"
            ></input>
          </div>
          <div className="inputfield">
            <label>Phone Number</label>
            <input
              onChange={(e) => setPhone(e.target.value)}
              type="text"
              className="input"
            ></input>
          </div>
          <div className="inputfield">
            <label>Date of Birth</label>
            <input
              onChange={(e) => setBirth(e.target.value)}
              type="text"
              className="input"
            ></input>
          </div>
          <div className="inputfield">
            <label>Address</label>
            <textarea
              onChange={(e) => setAddress(e.target.value)}
              className="textarea"
            ></textarea>
          </div>
          <div className="inputfield">
            <label>Gender</label>
            <div className="custom_select">
              <select onChange={(e) => setGender(e.target.value)}>
                <option value="">Select</option>
                <option value="male">Male</option>
                <option value="female">Female</option>
              </select>
            </div>
          </div>
          <div className="inputfield">
            <input
              onClick={onSubmit}
              type="submit"
              value="Update"
              className="btn"
            ></input>
          </div>
        </div>
      </div>
    </div>
  );
}

export default AccountUpdate;
