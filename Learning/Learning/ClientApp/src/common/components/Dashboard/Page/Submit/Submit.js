import React, { useState } from "react";
import Modal from "react-modal";
import ModalSubmit from "./ModalSubmit";
import "./Submit.css";
function Submit() {
	const [submitlIsOpen, setSubmitIsOpen] = useState(false);
	return (
		<div className="wrapper-dashboard">
			<h1 className="submit-title">Submit</h1>
			<div className="form-assignment">
				<div className="assignment-item">
					<span className="title-assignment">Bai tap</span>

					<span className="info-assignment">
						Lunar New Year Festival often falls between late January and early
						February; it is among the most important holidays in Vietnam
					</span>
					<button
						onClick={() => setSubmitIsOpen(true)}
						className="btn-submit-assignment"
					>
						them bai nop
					</button>
				</div>
			</div>
			<Modal
				isOpen={submitlIsOpen}
				onRequestClose={() => setSubmitIsOpen(false)}
				style={{
					overlay: {
						backgroundColor: "rgba(0,0,0,0.4)",
					},
					content: {
						width: "40%",
						margin: "auto",
						height: "70%",
					},
				}}
			>
				<ModalSubmit data setSubmitIsOpen={setSubmitIsOpen} />
			</Modal>
		</div>
	);
}

export default Submit;
