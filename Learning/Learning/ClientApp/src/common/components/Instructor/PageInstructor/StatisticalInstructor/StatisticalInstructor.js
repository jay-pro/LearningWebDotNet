import React, { useState, useEffect } from "react";
import { Line, Column } from "@ant-design/charts";
import axios from "axios";
import AuthHeader from "../../../../Service/AuthHeader";
import { getCurrentIdUser } from "../../../../Service/AuthService";

function StatisticalInstructor() {
	const IdUser = getCurrentIdUser();
	const [statis, setStatis] = useState([]);
	const url =
		"https://todoapi20210730142909.azurewebsites.net/api/Instructor/statistic/view-course-month";
	const reloadMonth = () => {
		axios
			.get(url, { params: { IDUser: IdUser }, headers: AuthHeader() })
			.then((res) => {
				setStatis(res.data.data);
				console.log(res.data.data);
			})
			.catch((err) => console.log(err));
	};
	useEffect(() => {
		reloadMonth();
	}, []);

	return (
		<div className="wrapper-dashboard  ">
			<div className="wrapper-flex">
				<div className="chart-week-month">
					<button className="chart-week">week</button>
					<button className="chart-month">month</button>
				</div>

				<div className="chart-statis">
					<div className="Chart-Score chart-right">
						<Line
							className="custom-chart"
							data={statis}
							height={300}
							xField="month"
							yField="quantity"
							point={{ size: 5, shape: "diamon" }}
							color="blue"
						/>
					</div>
				</div>
			</div>
		</div>
	);
}

export default StatisticalInstructor;
