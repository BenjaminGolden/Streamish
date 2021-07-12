import React from "react";
import { Card, CardBody } from "reactstrap";
import { Link } from "react-router-dom";

//the Video function is responsible for creating the individual cards associated with each saved video. The Video function is then imported in the VideoList and called in the JSX. We then map over all of the videos, creating a card for each video in the DB. 

const Video = ({ video, name }) => {
  return (
    <Card >
      {video.userProfile != undefined ? <p className="text-left px-2">Posted by: {video.userProfile.name}</p>: <p className="text-left px-2">Posted by: {name}</p>}
      <CardBody>
        <iframe className="video"
          src={video.url}
          title="YouTube video player"
          frameBorder="0"
          allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
          allowFullScreen />

        <Link to={`/videos/${video.id}`}>
            <strong>{video.title}</strong>
        </Link>
        <p>{video.description}</p>
        {video.comments?.map(c => {return <p>{c.message}</p>})}
      </CardBody>
    </Card>
  );
};

export default Video;