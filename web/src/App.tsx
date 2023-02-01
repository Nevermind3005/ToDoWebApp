import { createContext, useContext, useState } from 'react';
import { Outlet } from 'react-router-dom';
import { getAccessToken } from './AccessToken';
import { baseUrl, endpoints } from './api';
import { useFetch } from './useFetch';

interface ILoginContext {
    isLoggedIn: boolean;
    setIsLoggedIn: React.Dispatch<React.SetStateAction<boolean>>;
}

export const LoginContext = createContext<ILoginContext | null>(null);

function App() {
    const { get } = useFetch();

    const [isLoggedIn, setIsLoggedIn] = useState(false);

    return (
        <LoginContext.Provider value={{ isLoggedIn, setIsLoggedIn }}>
            <div className='App'>
                <Outlet />
                {!isLoggedIn ? (
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
        </LoginContext.Provider>
    );
}

export default App;
