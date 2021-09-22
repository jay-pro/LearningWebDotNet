import React, { useState } from "react";
import "./DetailProfile.css";
import axios from "axios";
import { getCurrentIdUser } from "../../Service/AuthService";
import AuthHeader from "../../Service/AuthHeader";
import { Image } from "cloudinary-react";

function ModalProfile({ setModalIsOpen, data }) {
	const [email, setEmail] = useState("");
	const [address, setAddress] = useState(data.address);
	const [phone, setPhone] = useState(data.phoneNumber);
	const [fullname, setFullName] = useState(data.fullname);
	const IdUser = getCurrentIdUser();
	/*useEffect(() => {
    if (IdUser) {
      setCurrentIdUser(IdUser);
    }
  }, [IdUser]); */

	const [imageSelected, setImageSelected] = useState("");
	const [dataimage, setDataImage] = useState(data.avatar);
	const uploadImage = () => {
		const formData = new FormData();
		formData.append("file", imageSelected);
		formData.append("upload_preset", "xwfcqasw");
		axios
			.post("https://api.cloudinary.com/v1_1/dbml4nd68/image/upload", formData)
			.then((res) => {
				console.log(res.data.url);
				setDataImage(res.data.url);
			})
			.catch((err) => console.log(err));
	};

	const url = `https://todoapi20210730142909.azurewebsites.net/api/Users/info/update/${IdUser}`;

	// uploadImage();
	function onSubmit() {
		// console.log(fullname);
		// console.log(data);
		// if (fullname === "") {
		//   setFullName(data.fullname);
		// }
		// if (phone === "") {
		//   setPhone(data.phoneNumber);
		// }
		// if (address === "") {
		//   setAddress(data.address);
		// }
		axios
			.put(
				url,
				{
					phoneNumber: phone,
					address: address,
					email: email,
					fullname: fullname,
					avatar: dataimage,
				},
				{ params: { id: IdUser }, headers: AuthHeader() }
			)
			.then((res) => {
				console.log(res.data);
				window.location.reload();
			})
			.catch((err) => console.log("Lỗi." + err));
	}

	return (
		<div className="wrapp-flex">
			<h1 className="title-update">UPDATE</h1>
			<div className="list-update">
				<div className="mdupdate-info">
					<div className="item-update">
						<span>FullName:</span>
						<input
							onChange={(e) => setFullName(e.target.value)}
							type="text"
							placeholder={data.fullname}
							value={fullname}
						/>
					</div>
					<div className="item-update">
						<span>Email:</span>
						<input
							onChange={(e) => setEmail(e.target.value)}
							type="text"
							placeholder="email..."
							value={data.email}
						/>
					</div>
					<div className="item-update">
						<span>Phone:</span>
						<input
							onChange={(e) => setPhone(e.target.value)}
							type="text"
							placeholder={data.phone}
							value={phone}
						/>
					</div>
					<div className="item-update">
						<span>Address:</span>
						<input
							value={address}
							onChange={(e) => setAddress(e.target.value)}
							type="text"
							placeholder="address..."
						/>
					</div>
				</div>
				<div className="create-course-item">
					<Image
						style={{ width: 200, height: 200 }}
						cloudName="dbml4nd68"
						publicId={dataimage}
						src={dataimage}
					/>
					<input
						onChange={(e) => setImageSelected(e.target.files[0])}
						className="course-desc"
						type="file"
					/>
					<button className="btn-loadImg-profile" onClick={uploadImage}>
						upload Image
					</button>
				</div>
			</div>

			<div className="btn-save-profile">
				<button onClick={onSubmit} className="btn-save">
					Save
				</button>
			</div>
		</div>
	);
}

export default ModalProfile;
