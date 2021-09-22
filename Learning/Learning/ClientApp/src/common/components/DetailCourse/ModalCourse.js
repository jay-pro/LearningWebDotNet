import React, { useEffect, useState } from "react";
import "./DetailCourse.css";
import "../Search/Search.css";
import SearchBar from "../Search/SearchBar";
import ItemClass from "../ItemClass/ItemClass";
import axios from "axios";
import { getCurrentIdUser } from "../../Service/AuthService";
import AuthHeader from "../../Service/AuthHeader";
import { useParams } from "react-router";

function ModalCourse() {
	const [classs, setClasss] = useState([]);
	const { id } = useParams();
	const IdUser = getCurrentIdUser();

	const url3 = `https://todoapi20210730142909.azurewebsites.net/api/Student/get-class-list?IDCourse=${id}`;
	const [loading, setLoading] = useState(false);
	useEffect(() => {
		const getPostsData = () => {
			axios
				.get(url3, { headers: AuthHeader(), params: { IDUser: IdUser } })
				.then((res) => {
					setClasss(res.data.data);
					setLoading(true);
					console.log(res.data.data);
				})
				.catch((error) => alert(error));
		};
		getPostsData();
	}, [url3, IdUser]);
	console.log(loading);

	return (
		<div className="wrapp-full">
			<div className="wrapp-flex">
				<h1 className="title-enroll">List Classes</h1>
				<div className="list-classes">
					<div className="list-outstand">
						{loading ? (
							classs.map((item, id) => <ItemClass key={id} item={item} />)
						) : (
							<SearchBar />
						)}
					</div>
				</div>
			</div>
		</div>
	);
}

export default ModalCourse;
