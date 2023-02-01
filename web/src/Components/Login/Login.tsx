import { useContext, useState } from 'react';
import { Button, Card, Form, Stack } from 'react-bootstrap';
import { setAccessToken } from '../../AccessToken';
import { baseUrl, endpoints } from '../../api';
import { LoginContext } from '../../App';
import { useFetch } from '../../useFetch';

const Login = () => {
    const [userLoginData, setUserLoginData] = useState({
        username: '',
        password: '',
    });

    const loginContext = useContext(LoginContext);

    const { post } = useFetch();

    const login = async () => {
        const response = await post(baseUrl + endpoints.login, {
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(userLoginData),
        });
        return response;
    };

    const handleFormChange = (e: any) => {
        setUserLoginData({ ...userLoginData, [e.target.name]: e.target.value });
    };

    const handleLoginSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        const response = await login();
        let text = await response.text();
        console.log(text);
        setAccessToken(text);
        if (response.status === 200) {
            setUserLoginData({
                username: '',
                password: '',
            });
            loginContext?.setIsLoggedIn(true);
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
                                minLength={3}
                                maxLength={22}
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
                                minLength={8}
                                maxLength={64}
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
