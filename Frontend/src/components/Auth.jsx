import { useState } from "react";
import {Form, Button } from 'react-bootstrap';
import $api from "../api/axios.api";
import { useDispatch } from 'react-redux';
import { change_auth } from '../redux/features/userSlice';

function Auth() {

    const dispatch = useDispatch();

    const [isLoged, setIsLoged] = useState(false);
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const regURL = '/register'
    // const regURL = '/api/Test'

    const onSubmitRegistration = (email, password) => $api.post(regURL, {
        email,
        password,
    })
    .catch(() => alert('Введены некорректыне данные или ошибка сервера'));
    // .then(  console.error('Error invoking SendTestAudio:', error)
    //     (result) => {
    //         // console.log(result);
    //         console.log("AAAAAAAAAAAAAAAA");
    //         const {data} = result;
    //         console.log(result)
    //         localStorage.setItem('token', data.access_token)
    //         console.log(localStorage.getItem('token'))
    //     });


    const authURL = '/login'

    const onSubmitAuth = (email, password) => $api.post(authURL, {
        email,
        password,
    })
    .then(
        (result) => {
            const {data} = result;
            // console.log(data)
            localStorage.setItem('accessToken', data.accessToken)
            localStorage.setItem('refreshToken', data.refreshToken)
            dispatch(change_auth(true))
        })
    .catch(() => alert('Почта занята или введены некорректыне данные'));
    alert('Пароль дожен состоять миниму из 9 цифр, иметь заглавные и прописные буквы, цифры, знаки')

    return ( 
        <>
        {isLoged ? "Войти" : "Регистрация"}
        <Form>
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
                <Form.Label>Адрес почты</Form.Label>
                <Form.Control type="email" placeholder="test@mail.ru" onChange={(e)=>{setEmail(e.target.value)}}/>
            </Form.Group>
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput2">
                <Form.Label>Пароль</Form.Label>
                <Form.Control type="email" placeholder="123456789" onChange={(e)=>{setPassword(e.target.value)}}/>
            </Form.Group>
        </Form>
        <div style={{display: 'flex', alignItems: 'center'}}>
            {isLoged ? 
            <Button onClick={() =>{
                onSubmitAuth(email, password)
            }}>
                Войти
            </Button> :
             <Button onClick={() =>{
                onSubmitRegistration(email, password)
             }}>
                Зарегистрироваться
            </Button>}
        </div>
        {isLoged ? 
        <div style={{display: 'flex', alignItems: 'center', flexDirection:'column'}}>
            <p>Нет аккаунта?</p>
            <Button variant="primary" onClick={()=>{setIsLoged(prev => !prev)}}>Создать аккаунт</Button>
        </div> :
        <div style={{display: 'flex', alignItems: 'center', flexDirection:'column'}}>
            <p>Есть аккаунт?</p>
            <Button variant="primary" onClick={()=>{setIsLoged(prev => !prev)}}>Войти в аккаунт</Button>
        </div>}
        </>
     );
}

export default Auth;