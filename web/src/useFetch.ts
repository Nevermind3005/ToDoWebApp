import { getAccessToken, setAccessToken } from './AccessToken';
import { baseUrl, endpoints } from './api';

const useFetch = () => {
    const auth = () => {
        return 'Bearer ' + getAccessToken();
    };

    const createRequestOptions = (method: string, body: string) => {
        return {
            method: method,
            headers: {
                Authorization: auth(),
                'content-type': 'application/json',
                body: body,
            },
        };
    };

    const fetchData = (
        url: string,
        method: string,
        body: string,
        retry: boolean = false
    ): Response | null => {
        fetch(url, createRequestOptions(method, body))
            .then(async (response) => {
                if (response.status === 401 && retry) {
                    await refreshToken();
                    return fetchData(url, method, body);
                }
                return response;
            })
            .catch((error) => console.error(error.message));
        return null;
    };

    const refreshToken = async () => {
        await fetch(baseUrl + endpoints.token, { method: 'post' })
            .then((response) => response.text())
            .then((data) => {
                setAccessToken(data);
            });
    };

    const request = (method: string) => {
        return (url: string, body: string = '') => {
            let response = fetchData(url, method, body, true);
            return response;
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
