import React from 'react';
import { useState } from 'react';
import { Button, Card, Form, Stack } from 'react-bootstrap';
import { baseUrl, endpoints } from '../../api';

const Register = () => {
    const [userRegisterInfo, setUserRegisterInfo] = useState({
        username: '',
        email: '',
        password: '',
    });

    const register = async () => {
        const response = await fetch(`${baseUrl}${endpoints.register}`, {
            method: 'post',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(userRegisterInfo),
        });
        return response;
    };

    const handleFormChange = (e: any) => {
        setUserRegisterInfo({
            ...userRegisterInfo,
            [e.target.name]: e.target.value,
        });
    };

    const handleRegisterSubmit = async (
        e: React.FormEvent<HTMLFormElement>
    ) => {
        e.preventDefault();
        console.log(userRegisterInfo);
        const response = await register();
        if (response.status === 200) {
            setUserRegisterInfo({
                username: '',
                email: '',
                password: '',
            });
        }
    };

    return (
        <Card style={{ width: '35rem' }}>
            <Card.Body>
                <Card.Title className='text-center'>Register</Card.Title>
                <Form onSubmit={(e) => handleRegisterSubmit(e)}>
                    <Stack gap={3}>
                        <Form.Group>
                            <Form.Label>Username</Form.Label>
                            <Form.Control
                                type='text'
                                placeholder='Username'
                                name='username'
                                required
                                onChange={(e) => handleFormChange(e)}
                                value={userRegisterInfo.username}
                            ></Form.Control>
                        </Form.Group>
                        <Form.Group>
                            <Form.Label>Email</Form.Label>
                            <Form.Control
                                type='email'
                                name='email'
                                placeholder='Your email'
                                required
                                onChange={(e) => handleFormChange(e)}
                                value={userRegisterInfo.email}
                            ></Form.Control>
                        </Form.Group>
                        <Form.Group>
                            <Form.Label>Password</Form.Label>
                            <Form.Control
                                type='password'
                                name='password'
                                placeholder='Password'
                                required
                                onChange={(e) => handleFormChange(e)}
                                value={userRegisterInfo.password}
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
