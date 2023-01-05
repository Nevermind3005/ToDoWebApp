import { Outlet } from 'react-router-dom';
import { getAccessToken } from './AccessToken';

function App() {
    return (
        <div className='App'>
            <Outlet />
            <button onClick={() => alert(getAccessToken())}>Token</button>
        </div>
    );
}

export default App;
