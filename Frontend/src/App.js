import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';
import Room from './components/Room';
import { useState } from 'react';
import RoomCard from './components/Card';
import CardsGrid from './components/CardsGrid';
import {Routes, Route} from "react-router-dom";
import Auth from './components/Auth';
import Header from './components/header/header';
import Home from './components/home/home';
import { useSelector, useDispatch } from 'react-redux';
import { useEffect } from 'react';
import axios from 'axios';
import { API_URL } from './api/axios.api';
import { change_auth } from './redux/features/userSlice';


function App() {

  const [roomNumber, setRoomNumber] = useState(0);

  const dispatch = useDispatch();

  const isAuth = useSelector((state) => state.user.isAuth)

  useEffect(() =>{
    if (localStorage.getItem('token') && localStorage.getItem('token') !== "undefined"){
      axios({
        method:'get',
        url: `${API_URL}/refresh`,
        withCredentials: true
     })
     .then((result)=>{
        console.log('asdasd')
        const {data} = result;
        localStorage.setItem('token', data.access_token)
        dispatch(change_auth(true))
     })
    }
  }, [dispatch])

  return (
    <div>
      <Header />
      <header className="App-header">
        <Routes>
          {/* <Route path='/' element={<CardsGrid />}/> */}
          <Route path='/' element={<Auth />}/>
          {/* <Route path='/' element={<Home />}/> */}
          <Route path='/RoomLofi' element={<Room title="Lofi"/>}/>
          <Route path='/RoomRock' element={<Room title="Rock"/>}/>
          <Route path='/RoomShanson' element={<Room title="Shanson"/>}/>
        </Routes>
        {/* <Room/> */}
      </header>
    </div>
  );
}

export default App;
