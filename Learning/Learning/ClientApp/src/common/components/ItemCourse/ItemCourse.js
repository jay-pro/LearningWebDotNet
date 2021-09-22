import React from "react";
import "../Home/Home.css";
import { Link } from "react-router-dom";
function ItemCourse({ item }) {
  return (
    <div className="item-course">
      <div className="item-course__img">
        <img alt="" src={item.image} />
      </div>
      <div className="item-course__info">
        <h1 className="info-header">{item.courseName}</h1>
        <p className="info-desc">{item.description}</p>
        <h1 className="info-price">Free</h1>
        <div className="item-course-btn">
          <Link
            to={`/course/detail/${item.idCourse}`}
            className="btn-item-detail"
            /* ModalDetailCourse.js */
          >
            Detail
          </Link>
          <Link
            to={`course/study/${item.idCourse}`}
            className="btn-item-study"
            /* ModalCourse.js */
          >
            Enroll
          </Link>
        </div>
      </div>
    </div>
  );
}

export default ItemCourse;
