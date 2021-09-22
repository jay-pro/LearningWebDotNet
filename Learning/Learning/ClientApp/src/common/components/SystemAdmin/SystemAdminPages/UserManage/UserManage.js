import React, { useState, useEffect } from "react";
import TableUser from "./TableUser";
import axios from "axios";
import { toast } from "react-toastify";
import AuthHeader from "../../../../Service/AuthHeader";
import { getCurrentIdUser } from "../../../../Service/AuthService";
import imgTotal from "../../../../../images/tong.png";

function UserManage() {
	const [loading, setloading] = useState(false);
	const [listUser, setListUser] = useState([]);
	const IdUser = getCurrentIdUser();

	const url =
		"https://todoapi20210730142909.azurewebsites.net/api/SystemAdmin/user-management/get-user-list";
	useEffect(() => {
		const getPostsData = () => {
			setloading(true);
			axios
				.get(url, { headers: AuthHeader(), params: { IDUser: IdUser } })
				.then((res) => {
					setListUser(res.data.data);
					console.log(res.data.data);
					setloading(false);
					console.log(AuthHeader());
				})
				.catch((error) => alert(error));
		};
		getPostsData();
	}, [url, IdUser]);

	/* const onDelete = (key, e) => {
    e.preventDefault();
    console.log(key);
    axios
      .delete(
        `https://todoapi20210730142909.azurewebsites.net/api/SystemAdmin/user-management/hard-delete-user/${key}`,
        { headers: AuthHeader(), params: { IDUser: key } }
      )
      .then((res) => {
        console.log(res.data);
        // setListUser(res.data.data);
        toast.success("suscess");
      })
      .catch((err) => console.log(err));
  }; */

	const onApprove = (key, e) => {
		e.preventDefault();
		console.log(key);
		axios
			.put(
				`https://todoapi20210730142909.azurewebsites.net/api/SystemAdmin/user-management/approve-user/${key}`,
				{},
				{ headers: AuthHeader(), params: { IDUser: key } }
			)
			.then((res) => {
				console.log(res.data);
				// setListUser(res.data.data);
				toast.success("Duyệt user thành công");
				window.location.reload();
			})
			.catch((err) => console.log(err));
	};

	const onDeny = (key, e) => {
		e.preventDefault();
		console.log(key);
		axios
			.put(
				`https://todoapi20210730142909.azurewebsites.net/api/SystemAdmin/user-management/deny-user/${key}`,
				{},
				{ headers: AuthHeader(), params: { IDUser: key } }
			)
			.then((res) => {
				console.log(res.data);
				// setListUser(res.data.data);
				toast.success("Từ chối duyệt user thành công");
				window.location.reload();
			})
			.catch((err) => console.log(err));
	};

	const onLock = (key, e) => {
		e.preventDefault();
		console.log(key);
		var date = new Date();
		date.setDate(date.getDate() + 30);
		axios
			.put(
				`https://todoapi20210730142909.azurewebsites.net/api/SystemAdmin/user-management/lock-user/${key}`,
				{},
				{ headers: AuthHeader(), params: { IDUser: key, dateTime: date } }
			)
			.then((res) => {
				console.log(res.data);
				// setListUser(res.data.data);
				toast.success("Khóa user thành công");
				window.location.reload();
			})
			.catch((err) => console.log(err));
	};

	const onUnlock = (key, e) => {
		e.preventDefault();
		console.log(key);
		axios
			.put(
				`https://todoapi20210730142909.azurewebsites.net/api/SystemAdmin/user-management/unlock-user/${key}`,
				{},
				{ headers: AuthHeader(), params: { IDUser: key } }
			)
			.then((res) => {
				console.log(res.data);
				// setListUser(res.data.data);
				toast.success("Mở khóa user thành công");
				window.location.reload();
			})
			.catch((err) => console.log(err));
	};

	const columns = [
		{
			title: "Name",
			dataIndex: "username",
			responsive: ["md"],
		},
		{
			title: "Email",
			dataIndex: "email",
			responsive: ["lg"],
		},
		{
			title: "Phone",
			dataIndex: "phone",
			responsive: ["md"],
		},
		{
			title: "Avatar",
			dataIndex: "avatar",
			responsive: ["lg"],
		},
		{
			title: "Action",
			dataIndex: "",
			key: "x",
			render: (listUser) => (
				<div>
					<button
						className="check-delete"
						onClick={(e) => onApprove(listUser.idUser, e)}
					>
						Approve
					</button>
					<button
						className="check-delete"
						onClick={(e) => onDeny(listUser.idUser, e)}
					>
						Deny
					</button>
					{/* <button
            className="check-delete"
            onClick={(e) => onDelete(listUser.idUser, e)}
          >
            Delete
          </button> */}
					<button
						className="check-delete"
						onClick={(e) => onLock(listUser.idUser, e)}
					>
						Lock
					</button>
					<button
						className="check-delete"
						onClick={(e) => onUnlock(listUser.idUser, e)}
					>
						Unlock
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
						<span>{listUser.length}</span>
					</div>
				</div>
			</div>
			<div className="manager-attendance">
				<TableUser loading={loading} columns={columns} data={listUser} />
			</div>
		</div>
	);
}

export default UserManage;
