import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom'
import { getVideosByUser } from '../modules/videoManager';
import Video from './Video';

const UserVideos = () => {
    const [userVideos, setUserVideos] = useState();
    const { id } = useParams();

    const getUserVideos = () => {
        getVideosByUser(id)
        .then(videos => {
            setUserVideos(videos)})
    }

    useEffect(() => {
        getUserVideos();
      }, []);

    return (
        <>
        {/* <VideoForm getVideos={getVideos}/> */}
      <div className="container">
        <div className="row justify-content-center">
          {/* <div >  
      <input type='text' className="search" required onChange={handleSearch} id="search_box" placeholder="Search"/>
      </div> */}
          {userVideos?.videos.map((video) => (
            <Video video={video} name={userVideos.name} key={video.id} />
          ))}
        </div>
      </div>
      </>
    );
  };

  export default UserVideos;