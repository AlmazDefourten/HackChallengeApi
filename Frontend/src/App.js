import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';
import Room from './components/Room';
import { useState } from 'react';
import RoomCard from './components/Card';
import CardsGrid from './components/CardsGrid';
import {Routes, Route, Navigate} from "react-router-dom";
import Auth from './components/Auth';
import Header from './components/header/header';
import Home from './components/home/home';
import { useSelector, useDispatch } from 'react-redux';
import { useEffect } from 'react';
import axios from 'axios';
import { API_URL } from './api/axios.api';
import { change_auth } from './redux/features/userSlice';
import { socket } from './components/socket';
import Radio from './components/Radio';
import AudioChat from './components/Radio2';

function App() {

  const [roomNumber, setRoomNumber] = useState(0);

  const dispatch = useDispatch();

  const isAuth = useSelector((state) => state.user.isAuth)

  useEffect(() =>{

    // function onConnect() {
    //   console.log('connected');
    // }

    // function onDisconnect() {
    //   console.log('disconnected');
    // }

    // socket.on('connect', onConnect);
    // socket.on('disconnect', onDisconnect);

    if (localStorage.getItem('accessToken') && localStorage.getItem('accessToken') !== "undefined"){
      // console.log(123)
    //   axios({
    //     method:'get',
    //     url: `${API_URL}/refresh`,
    //     withCredentials: true,
    //     headers:{
    //       refreshToken: localStorage.getItem('refreshToken')
    //     }
    //  })
    axios.post(`${API_URL}/refresh`, {
      refreshToken: localStorage.getItem('refreshToken')
    })
     .then((result)=>{
        // console.log('asdasd')
        const {data} = result;
        // console.log(data)
        localStorage.setItem('accessToken', data.accessToken)
        localStorage.setItem('refreshToken', data.refreshToken)
        dispatch(change_auth(true))
     })
     .catch(() => alert('Ошибка refresh-токена. Если данная ошибка появляется много раз, очистите локальное хранилище'))
    }
  }, [dispatch])

  return (
    <div>
      <Header />
      <header className="App-header">
        {isAuth ?
        <Routes>
          <Route path='/' element={<CardsGrid />}/>
          {/* <Route path='/' element={<AudioChat />}/> */}
          {/* <Route path='/' element={<Radio />}/> */}
          {/* <Route path='/' element={<Home />}/> */}
          {/* <Route path='/RoomLofi' element={<Room title="Lofi"/>}/>
          <Route path='/RoomRock' element={<Room title="Rock"/>}/>
          <Route path='/RoomShanson' element={<Room title="Shanson"/>}/> */}
        </Routes>
        :
        <Routes>
          <Route path='/' element={<Auth />}/>
          <Route path="*" element={ <Navigate to='/' replace /> }/>
        </Routes>
        }
      </header>
    </div>
  );
}

export default App;
