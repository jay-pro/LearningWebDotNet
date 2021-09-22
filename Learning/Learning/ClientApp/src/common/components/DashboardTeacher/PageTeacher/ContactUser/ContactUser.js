import React from "react";
import chatImg from "../../../../../images/chat.png";
function ContactUser() {
	return (
		<div className="wrapper-dashboard">
			<h1 className="chat-title">Chat</h1>
			<div className="form-contact">
				<img className="chat-img" alt="" src={chatImg} />
				<div className="form-chat">
					<div className="chat-content">
						<div className="chat-teacher">
							<img alt="" src={chatImg} />
							<span>Em hỏi bài xíu ạ</span>
						</div>
					</div>
					<div className="chat-bar">
						<input
							className="chat-input"
							type="text"
							placeholder="nhập tin nhắn...?"
						/>
						<button className="search-btn chat-margin">Send</button>
					</div>
				</div>
			</div>
		</div>
	);
}

export default ContactUser;
