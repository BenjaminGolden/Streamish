import React, { useEffect, useState } from "react";
import { addVideo } from '../modules/videoManager'
import { useHistory } from 'react-router-dom';
import { getAllVideosWithComments } from "../modules/videoManager";
import { VideoList } from './VideoList';
 
                            //we can also pass in props instead of ({getVideos})
export const VideoForm = ({getVideos}) => {

    
    const [video, setVideo] = useState({
        
        title: "",
        description: "",
        url: "",
        
    });
    
     const [isLoading, setIsLoading] = useState(false);
    
     const history = useHistory();
    
     //input change function is responsible for listening to any event or change in the input fields and capturing that data to be stored as the value of the corresponding object. 
    const handleControlledInputChange = (evt) => {

        const newVideo = { ...video }
        let selectedVal = evt.target.value
        if (evt.target.id.includes("Id")) {
            selectedVal = parseInt(selectedVal)
        }

        newVideo[evt.target.id] = selectedVal
        // update state
        setVideo(newVideo)
    }
    
    //the handle save function calls the addVideo fetch call, which communicates with the video controller and video repository on the server side. the method in the repository communicates with the database to save the selected object properties. 
    const handleClickSaveVideo = (evt) => {
        evt.preventDefault() //Prevents the browser from submitting the form
        setIsLoading(true)
            addVideo(video)
            //after the new video is saved to the DB, we reset the state of setVideo to empty strings, effectively clearing the input fields on the DOM. 
            .then(() => setVideo({
                title: "",
                description: "",
                url: ""
            }))
            // we would then call props.getVideos()
            // after the video is saved and the fields have been cleared, we call the getVideos function, which has been passed in as a prop, to automatically refresh the page and update state, which adds the new video to the list without the need for the user to refresh the page. 
            .then(() => getVideos())
            // .then(() => history.push('/'))
            
        
    }


    
    return (
        <form className="videoForm">
                <h2 className="videoForm__title">New Video</h2>
            <fieldset>
                <div>
                    <label className="videoFormLabel" >Video Title:</label>
                    <input type="text" id="title" onChange={handleControlledInputChange} required autoFocus className="from-control" placeholder="Video Title" value={video.title} />
                </div>
            </fieldset>
            <fieldset>
                <div>
                    <label className="videoDescription" >Video description:</label>
                    <input type="text" id="description" onChange={handleControlledInputChange} required autoFocus className="form-control" placeholder="Description" value={video.description}/>
                
                </div>
            </fieldset>
            <fieldset>
                <div>
                    <label className="videoFormUrl" >URL:</label>
                    <input type="text" id="url" onChange={handleControlledInputChange} required autoFocus className="form-control" placeholder="URL" value={video.url}/>
                </div>
            </fieldset>
                <button className="saveVideo" onClick={handleClickSaveVideo}>Save Video</button>
        </form>
        )
    };