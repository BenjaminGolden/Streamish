import React, { useEffect, useState } from "react";
import Video from './Video';
import { getAllVideosWithComments, searchVideos } from "../modules/videoManager";
import { VideoForm } from "./VideoForm";



const VideoList = () => {
  const [videos, setVideos] = useState([]);
  const [search, setSearch] = useState("")



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

  const handleSearch =(evt) => {
    evt.preventDefault()
    let searchInput = evt.target.value
    setSearch(searchInput)
  }


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

export default VideoList;