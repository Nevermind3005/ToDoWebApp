const baseUrl = 'http://localhost:5048';
const endpoints = {
    register: '/api/Auth/register',
    login: '/api/Auth/login',
    token: '/api/Auth/token',
    user: {
        me: '/api/User/me',
    },
    todo: {
        todos: '/api/ToDo',
        addTodo: '/api/ToDo',
    },
};

export { baseUrl, endpoints };
