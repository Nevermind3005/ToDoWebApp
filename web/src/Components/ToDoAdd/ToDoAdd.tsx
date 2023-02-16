import { useState } from 'react';
import { Card, Stack, Form, Button } from 'react-bootstrap';
import { baseUrl, endpoints } from '../../api';
import { useFetch } from '../../useFetch';

const TodoAdd = () => {
    const [newTodo, setNewTodo] = useState({
        name: '',
        description: '',
    });

    const { post } = useFetch();

    const handleFormChange = (e: any) => {
        setNewTodo({
            ...newTodo,
            [e.target.name]: e.target.value,
        });
    };

    const add = async () => {
        const response = await post(baseUrl + endpoints.todo.addTodo, {
            headers: {
                'content-type': 'application/json',
            },
            body: JSON.stringify(newTodo),
        });

        return response;
    };

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        const response = await add();
        if (response.status == 200) {
            console.log('OK');
        } else {
            console.log('ERROR');
        }
    };

    return (
        <Card style={{ width: '35rem' }}>
            <Card.Body>
                <Card.Title className='text-center'>Add new ToDO</Card.Title>
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
                                value={newTodo.name}
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
                                value={newTodo.description}
                            ></Form.Control>
                        </Form.Group>
                        <Button variant='primary' type='submit'>
                            Add
                        </Button>
                    </Stack>
                </Form>
            </Card.Body>
        </Card>
    );
};

export default TodoAdd;
