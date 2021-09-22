import React from "react";
import "../../../Home/Home.css";
import { Link } from "react-router-dom";
import "../../../../../App.css";

function ItemManageCourse({ item }) {
  /* const [loading, setloading] = useState(false);
  const [listUser, setListUser] = useState([]);
  const [currentIdUser, setCurrentIdUser] = useState("");
  const IdUser = getCurrentIdUser();
  useEffect(() => {
    if (IdUser) {
      setCurrentIdUser(IdUser);
    }
  }, [IdUser]); */

  return (
    <div className="item-course">
      <div className="item-course__img">
        <img alt="" src={item.image} />
      </div>
      <div className="item-course__info">
        <h1 className="info-header">{item.courseName}</h1>
        <p className="info-desc">{item.description}</p>
        <h1 className="info-price">Free</h1>
        <div className="item-manage-course-btn">
          {/* <Link
            to={`/systemadmin/coursemanage/${item.idCourse}`}
            className="btn-manage-detail-course"
          >
            Detail Course
          </Link> */}
          <button className="btn-approve-course">Approve Course</button>
          <button className="btn-deny-course">Deny Course</button>
        </div>
      </div>
    </div>
  );
}

export default ItemManageCourse;
