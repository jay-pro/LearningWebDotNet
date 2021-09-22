import React from "react";
import { BrowserRouter as Router, Link } from "react-router-dom";
import "./Home.css";
import Logo from "../../../images/logo.png";
function Footer() {
	return (
		<Router>
			<div className="footer">
				<div className="footer-column">
					<a href="index.html" className="header-logo">
						<img src={Logo} alt="" className="header-logo-image" />
					</a>
					<p className="text text--dark footer-desc">
						let's work together to create fantastic product
					</p>
					<div className="social">
						<div className="social-link bg-google">
							<i className="fa fa-google"></i>
						</div>
						<div className="social-link bg-instagram">
							<i className="fa fa-instagram"></i>
						</div>
						<div className="social-link bg-twitter">
							<i className="fa fa-twitter"></i>
						</div>
					</div>
				</div>

				<div className="footer-column">
					<h3 className="footer-heading">Course</h3>
					<ul className="footer-links">
						<li className="footer-link-item">
							<Link to="" className="footer-link">
								landing page
							</Link>
						</li>
						<li className="footer-link-item">
							<Link to="" className="footer-link">
								Contact
							</Link>
						</li>
						<li className="footer-link-item">
							<Link to="" className="footer-link">
								Group
							</Link>
						</li>
					</ul>
				</div>
				<div className="footer-column">
					<h3 className="footer-heading">Services</h3>
					<ul className="footer-links">
						<li className="footer-link-item">
							<Link to="" className="footer-link">
								Suppost
							</Link>
						</li>
						<li className="footer-link-item">
							<Link to="" className="footer-link">
								Service
							</Link>
						</li>
					</ul>
				</div>
				<div className="footer-column">
					<h3 className="footer-heading">Company</h3>
					<ul className="footer-links">
						<li className="footer-link-item">
							<Link to="" className="footer-link">
								About
							</Link>
						</li>
						<li className="footer-link-item">
							<Link to="" className="footer-link">
								Terms
							</Link>
						</li>
					</ul>
				</div>
			</div>
		</Router>
	);
}

export default Footer;
