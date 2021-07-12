const baseUrl = '/api/video';

//fetch calls in the manager communicate with the database to return requested information. 

export const getAllVideos = () => {
  return fetch(baseUrl)
    .then((res) => res.json())
};

export const getAllVideosWithComments = () => {
    return fetch(baseUrl + '/GetWithComments')
    .then((res) => res.json())
};

export const searchVideos = (string) => {
    return fetch(baseUrl + '/search?q=' + string)
    .then((res) => res.json())
};

export const addVideo = (video) => {
  return fetch(baseUrl, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(video),
  });
};