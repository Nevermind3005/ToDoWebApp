import { useState } from 'react';
import { Badge, Card } from 'react-bootstrap';

interface IToDo {
    title: string;
    description: string;
    completed: boolean;
}

const Todo: React.FC<IToDo> = (toDoProp: IToDo) => {
    const [toDo, setToDo] = useState<IToDo>(toDoProp);

    return (
        <Card style={{ width: 'calc(calc(100% / (3)) - 10px)', margin: '5px' }}>
            <Card.Body>
                <Card.Title>{toDo.title}</Card.Title>
                <Card.Text>{toDo.description}</Card.Text>
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
            </Card.Body>
        </Card>
    );
};

export default Todo;
