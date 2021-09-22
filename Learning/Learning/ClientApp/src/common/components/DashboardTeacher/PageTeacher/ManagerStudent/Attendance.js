import React from "react";
import "./ManagerStudent.css";
import { Table } from "antd";

function Attendance({ page, setPage, pageSize, setPageSize, columns, data }) {
	return (
		<div>
			<h1 className="table-User-title">Danh sách học viên</h1>
			<button className="add-User"> ADD</button>
			<div className="table-User">
				<Table
					columns={columns}
					dataSource={data}
					pagination={{
						current: page,
						pageSize: pageSize,
						onChange: (page, pageSize) => {
							setPage(page);
							setPageSize(pageSize);
						},
					}}
				/>
			</div>
		</div>
	);
}

export default Attendance;
