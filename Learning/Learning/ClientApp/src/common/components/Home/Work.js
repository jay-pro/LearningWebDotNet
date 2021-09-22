import React from "react";

function Work() {
	return (
		<div className="work">
			<div className="block">
				<span className="block-caption"> service commitment </span>
				<h2 className="block-heading">
					how does
					<br />
					study online?
				</h2>
			</div>
			<div className="work-list">
				<div className="work-item">
					<img src="./images/img2.png" alt="" className="work-img" />
					<h3 className="work-title">Professional learning environment</h3>
					<p className="work-desc text">
						When students participate in online classNamees, they can fully
						grasp their own knowledge. Besides, you can choose from a variety of
						study programs to meet all your specific needs.
					</p>
					<button className="button button--outline work-more">
						Learn more
					</button>
				</div>
				<div className="work-item">
					<img src="./images/img3.png" alt="" className="work-img" />
					<h3 className="work-title">Saving study costs</h3>
					<p className="work-desc text">
						Saving study costs: Online learning reduces about 60% of the cost of
						travel, location, etc. You can register for many courses with low
						cost.
					</p>
					<button className="button button--outline work-more">
						Learn more
					</button>
				</div>
				<div className="work-item">
					<img src="./images/img4.png" alt="" className="work-img" />
					<h3 className="work-title">Optimizing content</h3>
					<p className="work-desc text">
						Most individuals and organizations can design or hire online courses
						to design. However, the level of training will be different so
						students can learn according to their level.
					</p>
					<button className="button button--outline work-more">
						Learn more
					</button>
				</div>
			</div>
		</div>
	);
}

export default Work;
