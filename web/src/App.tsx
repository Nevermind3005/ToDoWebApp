import { Outlet } from 'react-router-dom';
import { getAccessToken } from './AccessToken';
import { baseUrl, endpoints } from './api';
import { useFetch } from './useFetch';

function App() {
    const { get } = useFetch();
    return (
        <div className='App'>
            <Outlet />
            {true ? (
                <div>
                    <button onClick={() => alert(getAccessToken())}>
                        Token
                    </button>
                    <button
                        onClick={() => get(baseUrl + endpoints.user.me, {})}
                    >
                        Me
                    </button>
                </div>
            ) : null}
        </div>
    );
}

export default App;
