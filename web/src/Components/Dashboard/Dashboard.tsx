import { Fragment, useContext, useEffect, useState } from 'react';
import { Stack } from 'react-bootstrap';
import { Link, Route, Routes } from 'react-router-dom';
import { baseUrl, endpoints } from '../../api';
import { LoginContext } from '../../App';
import { useFetch } from '../../useFetch';
import Todo from '../ToDo/ToDo';
import TodoAdd from '../ToDoAdd/ToDoAdd';
import './Dashboard.css';

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
        <Fragment>
            <div className='dashboard_main_container'>
                <div className='dashboard_container'>
                    {toDos != null &&
                        toDos.map((toDo) => (
                            <Todo
                                key={toDo.id}
                                id={toDo.id}
                                title={toDo.name}
                                description={toDo.description}
                                completed={toDo.completedAt}
                            />
                        ))}
                </div>
            </div>
            <Link to={'../add'} className={'dashboard_add_button'}>
                +
            </Link>
        </Fragment>
    );
};

export default Dashboard;
