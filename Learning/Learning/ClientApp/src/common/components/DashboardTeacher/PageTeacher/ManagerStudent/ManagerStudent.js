import React, { useState, useEffect } from "react";
import TableStudent from "./TableStudent";
import axios from "axios";
import { toast } from "react-toastify";
import AuthHeader from "../../../../Service/AuthHeader";
import { getCurrentIdUser } from "../../../../Service/AuthService";
import { useParams } from "react-router-dom";
import Modal from "react-modal";
import ModalAddStudent from "./ModalAddStudent";
import "./ManagerStudent.css";
import imgTotal from "../../../../../images/tong.png";
import imgCurrent from "../../../../../images/current.png";
import imgAbsent from "../../../../../images/vang.png";
import Attendance from "./Attendance";

function ManagerStudent() {
  const [modalIsOpen, setModalIsOpen] = useState(false);
  const [data, setData] = useState(""); //

  const [loading, setloading] = useState(false);
  const [listStudent, setListStudent] = useState([]);
  const [currentIdUser, setCurrentIdUser] = useState("");
  const IdUser = getCurrentIdUser();
  useEffect(() => {
    if (IdUser) {
      setCurrentIdUser(IdUser);
    }
  }, [IdUser]);
  const { idClass } = useParams();
  /* console.log(params.id); */
  console.log(idClass);
  const url = `https://todoapi20210730142909.azurewebsites.net/api/Teachers/get-students/${idClass}`;
  useEffect(() => {
    const getPostsData = () => {
      setloading(true);
      axios
        .get(url, {
          headers: AuthHeader(),
          /* params: {  id }, */
        })
        .then((res) => {
          setListStudent(res.data.data);
          console.log(res.data.data);
          setloading(false);
          console.log(AuthHeader());
        })
        .catch((error) => {
          console.log(error.response);
        });
    };
    getPostsData();
  }, [url, IdUser]);

  const onDelete = (key, e) => {
    e.preventDefault();
    console.log(key);
    axios
      .delete(
        `https://todoapi20210730142909.azurewebsites.net/api/Teachers/${idClass}/remove-student-id/${key}`,
        { headers: AuthHeader(), params: { IDStudent: key } }
      )
      .then((res) => {
        console.log(res.data);
        // setListStudent(res.data.data);
        toast.success("Xóa học viên khỏi lớp thành công");
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
      dataIndex: "",
      key: "x",
      render: (listStudent) => (
        <div>
          <button
            className="check-delete"
            onClick={(e) => onDelete(listStudent.idStudent, e)}
          >
            Delete
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
            <span className="data-user">Total:</span>
            <span>{listStudent.length}</span>
          </div>
        </div>
        <div className="form-count count-user-current">
          <img src={imgCurrent} alt="" />
          <div className="count-user-info">
            <span>Add more students</span>
            {/* <span>hi</span> */}
            <button
              onClick={() => setModalIsOpen(true)}
              className=/* "btn-add-student" */ "check-delete"
            >
              ADD
            </button>
          </div>
        </div>
      </div>
      <div className="manager-attendance">
        <TableStudent loading={loading} columns={columns} data={listStudent} />
      </div>
      <Modal
        isOpen={modalIsOpen}
        onRequestClose={() => setModalIsOpen(false)}
        style={{
          overlay: {
            backgroundColor: "rgba(0,0,0,0.4)",
          },
          content: {
            with: "100%",
            margin: "auto",
            marginLeft: "30px",
            height: "80%",
          },
        }}
      >
        <ModalAddStudent data={data} setModalIsOpen={setModalIsOpen} />
      </Modal>
    </div>
  );
}

export default ManagerStudent;
