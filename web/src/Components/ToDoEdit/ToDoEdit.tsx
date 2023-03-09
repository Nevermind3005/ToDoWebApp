import { ChangeEvent, FormEvent, useContext, useEffect, useState } from 'react';
import { Card, Stack, Form, Button } from 'react-bootstrap';
import { useNavigate, useParams } from 'react-router-dom';
import { baseUrl, endpoints } from '../../api';
import { LoginContext } from '../../App';
import { useFetch } from '../../useFetch';

interface IToDo {
    id: string;
    name: string;
    description: string;
    createdAt: string;
    completedAt: string | null;
}

const ToDoEdit = () => {
    const [todo, setToDo] = useState<IToDo>({
        id: '',
        name: '',
        description: '',
        createdAt: '',
        completedAt: '',
    });

    const navigate = useNavigate();

    const { toDoId } = useParams();

    const { get, put } = useFetch();

    const loginContext = useContext(LoginContext);

    useEffect(() => {
        const fetchData = async () => {
            let response = await get(
                baseUrl + endpoints.todo.todo + '/' + toDoId,
                {}
            );
            if (response.status === 401) {
                loginContext?.setIsLoggedIn(false);
            } else {
                loginContext?.setIsLoggedIn(true);
            }
            let responseJson = await response.json();

            setToDo(responseJson);
        };
        fetchData();
    }, [toDoId]);

    useEffect(() => {
        console.log(toDoId);
    }, [toDoId]);

    const add = async () => {
        const response = await put(
            baseUrl + endpoints.todo.editTodo + '/' + todo.id,
            {
                headers: {
                    'content-type': 'application/json',
                },
                body: JSON.stringify(todo),
            }
        );

        return response;
    };

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        const response = await add();
        if (response.status == 204) {
            console.log('OK');
            navigate('/dashboard');
        } else {
            console.log('ERROR');
        }
    };

    const handleFormChange = (
        e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
    ): void => {
        setToDo({
            ...todo,
            [e.target.name]: e.target.value,
        });
    };

    const handleCompleted = (e: ChangeEvent<HTMLInputElement>): void => {
        if (e.target.checked) {
            var currentdate = new Date();
            setToDo({ ...todo, completedAt: currentdate.toISOString() });
        } else {
            setToDo({ ...todo, completedAt: null });
        }
    };

    return (
        <Card style={{ width: '35rem' }}>
            <Card.Body>
                <Card.Title className='text-center'>Edit ToDo</Card.Title>
                <Form onSubmit={(e) => handleSubmit(e)}>
                    <Stack gap={3}>
                        <Form.Group>
                            <Form.Label>Name</Form.Label>
                            <Form.Control
                                type='text'
                                placeholder='Name of your new todo'
                                name='name'
                                required
                                minLength={3}
                                maxLength={22}
                                onChange={(e) => handleFormChange(e)}
                                value={todo.name}
                            ></Form.Control>
                        </Form.Group>
                        <Form.Group>
                            <Form.Label>Description</Form.Label>
                            <Form.Control
                                type='text'
                                name='description'
                                placeholder='Description of your new todo.'
                                required
                                onChange={(e) => handleFormChange(e)}
                                value={todo.description}
                            ></Form.Control>
                        </Form.Group>
                        <Form.Group>
                            <Form.Check
                                name='completedAt'
                                label='Completed'
                                checked={todo.completedAt != null}
                                onChange={(e) => handleCompleted(e)}
                            />
                        </Form.Group>
                        <Button variant='primary' type='submit'>
                            Save
                        </Button>
                    </Stack>
                </Form>
            </Card.Body>
        </Card>
    );
};

export default ToDoEdit;
