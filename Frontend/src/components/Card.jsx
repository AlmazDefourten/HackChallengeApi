import {Button, Alert, Form, Container, Row, Col, Card } from 'react-bootstrap';


function RoomCard({title, description, theme}) {
    return ( 
        <div>
            <Card style={{ width: '18rem', height: '18rem' }} bg={theme} text='white'>
                <Card.Header>{title}</Card.Header>
                <Card.Body>
                    {/* <Card.Title>{title}</Card.Title> */}
                  <Card.Text>
                    {description}
                  </Card.Text>
                </Card.Body>
              </Card>
        </div>
     );
}

export default RoomCard;