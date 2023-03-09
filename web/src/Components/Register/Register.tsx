import React, { ChangeEvent } from 'react';
import { useState } from 'react';
import { Button, Card, Form, Stack } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { baseUrl, endpoints } from '../../api';
import { useFetch } from '../../useFetch';

const Register = () => {
    const [userRegisterData, setUserRegisterData] = useState({
        username: '',
        email: '',
        password: '',
    });

    const navigate = useNavigate();
    const { post } = useFetch();

    const register = async () => {
        const response = await post(baseUrl + endpoints.register, {
            headers: {
                'content-type': 'application/json',
            },
            body: JSON.stringify(userRegisterData),
        });

        return response;
    };

    const handleFormChange = (
        e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
    ): void => {
        setUserRegisterData({
            ...userRegisterData,
            [e.target.name]: e.target.value,
        });
    };

    const handleRegisterSubmit = async (
        e: React.FormEvent<HTMLFormElement>
    ) => {
        e.preventDefault();
        const response = await register();
        if (response.status === 200) {
            navigate('/login');
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
                                minLength={3}
                                maxLength={22}
                                onChange={(e) => handleFormChange(e)}
                                value={userRegisterData.username}
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
                                value={userRegisterData.email}
                            ></Form.Control>
                        </Form.Group>
                        <Form.Group>
                            <Form.Label>Password</Form.Label>
                            <Form.Control
                                type='password'
                                name='password'
                                placeholder='Password'
                                required
                                minLength={8}
                                maxLength={64}
                                onChange={(e) => handleFormChange(e)}
                                value={userRegisterData.password}
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
