import { Button, Card, Form, Stack } from 'react-bootstrap';

const Login = () => {
    return (
        <Card style={{ width: '35rem' }}>
            <Card.Body>
                <Card.Title className='text-center'>Login</Card.Title>
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
                            <Form.Label>Password</Form.Label>
                            <Form.Control
                                type='password'
                                placeholder='Password'
                                required
                            ></Form.Control>
                        </Form.Group>
                        <Button variant='primary' type='submit'>
                            Login
                        </Button>
                    </Stack>
                </Form>
            </Card.Body>
        </Card>
    );
};

export default Login;
