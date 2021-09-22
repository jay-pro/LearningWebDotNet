import React, { useEffect, useState } from "react";
import "./DetailCourse.css";
import "../Search/Search.css";
import SearchBar from "../Search/SearchBar";
import ItemDetailCourse from "../ItemCourse/ItemDetailCourse";
import axios from "axios";
import { getCurrentIdUser } from "../../Service/AuthService";
import AuthHeader from "../../Service/AuthHeader";
import { useParams } from "react-router";

function ModalDetailCourse({ setModalIsOpen, data }) {
  const [currentIdUser, setCurrentIdUser] = useState("");
  const [detailcourse, setDetailCourse] = useState("");
  const [courseName, setCourseName] = useState("");
  const [image, setImage] = useState("");
  const [description, setDescription] = useState("");
  const [duration, setDuration] = useState("");

  /*Get login user */
  const IdUser = getCurrentIdUser;
  useEffect(() => {
    if (IdUser) {
      setCurrentIdUser(IdUser);
    }
  }, [IdUser]);

  /*Show course detail list */
  /* api get course detail list*/
  const params = useParams();
  const [idCourse, setIdCourse] = useState("");
  const url3 = `https://todoapi20210730142909.azurewebsites.net/api/Instructor/course/${params.id}`;
  /* const url3 = `https://todoapi20210730142909.azurewebsites.net/api/Student/course/${params.id}/course-detail-list`; */
  const [loading, setLoading] = useState(false);
  useEffect(() => {
    const getPostsData = () => {
      axios
        .get(url3, { headers: AuthHeader() })
        .then((res) => {
          setDetailCourse(res.data.data);
          setLoading(true);
          console.log(res.data.data);
        })
        .catch((error) => alert(error));
    };
    getPostsData();
  }, [url3]);
  console.log(loading);

  return (
    <div className="wrapp-full">
      <div className="wrapp-flex">
        <h1 className="title-enroll">Course Detail List</h1>
        <div className="list-classes">
          <div className="list-outstand">
            {loading ? (
              detailcourse.map((item, id) => (
                <ItemDetailCourse key={id} item={item} />
              ))
            ) : (
              <SearchBar />
            )}
          </div>
        </div>
      </div>
    </div>
  );
}

export default ModalDetailCourse;
