import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';
import {Button, Alert, Form, Container, Row, Col, Card } from 'react-bootstrap';
import Room from './components/Room';
import { useState } from 'react';
import RoomCard from './components/Card';
import CardsGrid from './components/CardsGrid';
import {Routes, Route} from "react-router-dom";
import Auth from './components/Auth';
import Header from './components/header/header';
import Home from './components/home/home'
;
function App() {

  const [roomNumber, setRoomNumber] = useState(0);

  return (
    <div>
      <Header />
      {/* <header className="App-header"> */}
        <Routes>
          {/* <Route path='/' element={<CardsGrid />}/> */}
          {/* <Route path='/' element={<Auth />}/> */}
          <Route path='/' element={<Home />}/>
          <Route path='/RoomLofi' element={<Room title="Lofi"/>}/>
          <Route path='/RoomRock' element={<Room title="Rock"/>}/>
          <Route path='/RoomShanson' element={<Room title="Shanson"/>}/>
        </Routes>
        {/* <Room/> */}
      {/* </header> */}
    </div>
  );
}

export default App;
