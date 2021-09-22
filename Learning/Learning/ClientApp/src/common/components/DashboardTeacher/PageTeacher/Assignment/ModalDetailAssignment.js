import React, { useState, useEffect } from "react";
import axios from "axios";
import "./Assignment.css";
import { toast } from "react-toastify";
import { getCurrentIdUser } from "../../../../Service/AuthService";
import AuthHeader from "../../../../Service/AuthHeader";
import { useParams } from "react-router-dom";
import TableAssignment from "./TableAssignment";
import Teacherr from "../../../../../images/study2.png";

function ModalDetailAssignment({ mdetail }) {
  console.log(mdetail);
  const [currentIdUser, setCurrentIdUser] = useState(null);
  const IdUser2 = getCurrentIdUser();
  useEffect(() => {
    if (IdUser2) {
      setCurrentIdUser(IdUser2);
    }
  }, [IdUser2]);
  const { idClass } = useParams();
  const { idAssignemnt } = useParams();
  const url = `https://todoapi20210730142909.azurewebsites.net/api/Teachers/get-assignment/${mdetail}`;
  const [name, setName] = useState();
  const [description, setDescription] = useState();
  const [classs, setClasss] = useState();
  const [assignmentSubmission, setAssignmentSubmission] = useState([]);

  const [detaimol, setDetailmol] = useState([]);
  useEffect(() => {
    axios
      .get(url, {
        params: { IDClass: idClass, IDAssignment: idAssignemnt },
        headers: AuthHeader(),
      })
      .then((res) => {
        console.log(res.data);
        setDetailmol(res.data.data);
      })
      .catch((err) => console.log(err));
  }, [url]);

  return (
    <div className="wrapper-dashboard ">
      <div className="wrapper-flex ">
        <h1 className="title-name-create">Detai assignment </h1>
        <div className="manager-assignment">
          <div className="manager-assignment-left">
            <div className="create-assignment-item">
              <div className="imgassignment">
                <img src={Teacherr} />
              </div>
              <span>Assignment: {detaimol.assignmentName}</span>
              <span>Description: {detaimol.description}</span>
              <span>Sumited: {detaimol.assignmentSubmission}</span>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ModalDetailAssignment;
