/* Đăng ký class trong này */
import React from "react";
import "../Home/Home.css";
import "../../../App.css";
import { Link } from "react-router-dom";
import "../DetailCourse/DetailCourse.css";
import axios from "axios";
import { getCurrentIdUser } from "../../Service/AuthService";
import AuthHeader from "../../Service/AuthHeader";
import { toast } from "react-toastify";

function ItemClass({ item }) {
	const IdUser = getCurrentIdUser();

	/*register a class */
	const url3 =
		"https://todoapi20210730142909.azurewebsites.net/api/Student/register-class";

	const onSubmit = (idClass) => {
		console.log(idClass);
		axios
			.post(
				url3,
				{},
				{ headers: AuthHeader(), params: { IDUser: IdUser, IDClass: idClass } }
			)
			.then((res) => {
				console.log(res.data.data);
				toast.success("Success");
			})
			.catch((error) => {
				console.log(error);
				toast.error("Error");
			});
	};
	return (
		<div className="item-class">
			<div className="item-class__info">
				<h1 className="info-header">{item.className}</h1>
				<p className="info-starttime">Bắt đầu: {item.startTime}</p>
				<p className="info-finishtime">Kết thúc: {item.finishTime}</p>
				<div className="item-class-btn">
					<button
						className="btn-item-detail"
						onClick={() => onSubmit(item.idClass)}
					>
						<Link to="/registerclass" />
						Register
					</button>
				</div>
			</div>
		</div>
	);
}

export default ItemClass;
