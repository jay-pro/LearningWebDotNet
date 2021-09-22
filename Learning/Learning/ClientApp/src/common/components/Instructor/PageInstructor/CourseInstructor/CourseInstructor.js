import React, { useState, useEffect } from "react";
import axios from "axios";
import SearchBar from "../../../Search/SearchBar";
import CourseItems from "./CourseItems";
import "./CourseInstructor.css";
import AuthHeader from "../../../../Service/AuthHeader";
import { getCurrentIdUser } from "../../../../Service/AuthService";

function CourseInstructor() {
	const [course, setCourse] = useState([]);
	// const [seacrhName, setSearchName] = useState("");
	// const [isSearch, setIsSearch] = useState(false);
	const [loading, setLoading] = useState(false);
	// const [currentIdUser, setCurrentIdUser] = useState(null);

	// useEffect(() => {
	// 	if (IdUser) {
	// 		setCurrentIdUser(IdUser);
	// 	}
	// }, [IdUser]);

	const url =
		"https://todoapi20210730142909.azurewebsites.net/api/Instructor/course";
	const IdUser = getCurrentIdUser();
	useEffect(() => {
		const getPostsData = () => {
			console.log(IdUser);
			axios
				.get(url, { params: { IDUser: IdUser }, headers: AuthHeader() })
				.then((res) => {
					setCourse(res.data.data);
					setLoading(true);
					console.log(res.data.data);
				})
				.catch((error) => alert(error));
		};
		getPostsData();
	}, [url, IdUser]);
	console.log(loading);

	return (
		<div>
			<div className="form-search">
				<h1 className="title-course-instructor">LIST COURSE</h1>

				<div className="search-list-course">
					<div className="list-outstand">
						{loading ? (
							course.map((item, id) => <CourseItems key={id} item={item} />)
						) : (
							<SearchBar />
						)}
					</div>
				</div>
			</div>
		</div>
	);
}

export default CourseInstructor;
