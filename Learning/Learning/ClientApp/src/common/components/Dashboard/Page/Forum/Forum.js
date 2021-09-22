import React, { useEffect, useState } from "react";
import axios from "axios";
import "./Forum.css";
import StudyForum from "../../../../../images/studyforum.png";
import AuthHeader from "../../../../Service/AuthHeader";
// import { getCurrentUser } from "../../../../Service/AuthService";
import { getCurrentIdUser } from "../../../../Service/AuthService";

function Forum() {
	// const userName = getCurrentUser();
	const url =
		"https://todoapi20210730142909.azurewebsites.net/api/Student/create-comment";
	const urlListData =
		"https://todoapi20210730142909.azurewebsites.net/api/Student/comment";
	const [content, setContent] = useState("");
	// const [mes, setMes] = useState("");
	const [listForum, setListForum] = useState([]);
	const IdUser = getCurrentIdUser();

	const onSend = () => {
		axios
			.post(
				url,
				{
					content: content,
					// IdUser: IdUser,
					// user: userName,
				},
				{ headers: AuthHeader(), params: { IDUser: IdUser } }
			)
			.then((res) => {
				// setMes(res.data.data);\
				setListForum([...listForum, res.data.data]);
				console.log([...listForum, res.data.data]);
			})
			.catch((err) => console.log(err));

		// setContent("");
	};
	//
	useEffect(() => {
		const Forum = () => {
			axios
				.get(urlListData, { params: { IDUser: IdUser }, headers: AuthHeader() })
				.then((res) => {
					console.log(res.data.data);
					setListForum(res.data.data);
				})
				.catch((err) => console.log(err));
		};
		Forum();
	}, [IdUser, urlListData]);
	return (
		<div className="wrapper-dashboard">
			<h1 className="chat-title">Forum</h1>
			<div className="form-contact">
				<div className="form-chat">
					<div className="chat-content">
						{listForum.map((item, id) => (
							<div key={id} className="chat-user">
								<img alt="" src={StudyForum} />
								<span>{item.content}</span>
							</div>
						))}
					</div>
					<div className="chat-bar">
						<input
							onChange={(e) => setContent(e.target.value)}
							className="chat-input"
							type="text"
							placeholder="nhập tin nhắn...?"
						/>
						<button onClick={onSend} className="search-btn chat-margin">
							Send
						</button>
					</div>
				</div>
				<div>
					<img className="chat-img" alt="" src={StudyForum} />
				</div>
			</div>
		</div>
	);
}

export default Forum;
