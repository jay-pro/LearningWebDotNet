import axios from "axios";
import React, { useEffect, useState } from "react";
import { toast } from "react-toastify";
import AuthHeader from "../../../../Service/AuthHeader";

function MdAssignStudent({ idClass }) {
	const [listStudent, setListStudent] = useState([]);
	const url =
		"https://todoapi20210730142909.azurewebsites.net/api/Instructor/get-students-to-add";

	useEffect(() => {
		axios
			.get(url, { params: { IDClass: idClass }, headers: AuthHeader() })
			.then((res) => {
				console.log(res.data.data);
				setListStudent(res.data.data);
			})
			.catch((err) => console.log(err));
	}, [url, idClass]);

	const urlAssign =
		"https://todoapi20210730142909.azurewebsites.net/api/Instructor/assign-student-to-class";

	const onAssignClass = (id) => {
		console.log(id);
		console.log(idClass);
		axios
			.post(
				urlAssign,
				{},
				{
					params: { IDClass: idClass, IDStudent: id },
					headers: AuthHeader(),
				}
			)
			.then((res) => {
				console.log(res.data.data);
				toast.success("success");
				window.location.reload();
			})
			.catch((err) => {
				console.log(err);
				toast.error("faild");
			});
	};
	return (
		<div>
			{listStudent.map((item, id) => (
				<div key={id} className="list-student">
					<div className="list-student-item">
						<div className="item-info-student">
							<span className="title-student">Name</span>
						</div>
						<div className="item-info-student">
							<span>{item.userName}</span>
						</div>
					</div>
					<div className="list-student-email">
						<div className="item-info-student">
							<span className="title-student">Email</span>
						</div>
						<div className="item-info-student">
							<span>{item.email}</span>
						</div>
					</div>
					<button
						onClick={() => onAssignClass(item.idStudent)}
						className="btn-assign-student"
					>
						Assign
					</button>
				</div>
			))}
		</div>
	);
}

export default MdAssignStudent;
