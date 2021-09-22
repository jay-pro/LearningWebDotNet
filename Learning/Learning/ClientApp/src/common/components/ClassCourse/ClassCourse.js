import React from "react";
// import axios from "axios";
// import { useParams } from "react-router";
import { Link } from "react-router-dom";
import Header from "../Home/Header";

function ClassCourse() {
	return (
		<div className="wrapp-full">
			<div className="wrapper-container">
				<Header />
				<div className="form-detail-course">
					{/* <h1>CHI TIẾT KHÓA HỌC</h1> */}
					<div className="course-detail-item">
						{/* <h2 className="title-course-item"></h2> */}
						<div className="item-course-info">
							<img
								className="item-course-img"
								src="http://3.bp.blogspot.com/-OT90z2H-iv4/VG9lLu-DA7I/AAAAAAAAAE4/nlAB24yZHtE/s1600/cho%2Bthu%2Bphong%2Bhoc%2Btai%2Bha%2Bnoi.jpg"
								alt=""
							/>
							<div className="course-info-right">
								<div className="fill-course-item">
									<h2>Name Create:</h2>
									<span>Instructor</span>
								</div>
								<div className="fill-course-item">
									<h2>Description:</h2>
									<span className="detail-course-desc"></span>
								</div>
								<div className="fill-course-item">
									<h2>Duration:</h2>
									{/* <span>{idCourse.duration} hour</span> */}
								</div>
								{/* <div className="fill-course-item">
									<h2>number of member:</h2>
									<span>600</span>
								</div> */}
								<Link to="" className="button-participate">
									Study
								</Link>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
}

export default ClassCourse;
