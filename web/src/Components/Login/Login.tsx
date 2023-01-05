import { useState } from 'react';
import { Button, Card, Form, Stack } from 'react-bootstrap';
import { setAccessToken } from '../../AccessToken';
import { baseUrl, endpoints } from '../../api';

const Login = () => {
    const [userLoginData, setUserLoginData] = useState({
        username: '',
        password: '',
    });

    const login = async () => {
        const response = await fetch(`${baseUrl}${endpoints.login}`, {
            method: 'post',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(userLoginData),
        });
        console.log(response);
        return response;
    };

    const handleFormChange = (e: any) => {
        setUserLoginData({ ...userLoginData, [e.target.name]: e.target.value });
    };

    const handleLoginSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        console.log(userLoginData);
        const response = await login();
        setAccessToken(await response.text());
        if (response.status === 200) {
            setUserLoginData({
                username: '',
                password: '',
            });
        }
    };

    return (
        <Card style={{ width: '35rem' }}>
            <Card.Body>
                <Card.Title className='text-center'>Login</Card.Title>
                <Form onSubmit={(e) => handleLoginSubmit(e)}>
                    <Stack gap={3}>
                        <Form.Group>
                            <Form.Label>Username</Form.Label>
                            <Form.Control
                                type='text'
                                placeholder='Username'
                                required
                                name='username'
                                value={userLoginData.username}
                                onChange={(e) => handleFormChange(e)}
                            ></Form.Control>
                        </Form.Group>
                        <Form.Group>
                            <Form.Label>Password</Form.Label>
                            <Form.Control
                                type='password'
                                placeholder='Password'
                                required
                                name='password'
                                value={userLoginData.password}
                                onChange={(e) => handleFormChange(e)}
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
