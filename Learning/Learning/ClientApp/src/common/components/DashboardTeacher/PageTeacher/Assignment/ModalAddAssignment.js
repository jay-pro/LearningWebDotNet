import React, { useState, useEffect } from "react";
import axios from "axios";
import "./Assignment.css";
import { toast } from "react-toastify";
import { getCurrentIdUser } from "../../../../Service/AuthService";
import AuthHeader from "../../../../Service/AuthHeader";
import { Image } from "cloudinary-react";
import { useParams } from "react-router-dom";
import TableAssignment from "./TableAssignment";
import Teacherr from "../../../../../images/study.png";

function ModalAddAssignment() {
  const [currentIdUser, setCurrentIdUser] = useState(null);
  const IdUser2 = getCurrentIdUser();
  useEffect(() => {
    if (IdUser2) {
      setCurrentIdUser(IdUser2);
    }
  }, [IdUser2]);
  const { idClass } = useParams();
  console.log(idClass);
  const url =
    "https://todoapi20210730142909.azurewebsites.net/api/Teachers/create-assignment";
  const [name, setName] = useState();
  const [description, setDescription] = useState();

  function onSubmit() {
    axios
      .post(
        url,
        {
          assignmentName: name,
          description: description,
          idClass: idClass,
        },
        {
          params: { IDClass: idClass },
          headers: AuthHeader(),
        }
      )
      .then((res) => {
        console.log(res.data);
        toast.success("Tạo bài tập thành công");
        window.location.reload();
      })
      .catch((err) => console.log(err));
  }

  return (
    <div className="wrapper-dashboard ">
      <div className="wrapper-flex ">
        <h1 className="title-name-create">Create new assignments </h1>
        <div className="manager-assignment">
          <div className="manager-assignment-left">
            <div className="create-assignment-item">
              <div className="imgassignment">
                <img src={Teacherr} />
              </div>
              <input
                onChange={(e) => setName(e.target.value)}
                className="title-assignment"
                type="text"
                placeholder="Assignment name"
              />
              <input
                onChange={(e) => setDescription(e.target.value)}
                type="text"
                placeholder="description"
                className="assignment-create-description"
              />
            </div>
          </div>
        </div>
        <div className="btn-create-assignment">
          {IdUser2 == null ? (
            <button onClick={onSubmit} className="Create-assignment">
              Publish assignment
            </button>
          ) : (
            <button onClick={onSubmit} className="Create-assignment">
              Publish assignment
            </button>
          )}
        </div>
      </div>
    </div>
  );
}

export default ModalAddAssignment;
