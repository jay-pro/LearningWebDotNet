import React from "react";
import "../Home/Home.css";
import "../../../App.css";
import { Link } from "react-router-dom";
import "../DetailCourse/DetailCourse.css";

function ItemRegisteredClass({ item }) {
	return (
		<div className="item-class">
			<div className="item-class__info">
				<h1 className="info-header">{item.className}</h1>
				<p className="info-starttime">Bắt đầu: {item.startTime}</p>
				<p className="info-finishtime">Kết thúc: {item.finishTime}</p>
				<div className="item-class-btn">
					<Link to={`/users/${item.idCourse}`} className="btn-item-detail">
						Học ngay
					</Link>
				</div>
			</div>
		</div>
	);
}

export default ItemRegisteredClass;
