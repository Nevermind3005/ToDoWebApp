import { getAccessToken } from './AccessToken';
import Login from './Components/Login/Login';
import Register from './Components/Register/Register';

function App() {
    return (
        <div className='App'>
            <Login />
            <Register />
            <button onClick={() => alert(getAccessToken())}>Token</button>
        </div>
    );
}

export default App;
