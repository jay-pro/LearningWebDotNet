import React, { useState } from "react";
import { Table } from "antd";
import "./Point.css";
function Point() {
	const [page, setPage] = useState(1);
	const [pageSize, setPageSize] = useState(3);
	const columns = [
		{
			title: "Lesson",
			dataIndex: "lesson",
			key: "lesson",
			responsive: ["md"],
		},
		{
			title: "Time",
			dataIndex: "time",
			key: "time",
			responsive: ["md"],
		},
		{
			title: "Score",
			dataIndex: "score",
			key: "score",
			responsive: ["lg"],
		},
		{
			title: "Teacher",
			dataIndex: "teacher",
			key: "teacher",
			responsive: ["lg"],
		},
	];

	const data = [
		{
			key: "1",
			teacher: "John Brown",
			time: "21/9/1021",
			score: 8,
			lesson: "lesson 1",
		},
		{
			key: "2",
			teacher: "John Brown",
			time: "21/9/1021",
			score: 9,
			lesson: "lesson 2",
		},
		{
			key: "3",
			teacher: "John Brown",
			time: "21/9/1021",
			score: 8,
			lesson: "lesson 3",
		},
		{
			key: "4",
			teacher: "John Brown",
			time: "21/9/1021",
			score: 8,
			lesson: "lesson 4",
		},
		{
			key: "5",
			teacher: "John Brown",
			time: "21/9/1021",
			score: 8,
			lesson: "lesson 5",
		},
		{
			key: "6",
			teacher: "John Brown",
			time: "21/9/1021",
			score: 8,
			lesson: "lesson 6",
		},
		{
			key: "7",
			teacher: "John Brown",
			time: "21/9/1021",
			score: 8,
			lesson: "lesson 7",
		},
		{
			key: "8",
			teacher: "John Brown",
			time: "21/9/1021",
			score: 8,
			lesson: "lesson 8",
		},
	];
	return (
		<div className="wrapper-dashboard">
			<h1 className="table-title">Xem điểm khóa học</h1>
			<div className="table-point">
				<Table
					columns={columns}
					dataSource={data}
					pagination={{
						current: page,
						pageSize: pageSize,
						// total: 10,
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

export default Point;
