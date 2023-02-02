import { useContext, useEffect, useState } from 'react';
import { Stack } from 'react-bootstrap';
import { baseUrl, endpoints } from '../../api';
import { LoginContext } from '../../App';
import { useFetch } from '../../useFetch';
import Todo from '../ToDo/ToDo';

const Dashboard = () => {
    const [toDos, setToDos] = useState<any[]>([]);

    const { get } = useFetch();

    const loginContext = useContext(LoginContext);

    useEffect(() => {
        const fetchData = async () => {
            let response = await get(baseUrl + endpoints.todo.todos, {});
            if (response.status === 401) {
                loginContext?.setIsLoggedIn(false);
            } else {
                loginContext?.setIsLoggedIn(true);
            }
            let responseJson = await response.json();
            setToDos(responseJson);
        };
        fetchData();
    }, []);

    return (
        <Stack gap={3} direction='horizontal' className='m-5'>
            {toDos != null &&
                toDos.map((toDo) => (
                    <Todo
                        key={toDo.id}
                        title={toDo.name}
                        description={toDo.description}
                        completed={toDo.completedAt}
                    />
                ))}
        </Stack>
    );
};

export default Dashboard;
