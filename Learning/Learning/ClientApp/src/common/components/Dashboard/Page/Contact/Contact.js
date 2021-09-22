import React from "react";
import "./Contact.css";
import chatImg from "../../../../../images/chat.png";
function Contact() {
	return (
		<div className="wrapper-dashboard">
			<h1 className="chat-title">Liên hệ giáo viên</h1>
			<div className="form-contact">
				<img className="chat-img" alt="" src={chatImg} />
				<div className="form-chat">
					<div className="chat-content">
						<div className="chat-teacher">
							<img alt="" src={chatImg} />
							<span>Cô có thể giúp gì được cho em ko ??</span>
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

export default Contact;
