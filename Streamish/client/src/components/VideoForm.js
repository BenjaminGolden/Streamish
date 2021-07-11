import React, { useEffect, useState } from "react";
import { addVideo } from '../modules/videoManager'
import { useHistory } from 'react-router-dom';
import { getAllVideosWithComments } from "../modules/videoManager";
import { VideoList } from './VideoList';
 

export const VideoAddForm = (getVideos) => {

    // const currentUser = parseInt(sessionStorage.getItem("nutshell_user"));
    // const date = Date.now();
    const [videos, setVideos] = useState([]);
    const [video, setVideo] = useState({
        
        title: "",
        description: "",
        url: "",
       
        });
    
     const [isLoading, setIsLoading] = useState(false);
    
     const history = useHistory();
    
    const handleControlledInputChange = (evt) => {

        const newVideo = { ...video }
        let selectedVal = evt.target.value
        // forms always provide values as strings. But we want to save the ids as numbers.
        if (evt.target.id.includes("Id")) {
            selectedVal = parseInt(selectedVal)
        }

        newVideo[evt.target.id] = selectedVal
        // update state
        setVideo(newVideo)
    }
    
    const handleClickSaveVideo = (evt) => {
        evt.preventDefault() //Prevents the browser from submitting the form
        setIsLoading(true)
            addVideo(video)
            .then(() => setVideo({
                title: "",
                description: "",
                url: ""
            }))
            getAllVideosWithComments()
            .then(videos => setVideos(videos))
        
    }
    
    return (
        <form className="articleForm">
                <h2 className="articleForm__title">New Video</h2>
            <fieldset>
                <div>
                    <label className="eventFormLabel" >Video Title:</label>
                    <input type="text" id="title" onChange={handleControlledInputChange} required autoFocus className="from-control" placeholder="Video Title" value={video.title} />
                </div>
            </fieldset>
            <fieldset>
                <div>
                    <label className="eventFormLabel" >Video description:</label>
                    <input type="text" id="description" onChange={handleControlledInputChange} required autoFocus className="form-control" placeholder="Description" value={video.description}/>
                
                </div>
            </fieldset>
            <fieldset>
                <div>
                    <label className="eventFormLabel" >URL:</label>
                    <input type="text" id="url" onChange={handleControlledInputChange} required autoFocus className="form-control" placeholder="URL" value={video.url}/>
                </div>
            </fieldset>
                <button className="saveVideo" onClick={handleClickSaveVideo}>Save Video</button>
        </form>
        )
    };