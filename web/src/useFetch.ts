import { getAccessToken } from './AccessToken';

const useFetch = () => {
    const auth = () => {
        return 'Bearer ' + getAccessToken();
    };

    const request = (method: string) => {
        return (url: string, body?: string) => {
            let headers: any = {
                Authorization: auth(),
            };
            const reqOptions: RequestInit = {
                method: method,
                headers,
            };
            if (body) {
                headers['Content-Type'] = 'application/json';
                reqOptions.body = body;
            }
            return fetch(url, reqOptions);
        };
    };

    return {
        get: request('GET'),
        post: request('POST'),
        put: request('PUT'),
        delete: request('DELETE'),
    };
};

export { useFetch };
