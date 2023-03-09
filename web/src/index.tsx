import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import 'bootstrap/dist/css/bootstrap.min.css';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import Login from './Components/Login/Login';
import Register from './Components/Register/Register';
import LandingPage from './Components/LandingPage/LandingPage';
import Dashboard from './Components/Dashboard/Dashboard';
import TodoAdd from './Components/ToDoAdd/ToDoAdd';
import './index.css';
import ToDoEdit from './Components/ToDoEdit/ToDoEdit';

const router = createBrowserRouter([
    {
        path: '/',
        element: <App />,
        children: [
            {
                path: '/login',
                element: <Login />,
            },
            {
                path: '/register',
                element: <Register />,
            },
            {
                path: '/',
                element: <LandingPage />,
            },
            {
                path: '/dashboard',
                element: <Dashboard />,
            },
            {
                path: '/add',
                element: <TodoAdd />,
            },
            {
                path: '/edit/:toDoId',
                element: <ToDoEdit />,
            },
        ],
    },
]);

const root = ReactDOM.createRoot(
    document.getElementById('root') as HTMLElement
);
root.render(
    <React.StrictMode>
        <RouterProvider router={router} />
    </React.StrictMode>
);
