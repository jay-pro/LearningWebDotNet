import React from "react";
import Banner from "./Banner";
import Feedback from "./Feedback";
import Footer from "./Footer";
import Header from "./Header";
import Introl from "./Introl";
import Work from "./Work";
// import SliderImg from "./SliderImg";
// import Outstanding from "./Outstanding";

function Home() {
  return (
    <div className="wrapp">
      <div className="wrapper-container">
        <Header />
        <Banner />
        <Work />
        <Introl />
        <Feedback />
        <Footer />
      </div>
    </div>
  );
}

export default Home;
