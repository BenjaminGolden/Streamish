import React from "react";
import "./App.css";
import { VideoForm } from "./components/VideoForm";
import VideoList from "./components/VideoList";
import { Route } from "react-router-dom";

function App() {
  return (
    <div className="App">
      <Route>
      <VideoList />
      </Route>
    </div>
  );
}

export default App;
