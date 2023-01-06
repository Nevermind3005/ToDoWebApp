import { Button, Card, Stack } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';

const LandingPage = () => {
    const navigate = useNavigate();

    return (
        <Card style={{ width: '35rem' }}>
            <Card.Body>
                <Stack gap={3}>
                    <Card.Title className='text-center'>Welcome</Card.Title>
                    <Button
                        variant='primary'
                        onClick={() => navigate('/login')}
                    >
                        Login
                    </Button>
                    <Button
                        variant='primary'
                        onClick={() => navigate('/register')}
                    >
                        Register
                    </Button>
                </Stack>
            </Card.Body>
        </Card>
    );
};
export default LandingPage;
