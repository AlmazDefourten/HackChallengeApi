import React, { useState, useEffect } from 'react';

const AudioPlayer = ({ audioData }) => {
  const [audioSrc, setAudioSrc] = useState(null);

  useEffect(() => {
    if (audioData) {
      const blob = new Blob([audioData], { type: 'audio/wav' });
      setAudioSrc(URL.createObjectURL(blob));
    }
  }, [audioData]);

  return (
    <audio controls>
      <source src={audioSrc} type="audio/wav" />
      Your browser does not support the audio element.
    </audio>
  );
};

export default AudioPlayer;