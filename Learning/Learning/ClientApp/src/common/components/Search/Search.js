import axios from "axios";
import React, { useEffect, useState } from "react";
import Header from "../Home/Header";
import ItemCourse from "../ItemCourse/ItemCourse";
import "./Search.css";
import SearchBar from "./SearchBar";
import AuthHeader from "../../Service/AuthHeader";

function Search() {
  const [course, setCourse] = useState([]);
  const [seacrhName, setSearchName] = useState("");
  const [isSearch, setIsSearch] = useState(false);
  const [loading, setLoading] = useState(false);

  const url = "https://todoapi20210730142909.azurewebsites.net/api/Courses";

  useEffect(() => {
    const getPostsData = () => {
      if (!isSearch) {
        axios
          .get(url, { headers: AuthHeader() })
          .then((res) => {
            setCourse(res.data.data);
            setLoading(true);
            console.log(res.data.data);
          })
          .catch((error) => alert(error));
      }
    };
    getPostsData();
  }, [url, isSearch]);
  console.log(loading);

  // Search Data
  const urlSearch = `https://todoapi20210730142909.azurewebsites.net/api/Student/search-course/${seacrhName}`;
  useEffect(() => {
    const searchData = () => {
      if (seacrhName === "") {
        setIsSearch(false);
      } else {
        axios
          .get(urlSearch, { headers: AuthHeader() })
          .then((res) => {
            setIsSearch(true);
            setCourse(res.data.data);
          })
          .catch((error) => console.log(error));
      }
    };
    searchData();
  }, [urlSearch, seacrhName]);

  return (
    <div className="wrapp-full">
      <div className="wrapper-container">
        <Header />
        <div className="form-search">
          <h1>CÁC KHÓA HỌC</h1>
          <div className="search-bar">
            <input
              className="search-input"
              type="text"
              placeholder="search...?"
              onChange={(e) => setSearchName(e.target.value)}
            />
          </div>
          <div className="search-list-course">
            <div className="list-outstand">
              {loading ? (
                course.map((item, id) => <ItemCourse key={id} item={item} />)
              ) : (
                <SearchBar />
              )}
              {/* {course.map((item, id) => (
							<ItemCourse key={id} item={item} />
							))} */}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Search;
