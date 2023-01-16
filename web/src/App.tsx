import { Outlet } from 'react-router-dom';
import { getAccessToken } from './AccessToken';
import { baseUrl, endpoints } from './api';
import { useFetch } from './useFetch';

function App() {
    const fetch = useFetch();

    return (
        <div className='App'>
            <Outlet />
            <button onClick={() => alert(getAccessToken())}>Token</button>
            <button onClick={() => fetch.get(baseUrl + endpoints.user.me)}>
                Me
            </button>
        </div>
    );
}

export default App;
