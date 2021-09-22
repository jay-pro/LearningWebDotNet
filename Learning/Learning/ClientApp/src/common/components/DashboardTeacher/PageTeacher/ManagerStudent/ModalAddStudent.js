import React, { useEffect, useState } from "react";
import TableUserStudent from "./TableUserStudent";
import axios from "axios";
import { toast } from "react-toastify";
import AuthHeader from "../../../../Service/AuthHeader";
import { getCurrentIdUser } from "../../../../Service/AuthService";
import { useParams } from "react-router-dom";
import "./ManagerStudent.css";
import imgTotal from "../../../../../images/tong.png";

function ModalAddStudent({ setModalIsOpen }) {
  const IdUser = getCurrentIdUser();
  const [loading, setloading] = useState(false);
  const [listUserStudent, setListUserStudent] = useState([]);
  const [currentIdUser, setCurrentIdUser] = useState("");
  const IdUser2 = getCurrentIdUser();
  useEffect(() => {
    if (IdUser2) {
      setCurrentIdUser(IdUser2);
    }
  }, [IdUser2]);
  const { idClass } = useParams();
  console.log(idClass);
  const url = `https://todoapi20210730142909.azurewebsites.net/api/Teachers/get-students-to-add`;
  useEffect(() => {
    const getPostsData = () => {
      setloading(true);
      axios
        .get(url, {
          headers: AuthHeader(),
          params: { IDClass: idClass },
        })
        .then((res) => {
          setListUserStudent(res.data.data);
          console.log(res.data.data);
          setloading(false);
          console.log(AuthHeader());
        })
        .catch((error) => {
          console.log(error.response);
        });
    };
    getPostsData();
  }, [url, IdUser2]);

  const onAdd = (key, e) => {
    e.preventDefault();
    console.log(key);
    axios
      .post(
        `https://todoapi20210730142909.azurewebsites.net/api/Teachers/${idClass}/add-student-id/${key}`,
        { headers: AuthHeader(), params: { IDStudent: key } }
      )
      .then((res) => {
        console.log(res.data);
        // setListStudent(res.data.data);
        toast.success("Gán học viên vào lớp thành công");
        window.location.reload();
      })
      .catch((err) => console.log(err));
  };

  const columns = [
    {
      title: "Username",
      dataIndex: "userName",
      responsive: ["md"],
    },
    {
      title: "Fullname",
      dataIndex: "fullName",
      responsive: ["md"],
    },
    {
      title: "Email",
      dataIndex: "email",
      responsive: ["lg"],
    },
    {
      title: "Phone",
      dataIndex: "phone",
      responsive: ["md"],
    },
    {
      title: "Avatar",
      dataIndex: "avatar",
      responsive: ["lg"],
    },
    {
      title: "Action",
      dataIndex: "idStudent",
      key: "x",
      render: (listUserStudent) => (
        <div>
          <button
            className="check-delete"
            onClick={(e) => onAdd(listUserStudent, e)} //listUserStudent.idStudent
          >
            Add
          </button>
        </div>
      ),
    },
  ];

  return (
    <div className="wrapper-dashboard">
      <div className="form-manager-user">
        <div className="form-count count-user-course">
          <img src={imgTotal} alt="" />
          <div className="count-user-info">
            <span className="data-user">Remain:</span>
            <span>{listUserStudent.length}</span>
          </div>
        </div>
      </div>
      <div className="manager-attendance">
        <TableUserStudent
          loading={loading}
          columns={columns}
          data={listUserStudent}
        />
      </div>
      <div className="btn-cancel-modal">
        <button onClick={() => setModalIsOpen(false)} className="btn-cancel">
          Cancel
        </button>
      </div>
    </div>
  );
}

export default ModalAddStudent;
