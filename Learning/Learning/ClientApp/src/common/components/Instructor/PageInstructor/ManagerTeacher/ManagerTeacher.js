import React, { useState, useEffect } from "react";
import TableTeacher from "./TableTeacher";
import axios from "axios";
import AuthHeader from "../../../../Service/AuthHeader";
import countUser from "../../../../../images/vang.png";
import Modal from "react-modal";
import MdDetailTeacher from "./MdDetailTeacher";
import MdAssign from "./MdAssign";

function ManagerTeacher() {
	const [modalDetailTeacher, setModalDetailTeacher] = useState(false);
	const [modalAssign, setModalAssign] = useState(false);
	const [loading, setloading] = useState(false);
	const [listTeacher, setListTeacher] = useState([]);
	const [selectedTeacher, setSelectedTeacher] = useState();

	const url =
		"https://todoapi20210730142909.azurewebsites.net/api/Instructor/get-teacher-list";
	useEffect(() => {
		const getPostsData = () => {
			setloading(true);
			axios
				.get(url, { headers: AuthHeader() })
				.then((res) => {
					setListTeacher(res.data.data);
					console.log(res.data.data);
					setloading(false);
				})

				.catch((error) => alert(error));
		};
		getPostsData();
	}, [url]);

	const onDetail = (id, e) => {
		e.preventDefault();
		setSelectedTeacher(id);
		setModalDetailTeacher(true);
	};
	const onAssign = (id, e) => {
		e.preventDefault();
		setSelectedTeacher(id);
		setModalAssign(true);
	};
	const columns = [
		{
			title: "Name",
			dataIndex: "userName",
			responsive: ["md"],
			key: "userName",
		},

		{
			title: "Action",
			dataIndex: "action",
			key: "idTeacher",
			render: (item, teacher) => (
				<div>
					<button
						className="check-delete"
						// onClick={(e) => onDelete(listTeacher.idTeacher, e)}
						onClick={(e) => onDetail(teacher.idTeacher, e)}
					>
						Detail
					</button>
					<button
						className="check-delete"
						// onClick={(e) => onDelete(listTeacher.idTeacher, e)}
						onClick={(e) => onAssign(teacher.idTeacher, e)}
					>
						Assign
					</button>

					<Modal
						isOpen={modalDetailTeacher}
						onRequestClose={() => setModalDetailTeacher(false)}
						style={{
							overlay: {
								backgroundColor: "rgba(0,0,0,0.4)",
							},
							content: {
								width: "30%",
								margin: "auto",
								height: "85%",
							},
						}}
					>
						<MdDetailTeacher
							dataIdTeacher={selectedTeacher}
							setModalIsOpen={setModalDetailTeacher}
						/>
					</Modal>

					<Modal
						isOpen={modalAssign}
						onRequestClose={() => setModalAssign(false)}
						style={{
							overlay: {
								backgroundColor: "rgba(0,0,0,0.4)",
							},
							content: {
								width: "30%",
								margin: "auto",
								height: "30%",
							},
						}}
					>
						<MdAssign
							dataIdTeacher={selectedTeacher}
							setModalSelect={setModalAssign}
						/>
					</Modal>
				</div>
			),
		},
	];

	return (
		<div className="wrapper-dashboard">
			<div className="form-manager-user">
				<div className="form-count count-user-course">
					<img src={countUser} alt="" />
					<div className="count-user-info">
						<span className="data-user">Tổng</span>
						<span>{listTeacher.length}</span>
					</div>
				</div>
			</div>
			<div className="manager-attendance">
				<TableTeacher loading={loading} columns={columns} data={listTeacher} />
			</div>
		</div>
	);
}

export default ManagerTeacher;
