import { OmitProps } from "antd/lib/transfer/ListBody";
import axios from "axios";
import React from "react";
import { getCurrentIdUser } from "../../../../Service/AuthService";

function ModalSubmit({ setSubmitIsOpen, IDAssignment }) {
	
	// const url = "https://todoapi20210730142909.azurewebsites.net/api/Student/submit-assignment";
	const url = "https://localhost:44396/api/Student/submit-assignment";
	let formFile = new FormData();
	const IDUser = getCurrentIdUser();

	const onFileChange = (e) => {
		console.log(e.target.files[0]);
		if(e.target && e.target.files[0]) {
			formFile.append('formFile',e.target.files[0]);
			console.log(formFile);
		}
	}

	const submitFile = () => {
		axios.post(url,
			formFile,
			{ params : {IDAssignment: IDAssignment, IDUser: IDUser}})
		.then(()=> {
			setSubmitIsOpen(false);
			console.log("ok");
		})
		.catch(err=> console.log(err.response))
	}

	return (
		<div className="wrapp-flex">
			<div className="form-submit-course">
				<div className="form-submit-info">
					<div className="submit-name-course">
						<span>Name</span>
						<input type="text" placeholder="UserName..." />
					</div>
					<div className="submit-name-course">
						<span>Tên Bài tập</span>
						<input type="text" placeholder="Name Course..." />
					</div>
				</div>
				<div className="submit-file-course">
					<div className="file"></div>
					<input type="file" className="up-file" onChange={onFileChange} />
				</div>
			</div>
			<div className="btn-submit-file">
				<button className="button-file" onClick={submitFile} >SUBMIT</button>
			</div>
		</div>
	);
}

export default ModalSubmit;
