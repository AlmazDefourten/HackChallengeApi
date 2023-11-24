import {Button, Alert, Form, Container, Row, Col, Card } from 'react-bootstrap';
import RoomCard from './Card';
import {Link} from "react-router-dom";

function CardsGrid() {
    return ( <>
        <Container>
            <p style={{textAlign: 'center'}}>Нажмите на карточку, чтобы перейти в комнату прослушивания</p>
            <Row>
                <Col className='d-flex justify-content-center m-4'>
                    <Link to='/RoomLofi'>
                        <RoomCard title='Lofi'description={`beats to relax/study to`} theme='success'/>
                    </Link>
                </Col>
                <Col className='d-flex justify-content-center m-4'>
                    <Link to='/RoomRock'>
                        <RoomCard title='Rock'description={`WE ROOOOOCK!`} theme='danger'/>
                    </Link>
                </Col>
                <Col className='d-flex justify-content-center m-4'>
                    <Link to='/RoomShanson'>
                        <RoomCard title='Shanson'description={`Some quick example text to build on the card title and make up the
                            bulk of the card's content.`} theme='warning'/>
                    </Link>
                </Col>
            </Row>
            {/* <Row>
                <Col className='d-flex justify-content-center m-4'>
                    <Link to='/RoomLofi'>
                        <RoomCard title='Lofi'description={`beats to relax/study to`} theme='success'/>
                    </Link>
                </Col>
            </Row>
            <Row>
                <Col className='d-flex justify-content-center m-4'>
                    <Link to='/RoomRock'>
                        <RoomCard title='Rock'description={`WE ROOOOOCK!`} theme='danger'/>
                    </Link>
                </Col>
            </Row>
            <Row>
                <Col className='d-flex justify-content-center m-4'>
                    <Link to='/RoomShanson'>
                        <RoomCard title='Shanson'description={`Some quick example text to build on the card title and make up the
                            bulk of the card's content.`} theme='warning'/>
                    </Link>
                </Col>
            </Row> */}
        </Container>
    </> );
}

export default CardsGrid;