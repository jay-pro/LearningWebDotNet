import React, { useState } from "react";
import { Line } from "@ant-design/charts";
import "./statistic.css";
import axios from "axios";
import "../../../../../App.css";
import "../../../Home/Home.css";
import AuthHeader from "../../../../Service/AuthHeader";

function Statistic() {
	// const [statis1, setStatis1] = useState([]);
	/* const url1 =
		"https://todoapi20210730142909.azurewebsites.net/api/SystemAdmin/statistic/class-student-quantity-course";

	const reload = () => {
		axios
		.get(url1, { headers: AuthHeader() })
		.then((res) => {
			setStatis(res.data.data);
		})
		.catch((err) => console.log(err));
	}; */

	const [statis2, setStatis2] = useState([]);
	const url2 =
		"https://todoapi20210730142909.azurewebsites.net/api/SystemAdmin/statistic/view-course-month";
	const reload2 = () => {
		axios
			.get(url2, { headers: AuthHeader() })
			.then((res) => {
				setStatis2(res.data.data);
			})
			.catch((err) => console.log(err));
	};

	const [statis3, setStatis3] = useState([]);
	const url3 =
		"https://todoapi20210730142909.azurewebsites.net/api/SystemAdmin/statistic/quantiy-course-according-to-field";
	const reload3 = () => {
		axios
			.get(url3, { headers: AuthHeader() })
			.then((res) => {
				setStatis3(res.data.data);
			})
			.catch((err) => console.log(err));
	};

	/*const url4 =
		"https://todoapi20210730142909.azurewebsites.net/api/SystemAdmin/statistic/view-course-month";

		const reload4 = () => {
		axios
			.get(url4, { headers: AuthHeader() })
			.then((res) => {
			setStatis4(res.data.data);
			})
			.catch((err) => console.log(err));
	};*/

	const [statis5, setStatis5] = useState([]);
	const url5 =
		"https://todoapi20210730142909.azurewebsites.net/api/SystemAdmin/statistic/course-quantity-according-to-rating";
	const reload5 = () => {
		axios
			.get(url5, { headers: AuthHeader() })
			.then((res) => {
				setStatis5(res.data.data);
			})
			.catch((err) => console.log(err));
	};

	return (
		<div className="wrapper-dashboard  ">
			<div className="wrapper-flex">
				<div className="chart">
					<h2>Xem thống kê</h2>
					<div className="list-btn-statistic">
						<button id="sa-bt2" onClick={reload2} className="btn-statistic">
							Thống kê khóa học theo tháng
						</button>
						<button id="sa-bt3" onClick={reload3} className="btn-statistic">
							Thống kê tỷ lệ khóa học theo chuyên ngành
						</button>

						<button onClick={reload5} className="btn-statistic">
							Thống kê số lượng khóa học theo rating
						</button>
					</div>
				</div>

				<div className="Chart-Score">
					<Line
						className="custom-chart"
						data={statis2}
						height={300}
						xField="month"
						yField="quantity"
						point={{ size: 5, shape: "diamon" }}
						color="blue"
					/>
					<Line
						className="custom-chart"
						data={statis3}
						height={300}
						xField="name"
						yField="ratio"
						point={{ size: 5, shape: "diamon" }}
						color="red"
					/>
					<Line
						className="custom-chart"
						data={statis5}
						height={300}
						xField="rating"
						yField="quantity"
						point={{ size: 5, shape: "diamon" }}
						color="green"
					/>
				</div>
			</div>
		</div>
	);
}

export default Statistic;
