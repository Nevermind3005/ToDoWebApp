import { Badge, Card } from 'react-bootstrap';

interface IToDo {
    title: string;
    description: string;
    completed: boolean;
}

const Todo: React.FC<IToDo> = ({ title, description, completed }) => {
    return (
        <Card style={{ width: '35rem' }}>
            <Card.Body>
                <Card.Title>{title}</Card.Title>
                <Card.Text>{description}</Card.Text>
                {!completed && (
                    <Badge pill bg='warning' text='dark'>
                        Uncompleted
                    </Badge>
                )}
                {completed && (
                    <Badge pill bg='success' text='dark'>
                        Completed
                    </Badge>
                )}
            </Card.Body>
        </Card>
    );
};

export default Todo;
