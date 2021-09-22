import React from "react";
import { Line } from "@ant-design/charts";
import "./Statistical.css";
function Statistical() {
	const data = [
		{ Day: "Monday", value: 8 },
		{ Day: "Tuesday", value: 9 },
		{ Day: "Thurday", value: 7 },
		{ Day: "Wednesday", value: 6 },
		{ Day: "Friday", value: 9 },
		{ Day: "Satuday", value: 7 },
	];

	const config = {
		data,
		xField: "Day",
		yField: "value",
		point: {
			size: 3,
			shape: "diamond",
			stroke: "#2593fc",
		},
	};
	return (
		<div className="wrapper-dashboard  ">
			<div className="wrapper-flex">
				<div className="chart-week-month">
					<button className="chart-week">week</button>
					<button className="chart-month">month</button>
				</div>

				<div className="Chart-Score">
					<Line className="custom-chart" {...config} />
				</div>
			</div>
		</div>
	);
}

export default Statistical;
