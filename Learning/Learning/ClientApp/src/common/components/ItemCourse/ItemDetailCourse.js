import React, { useState, useEffect } from "react";
import "../Home/Home.css";
import Header from "../Home/Header";
import "../DetailCourse/DetailCourse.css";
import axios from "axios";
import { useParams } from "react-router";
import { getCurrentIdUser } from "../../Service/AuthService";
import AuthHeader from "../../Service/AuthHeader";
import Modal from "react-modal";
import ModalCourse from "../DetailCourse/ModalCourse";

function ItemDetailCourse(item) {
  /* const params = useParams();
    const [idCourse, setIdCourse] = useState("");
    const url = `https://todoapi20210730142909.azurewebsites.net/api/Student/course/${params.id}/course-detail-list`;
    useEffect(() => {
      axios
        .get(url, { headers: AuthHeader() })
        .then((res) => {
          setIdCourse(res.data.data);
          console.log(res.data.data);
        })
        .catch((err) => console.log(err));
    }, [url]); */
  const [modalIsOpen, setModalIsOpen] = useState(false);
  const [currentIdUser, setCurrentIdUser] = useState("");
  const [data, setData] = useState("");
  const IdUser = getCurrentIdUser;
  useEffect(() => {
    if (IdUser) {
      setCurrentIdUser(IdUser);
    }
  }, [IdUser]);

  return (
    <div className="wrapp-full">
      <div className="wrapper-container">
        <Header />
        <div className="form-detail-course">
          <h1>CHI TIẾT KHÓA HỌC</h1>
          <div className="course-detail-item">
            <h2 className="title-course-item">{item.courseName}</h2>
            <div className="item-course-info">
              <img className="item-course-img" src={item.image} alt="" />
              <div className="course-info-right">
                <div className="fill-course-item">
                  <h2>Name Creator:</h2>
                  <span>?? Instructor</span>
                </div>
                <div className="fill-course-item">
                  <h2>Description:</h2>
                  <span className="detail-course-desc">{item.description}</span>
                </div>
                <div className="fill-course-item">
                  <h2>Duration:</h2>
                  <span>{item.duration} hours</span>
                </div>
                <button
                  onClick={() => setModalIsOpen(true)}
                  className="button-participate"
                >
                  Enroll
                </button>
                <Modal
                  isOpen={modalIsOpen}
                  onRequestClose={() => setModalIsOpen(false)}
                  style={{
                    overlay: {
                      backgroundColor: "rgba(0,0,0,0.4)",
                    },
                    content: {
                      width: "40%",
                      margin: "auto",
                      height: "70%",
                    },
                  }}
                >
                  <ModalCourse data={data} setModalIsOpen={setModalIsOpen} />
                </Modal>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ItemDetailCourse;
