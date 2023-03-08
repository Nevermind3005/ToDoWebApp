import { useState } from 'react';
import { Badge, Card, Button } from 'react-bootstrap';
import { baseUrl, endpoints } from '../../api';
import { useFetch } from '../../useFetch';
import './ToDo.css';

interface IToDo {
    id: string;
    title: string;
    description: string;
    completed: boolean;
}

const Todo: React.FC<IToDo> = (toDoProp: IToDo) => {
    const [toDo, setToDo] = useState<IToDo>(toDoProp);

    const d = useFetch();

    const deleteToDo = () => {
        let a = window.confirm('Are you sure to remove this todo?');
        if (a === false) {
            return;
        }
        d.delete(baseUrl + endpoints.todo.deleteTodo + '/' + toDo.id, {});
    };

    return (
        <Card
            style={{
                width: 'calc(calc(100% / (3)) - 10px)',
                margin: '5px',
                minWidth: '250px',
            }}
        >
            <Card.Body>
                <Card.Title>{toDo.title}</Card.Title>
                <Card.Text>{toDo.description}</Card.Text>
                <div className='todo_bottom_badges'>
                    {!toDo.completed && (
                        <Badge pill bg='warning' text='dark'>
                            Uncompleted
                        </Badge>
                    )}
                    {toDo.completed && (
                        <Badge pill bg='success' text='dark'>
                            Completed
                        </Badge>
                    )}
                    <div className='todo_coltroll_badges'>
                        <Badge
                            pill
                            bg='primary'
                            text='dark'
                            className='todo_delete_button'
                        >
                            <i className='bi bi-pen'></i>Edit
                        </Badge>
                        <Badge
                            pill
                            bg='danger'
                            text='dark'
                            className='todo_delete_button'
                            onClick={deleteToDo}
                        >
                            <i className='bi bi-trash'></i>Delete
                        </Badge>
                    </div>
                </div>
            </Card.Body>
        </Card>
    );
};

export default Todo;
