import React from "react";
import { Link } from "react-router-dom";

function ItemClassTeacher({ item }) {
  return (
    <div>
      <div className="item-class">
        <div className="item-class__info">
          <h1 className="info-header">{item.className}</h1>
          <p className="info-starttime">Bắt đầu: {item.startTime} </p>
          <p className="info-finishtime">Kết thúc: {item.finishTime}</p>
          <div className="item-class-btn">
            <Link
              className="btn-item-teacher"
              to={`/teacher/${item.idCourse}/${item.idClass}`}
              // to={`/teacher/${item.idClass}`}
            >
              Direct to Class for managing
            </Link>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ItemClassTeacher;
