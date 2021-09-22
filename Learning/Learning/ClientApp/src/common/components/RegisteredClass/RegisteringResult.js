import React, { useEffect, useState } from "react";
import "../DetailCourse/DetailCourse.css";
import "../Search/Search.css";
import SearchBar from "../Search/SearchBar";
import ItemRegisteredClass from "./ItemRegisteredClass";
import axios from "axios";
import { getCurrentIdUser } from "../../Service/AuthService";
import AuthHeader from "../../Service/AuthHeader";
import Header from "../Home/Header";
// import { useParams } from "react-router";

function RegisteringResult() {
	/*Get login user */
	const [currentIdUser, setCurrentIdUser] = useState("");
	const IdUser = getCurrentIdUser();
	useEffect(() => {
		if (IdUser) {
			setCurrentIdUser(IdUser);
		}
	}, [IdUser]);

	/*Show list of registered classes */
	/* api get registered class list*/
	const [registeredclasss, setRegisteredClasss] = useState([]);
	const [loading, setLoading] = useState(false);
	const url3 = `https://todoapi20210730142909.azurewebsites.net/api/Student/get-registered-class-list/${currentIdUser}`;
	useEffect(() => {
		const getPostsData = () => {
			if (IdUser == null) return 0;
			axios
				.get(url3, { headers: AuthHeader(), params: { IDUser: IdUser } })
				.then((res) => {
					setRegisteredClasss(res.data.data);
					setLoading(true);
					console.log(res.data.data);
				});
			/* .catch((error) => console.log(error)); */
		};
		getPostsData();
	}, [url3, IdUser]);

	return (
		<div className="wrapp-full">
			<div className="wrapper-container">
				<Header />
				<div className="wrapp-flex">
					<h1 className="title-enroll">List Registered Classes</h1>
					<div className="list-classes">
						<div className="list-outstand">
							{loading ? (
								registeredclasss.map((item, id) => (
									<ItemRegisteredClass key={id} item={item} />
								))
							) : (
								<SearchBar />
							)}
						</div>
					</div>
				</div>
			</div>
		</div>
	);
}

export default RegisteringResult;
