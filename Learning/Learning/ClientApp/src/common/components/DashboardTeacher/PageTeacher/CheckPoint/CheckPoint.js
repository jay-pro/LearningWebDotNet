import React from "react";
import "./CheckPoint.css";

function CheckPoint() {
	return (
		<div className="wrapper-dashboard ">
			<div className="wrapper-flex box-check-point">
				<div className="lesson-check-point">
					<div className="item-check-point">
						<span className="title-check-point">Thời gian</span>
						<span className="desc-submit-lesson">21/9/2021</span>
					</div>
					<div className="item-check-point">
						<span className="title-check-point">Mã lớp</span>
						<span className="desc-submit-lesson">ST5</span>
					</div>
					<div className="item-check-point">
						<span className="title-check-point">Tên</span>
						<span className="desc-submit-lesson">Người lạ</span>
					</div>
					<div className="item-check-point">
						<span className="title-check-point">Tên file nộp</span>
						<span className="desc-submit-lesson">lesson1.txt</span>
					</div>
					<div className="item-check-point">
						<input type="text" placeholder="nhập điểm" />
					</div>

					<button className="button-check-point">Save</button>
				</div>
			</div>
		</div>
	);
}

export default CheckPoint;
