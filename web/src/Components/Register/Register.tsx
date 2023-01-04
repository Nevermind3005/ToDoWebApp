import { Button, Card, Form, Stack } from 'react-bootstrap';

const Register = () => {
    return (
        <Card style={{ width: '35rem' }}>
            <Card.Body>
                <Card.Title className='text-center'>Register</Card.Title>
                <Form>
                    <Stack gap={3}>
                        <Form.Group>
                            <Form.Label>Username</Form.Label>
                            <Form.Control
                                type='text'
                                placeholder='Username'
                                required
                            ></Form.Control>
                        </Form.Group>
                        <Form.Group>
                            <Form.Label>Email</Form.Label>
                            <Form.Control
                                type='email'
                                placeholder='Your email'
                                required
                            ></Form.Control>
                        </Form.Group>
                        <Form.Group>
                            <Form.Label>Password</Form.Label>
                            <Form.Control
                                type='password'
                                placeholder='Password'
                                required
                            ></Form.Control>
                        </Form.Group>
                        <Button variant='primary' type='submit'>
                            Register
                        </Button>
                    </Stack>
                </Form>
            </Card.Body>
        </Card>
    );
};

export default Register;
