import {Button, Alert, Stack} from 'react-bootstrap';
import {Link} from "react-router-dom";
function Room({title}) {
    return (
        <>
        {/* <Alert variant='Light' className='asd'>Вы находитесь в комнате: {title}</Alert> */}
        <p>Вы находитесь в комнате: {title}</p>
        {/* <Alert variant='Light' className='asd'>В данный момент проигрывается: {"{название трека или радио}"}</Alert> */}
        <p>В данный момент проигрывается: {"{songName}"}</p>
        <Link to='/'>
            <Button variant='primary'>Выйти из комнаты</Button>
        </Link>
        </> 
     );
}

export default Room;