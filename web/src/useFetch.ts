import { useState } from 'react';
import { getAccessToken, setAccessToken } from './AccessToken';
import { baseUrl, endpoints } from './api';

const useFetch = () => {
    const [isLoading, setIsLoading] = useState(true);

    const auth = () => {
        return 'Bearer ' + getAccessToken();
    };

    const createRequestOptions = (
        method: string,
        init: RequestInit | undefined
    ): RequestInit | undefined => {
        let request = init;
        if (request != null) {
            request.method = method;
            request.credentials = 'include';
            request.headers = new Headers(request.headers);
            request.headers.set('Authorization', auth());
        }
        return request;
    };

    const fetchData = async (
        url: string,
        method: string,
        init: RequestInit | undefined,
        retry: boolean = false
    ): Promise<Response> => {
        setIsLoading(true);
        let response = await fetch(url, createRequestOptions(method, init));
        if (response.status === 401 && retry) {
            let token = await fetchData(
                baseUrl + endpoints.token,
                'POST',
                init
            );
            setAccessToken(await token.text());
            response = await fetchData(url, method, init);
        } else if (response.status === 401) {
            setAccessToken('');
        }
        setIsLoading(false);
        return response;
    };

    const request = (method: string) => {
        return (url: string, init?: RequestInit | undefined) => {
            let response = fetchData(url, method, init, true);
            return response;
        };
    };

    return {
        get: request('GET'),
        post: request('POST'),
        put: request('PUT'),
        delete: request('DELETE'),
        getIsLoading: () => {
            return isLoading;
        },
    };
};

export { useFetch };
