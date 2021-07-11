import React from "react";
import "./App.css";
import { VideoAddForm } from "./components/VideoForm";
import VideoList from "./components/VideoList";
import { Route } from "react-router-dom";

function App() {
  return (
    <div className="App">
      <Route>
      <VideoAddForm />
      <VideoList />
      </Route>
    </div>
  );
}

export default App;
