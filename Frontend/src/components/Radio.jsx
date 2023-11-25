import React, { useState, useEffect } from 'react';
// import { HubConnectionBuilder, signalR } from '@microsoft/signalr';
import * as signalR from '@microsoft/signalr';

// import {signalR} from 'react-signalr'
import AudioPlayer from './AudioPlayer';

const Radio = () => {
  const [audioData, setAudioData] = useState(null);

  useEffect(() => {
    // console.log(signalR)
    // Создание подключения к SignalR Hub
    const connection = new signalR.HubConnectionBuilder()
      .withUrl("http://31.129.105.161/audiohub")
      .build();

   

    // Определение обработчика события ReceiveAudioStream
    connection.on("SendTestAudio", (receivedAudioData) => {
      // Обновление состояния с полученными аудиоданными
      setAudioData(receivedAudioData);
    });

    connection.invoke("SendTestAudio");

    // Запуск подключения
    connection.start()

    // Очистка подключения при размонтировании компонента
    return () => {
      if (connection.state === signalR.HubConnectionState.Connected) {
        connection.stop();
      }
    };
  }, []);


  console.log(audioData)

  return (
    <div>
      <h2>Audio Player</h2>
        <audio controls>
          <source src={`data:audio/mp3;base64,${audioData}`} type="audio/mp3" />
          Your browser does not support the audio element.
        </audio>
    </div>
  );
};

export default Radio;