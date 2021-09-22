import React, { useEffect, useState } from "react";
import axios from "axios";
import { getCurrentIdUser } from "../../../../Service/AuthService";
import AuthHeader from "../../../../Service/AuthHeader";
// import { useParams } from "react-router";
import ItemClassTeacher from "./ItemClassTeacher";
import Header from "../../../Home/Header";
import SearchBar from "../../../Search/SearchBar";

function ClassTeacher() {
	const [ClassAssign, setClassAssign] = useState([]);
	const IdUser = getCurrentIdUser();

	const [loading, setLoading] = useState(false);
	const url3 = `https://todoapi20210730142909.azurewebsites.net/api/Teachers/get-classes`;
	useEffect(() => {
		const getPostsData = () => {
			axios
				.get(url3, { headers: AuthHeader(), params: { IDUser: IdUser } })
				.then((res) => {
					setClassAssign(res.data.data);
					console.log(res.data);
					setLoading(true);
				})
				.catch((err) => console.log(err));
		};
		getPostsData();
	}, [url3, IdUser]);

	return (
		<div className="wrapp-full">
			<Header />
			<div className="wrapp-flex">
				<h1 className="title-enroll">List Registered Classes</h1>
				<div className="list-classes">
					<div className="list-outstand">
						{loading ? (
							ClassAssign.map((item, id) => (
								<ItemClassTeacher key={id} item={item} />
							))
						) : (
							<SearchBar />
						)}
					</div>
				</div>
			</div>
		</div>
	);
}

export default ClassTeacher;
