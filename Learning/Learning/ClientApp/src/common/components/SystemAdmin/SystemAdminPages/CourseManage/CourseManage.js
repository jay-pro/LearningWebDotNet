import React, { useEffect, useState } from "react";
import "../../../DetailCourse/DetailCourse.css";
import "../../../Search/Search.css";
import "../../../../../App.css";
import "../../../Home/Home.css";
import axios from "axios";
import { getCurrentIdUser } from "../../../../Service/AuthService";
import AuthHeader from "../../../../Service/AuthHeader";
import { toast } from "react-toastify";
import TableCourse from "./TableCourse";

function CourseManage() {
	const [loading, setloading] = useState(false);
	const [listCourse, setListCourse] = useState([]);
	const IdUser = getCurrentIdUser();

	const url =
		"https://todoapi20210730142909.azurewebsites.net/api/SystemAdmin/course-management/get-course-list";
	useEffect(() => {
		const getPostsData = () => {
			setloading(true);
			axios
				.get(url, { headers: AuthHeader(), params: { IDUser: IdUser } })
				.then((res) => {
					setListCourse(res.data.data);
					console.log(res.data.data);
					setloading(false);
					console.log(AuthHeader());
				})
				.catch((error) => alert(error));
		};
		getPostsData();
	}, [url, IdUser]);

	const onApprove = (key, e) => {
		e.preventDefault();
		console.log(key);
		axios
			.put(
				`https://todoapi20210730142909.azurewebsites.net/api/SystemAdmin/course-management/approve-course/${key}`,
				{},
				{ headers: AuthHeader(), params: { IDCourse: key } }
			)
			.then((res) => {
				console.log(res.data);
				// setListUser(res.data.data);
				toast.success("Duyệt khóa học thành công");
				window.location.reload();
			})
			.catch((err) => console.log(err));
	};

	const onDeny = (key, e) => {
		e.preventDefault();
		console.log(key);
		axios
			.put(
				`https://todoapi20210730142909.azurewebsites.net/api/SystemAdmin/course-management/deny-course/${key}`,
				{},
				{ headers: AuthHeader(), params: { IDCourse: key } }
			)
			.then((res) => {
				console.log(res.data);
				// setListUser(res.data.data);
				toast.success("Từ chối duyệt khóa học thành công");
				window.location.reload();
			})
			.catch((err) => console.log(err));
	};

	const columns = [
		{
			title: "Course Name",
			dataIndex: "courseName",
			responsive: ["md"],
		},
		{
			title: "Description",
			dataIndex: "description",
			responsive: ["md"],
		},
		{
			title: "Image",
			dataIndex: "image",
			responsive: ["lg"],
		},
		{
			title: "Action",
			dataIndex: "",
			key: "x",
			render: (listCourse) => (
				<div>
					<button
						className="check-delete"
						onClick={(e) => onApprove(listCourse.idCourse, e)}
					>
						Approve
					</button>
					<button
						className="check-delete"
						onClick={(e) => onDeny(listCourse.idCourse, e)}
					>
						Deny
					</button>
				</div>
			),
		},
	];

	return (
		<div className="wrapper-dashboard">
			<div className="form-manager-user">
				<div className="form-count count-user-course">
					<img src="" alt="" />
					<div className="count-user-info">
						<span className="data-user">Total</span>
						<span>{listCourse.length}</span>
					</div>
				</div>
			</div>
			<div className="manager-attendance">
				<TableCourse loading={loading} columns={columns} data={listCourse} />
			</div>
		</div>
	);
}

export default CourseManage;
