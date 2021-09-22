import React, { useState, useEffect } from "react";
import TableAssigment from "./TableAssignment";
import axios from "axios";
import { toast } from "react-toastify";
import AuthHeader from "../../../../Service/AuthHeader";
import { getCurrentIdUser } from "../../../../Service/AuthService";
import { useParams } from "react-router-dom";
import "./Assignment.css";
import imgTotal from "../../../../../images/tong.png";
import imgAbsent from "../../../../../images/vang.png";
import TableAssignment from "./TableAssignment";
import Modal from "react-modal";
import ModalAddAssignment from "./ModalAddAssignment";
import ModalEditAssignment from "./ModalEditAssignment";
import ModalDetailAssignment from "./ModalDetailAssignment";

function Assignment() {
  const [modalIsOpen_add, setModalIsOpen_add] = useState(false);
  const [modalIsOpen_detail, setModalIsOpen_detail] = useState(false);
  const [modalIsOpen_edit, setModalIsOpen_edit] = useState(false);
  const [data, setData] = useState(""); //

  const [loading, setloading] = useState(false);
  const [listAssignment, setListAssignment] = useState([]);
  const [currentIdUser, setCurrentIdUser] = useState("");
  const IdUser = getCurrentIdUser();
  useEffect(() => {
    if (IdUser) {
      setCurrentIdUser(IdUser);
    }
  }, [IdUser]);
  const { idClass } = useParams();

  const url = `https://todoapi20210730142909.azurewebsites.net/api/Teachers/assignment/`;
  useEffect(() => {
    const getPostsData = () => {
      setloading(true);
      axios
        .get(url, {
          headers: AuthHeader(),
          params: { IDClass: idClass },
        })
        .then((res) => {
          setListAssignment(res.data.data);
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
      .put(
        `https://todoapi20210730142909.azurewebsites.net/api/Teachers/assignment/delete?assignmentId=${key}`,
        {
          headers: AuthHeader(),
          params: { IDAssignment: key },
        }
      )
      .then((res) => {
        console.log(res.data);
        // setListAssignment(res.data.data);
        toast.success("Xóa bài tập thành công");
        window.location.reload();
      })
      .catch((err) => console.log(err));
  };

  const [mdetail, setMdetail] = useState("");
  const onDetail = (key, e) => {
    setMdetail(key);
    e.preventDefault();
    console.log(key);
    setModalIsOpen_detail(true);
  };

  const onEdit = (key, e) => {
    setMdetail(key);
    e.preventDefault();
    console.log(key);
    setModalIsOpen_edit(true);
  };

  const columns = [
    {
      title: "Assigntment name",
      dataIndex: "assignmentName",
      responsive: ["md"],
    },
    {
      title: "Description",
      dataIndex: "description",
      responsive: ["lg"],
    },
    {
      title: "Action",
      dataIndex: "",
      key: "x",
      render: (listAssignment) => (
        <div>
          <button
            className="check-delete"
            onClick={(e) => onDetail(listAssignment.idAssignemnt, e)}
          >
            Detail
          </button>
          <button
            className="check-delete"
            onClick={(e) => onEdit(listAssignment.idAssignemnt, e)}
          >
            Edit
          </button>
          <button
            className="check-delete"
            onClick={(e) => onDelete(listAssignment.idAssignemnt, e)}
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
            <span>{listAssignment.length}</span>
          </div>
        </div>
        <div className="form-count count-user-current">
          <img src={imgAbsent} alt="" />
          <div className="count-user-info">
            <span>More assignment</span>
            <button
              onClick={() => setModalIsOpen_add(true)}
              className="check-delete"
            >
              Create
            </button>
          </div>
        </div>
      </div>
      <div className="manager-attendance">
        <TableAssignment
          loading={loading}
          columns={columns}
          data={listAssignment}
        />
      </div>
      <Modal
        isOpen={modalIsOpen_add}
        onRequestClose={() => setModalIsOpen_add(false)}
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
        <ModalAddAssignment
          data={data}
          setModalIsOpen_add={setModalIsOpen_add}
        />
      </Modal>
      <Modal
        isOpen={modalIsOpen_detail}
        onRequestClose={() => setModalIsOpen_detail(false)}
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
        <ModalDetailAssignment
          mdetail={mdetail}
          data={data}
          setModalIsOpen_detail={setModalIsOpen_detail}
        />
      </Modal>
      <Modal
        isOpen={modalIsOpen_edit}
        onRequestClose={() => setModalIsOpen_edit(false)}
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
        <ModalEditAssignment
          mdetail={mdetail}
          data={data}
          setModalIsOpen_dedit={setModalIsOpen_edit}
        />
      </Modal>
    </div>
  );
}

export default Assignment;
