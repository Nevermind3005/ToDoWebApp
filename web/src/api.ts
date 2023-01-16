const baseUrl = 'http://localhost:5048';
const endpoints = {
    register: '/api/Auth/register',
    login: '/api/Auth/login',
    user: {
        me: '/api/User/me',
    },
};

export { baseUrl, endpoints };
