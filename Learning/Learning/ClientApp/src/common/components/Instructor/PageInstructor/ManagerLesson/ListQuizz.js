import { Radio } from "antd";
import React from "react";

function ListQuizz() {
	return (
		<div className="wrapper-flex ">
			<div className="quizz-item">
				<span className="quizz-question">
					Câu 1: Tên của công ty công nghệ lớn nhất ở Hàn Quốc là gì?
				</span>
				<Radio.Group>
					<Radio className="radio-custom" value={1}>
						A<span style={{ marginLeft: 10 }}>daA</span>
					</Radio>
					<Radio className="radio-custom" value={2}>
						B<span style={{ marginLeft: 10 }}>daB</span>
					</Radio>
					<Radio className="radio-custom" value={3}>
						C<span style={{ marginLeft: 10 }}>daC</span>
					</Radio>
					<Radio className="radio-custom" value={4}>
						D<span style={{ marginLeft: 10 }}>daD</span>
					</Radio>
				</Radio.Group>
			</div>
		</div>
	);
}

export default ListQuizz;
