import React, { useState, useEffect } from 'react';
import * as signalR from '@microsoft/signalr';

const AudioChat = () => {
  const [audioMessages, setAudioMessages] = useState([]);
  const [message, setMessage] = useState('');
  const [hubConnection, setHubConnection] = useState(null);

  useEffect(() => {
    const connection = new signalR.HubConnectionBuilder()
      .withUrl("http://31.129.105.161/audiohub")
      .build();

    connection.on("SendTestAudio", (data) => {
      setAudioMessages(prevMessages => [...prevMessages, data]);
    });

    connection.start()
      .then(() => {
        console.log("Connected to AudioHub");
        setHubConnection(connection);
      })
      .catch(err => console.error(err));

    return () => {
      if (connection.state === signalR.HubConnectionState.Connected) {
        connection.stop();
      }
    };
  }, []); 

  const sendMessage = () => {
    if (hubConnection && hubConnection.state === signalR.HubConnectionState.Connected) {
        hubConnection.invoke("SendTestAudio", message)
        .then(() => setMessage(''))
        .catch(error => console.error('Error invoking SendTestAudio:', error));
      setMessage('');
    }
  };

  return (
    <div>
      <div id="chatroom">
        {audioMessages.map((audioMessage, index) => (
          <p key={index}>{audioMessage}</p>
        ))}
      </div>
      <input
        type="text"
        id="message"
        value={message}
        onChange={(e) => setMessage(e.target.value)}
      />
      <button id="sendBtn" onClick={sendMessage}>Send</button>
    </div>
  );
};

export default AudioChat;