import { useState } from "react";
import {Form, Button } from 'react-bootstrap';
import { AuthService } from "../services/auth.service";

function Auth() {

    const [isLoged, setIsLoged] = useState(false);
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

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
            <Button>{isLoged ? "Войти" : 'Зарегистрироваться'}</Button>
        </div>
        {isLoged ? 
        <div style={{display: 'flex', alignItems: 'center', flexDirection:'column'}}>
            <p className="font-mts-wide-medium">Нет аккаунта?</p>
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