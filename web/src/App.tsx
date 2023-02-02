import { createContext, useEffect, useState } from 'react';
import { Outlet } from 'react-router-dom';
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

    useEffect(() => {
        const fetchData = async () => {
            let response = await get(baseUrl + endpoints.user.me, {});
            if (response.ok) {
                setIsLoggedIn(true);
            } else {
                setIsLoggedIn(false);
            }
        };
        fetchData();
    }, []);

    return (
        <LoginContext.Provider value={{ isLoggedIn, setIsLoggedIn }}>
            <div className='App'>
                <Outlet />
            </div>
        </LoginContext.Provider>
    );
}

export default App;
