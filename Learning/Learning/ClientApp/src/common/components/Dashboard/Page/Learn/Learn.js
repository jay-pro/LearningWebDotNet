import React, { useState, useEffect } from "react";
import "./Learn.css";
import { useParams } from "react-router-dom";
import AuthHeader from "../../../../Service/AuthHeader";
import { getCurrentIdUser } from "../../../../Service/AuthService";
import axios from "axios";
import ReactPlayer from "react-player";

function Learn() {
	const paramUrls = useParams();
	console.log(paramUrls.id);
	const [video, setVideo] = useState([]);
	const IdUser = getCurrentIdUser();
	const url = `https://todoapi20210730142909.azurewebsites.net/api/Courses/course/${paramUrls.id}/course-detail-list`;
	// const url =
	// 	"https://todoapi20210730142909.azurewebsites.net/api/Courses/course/220ba800-fc70-4945-8d0a-0592c9213a00/course-detail-list";
	useEffect(() => {
		async function getData() {
			const res = await axios.get(url, {
				params: { IDUser: IdUser },
				headers: AuthHeader(),
			});
			console.log(res.data.data);
			setVideo(res.data.data.courseDetailList);
		}
		getData();
	}, [url, IdUser]);
	return (
		<div className="wrapper-dashboard ">
			<div className="wrapper-flex">
				<div className="learn-video">
					{video.map((item) => (
						<div key={item.id} className="box-video">
							<span className="title-video">{item.lessonName}</span>
							{/* <iframe
								// width="560"
								height="315"
								src={item.idvideo}
								title="YouTube video player"
								// frameborder="0"
								allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
								// allowfullscreen
							></iframe> */}
							{ item.lessonLink ? (<ReactPlayer url={item.lessonLink}/>): (<div></div>)}	
							{/* { item.material ? (<a href={item.material}>Material</a>) :(<div></div>)}	 */}
						</div>
					))}
				</div>
			</div>
		</div>
	);
}

export default Learn;
