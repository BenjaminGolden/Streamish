import React, { useEffect, useState } from "react";
import Video from './Video';
import { getAllVideosWithComments, searchVideos } from "../modules/videoManager";
import { VideoForm } from "./VideoForm";


//The Video list is what is passed in to the App.js, which is loaded on the dom to display JSX. 
//Video.js, which contains the card for each video that is to be rendered on the dom, is brought in to the VideoList. 
//VideoForm is also passed into the VideoList, which is called in the JSX and displayed above the video list on the DOM.

const VideoList = () => {
  const [videos, setVideos] = useState([]);
  const [search, setSearch] = useState("")


//here we are saying if the search field is empty, then getAllVideosWithComments() and set those videos in state. 
//if there is something in the search field, display videos that match the input in the search field
  const getVideos = () => {
      if (search === "")
      {
        getAllVideosWithComments()
        .then(videos => setVideos(videos))
      }
      else
      {
          searchVideos(search).then(videos => setVideos(videos));
      }
    
  };

  //the handleSearch function saves the input value that the user types in the search bar in the variable searchInput. the state of search (setSearch) is then set with the search input. 
  const handleSearch =(evt) => {
    evt.preventDefault()
    let searchInput = evt.target.value
    setSearch(searchInput)
  }

// the useEffect is the first thing to load on the page with the DOM is rendered. getVideos function is called and the results are set in state. then the JSX loads which calls the Video form, and maps over the video card from Video.js to create individual cards on the dom with the video objects contained in state. 
  useEffect(() => {
    getVideos();
  }, [search]);

  

  return (
      <>
      <VideoForm getVideos={getVideos}/>
    <div className="container">
      <div className="row justify-content-center">
        <div >  
    <input type='text' className="search" required onChange={handleSearch} id="search_box" placeholder="Search"/>
    </div>
        {videos.map((video) => (
          <Video video={video} key={video.id} />
        ))}
      </div>
    </div>
    </>
  );
};

//VideoList is imported in App.js which loads all of the content on the DOM
export default VideoList;