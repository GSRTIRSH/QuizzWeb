import { config } from '@/config'

//TODO: refactor all this shit!
const username: string = 'example_username';
const password: string = 'example_password';

const credentials: object =
{
    username: username,
    password: password
};

export const sendRegistrationDetails = async (label1: string, label2: string): Promise<boolean> => {

    //get 
    const aw = await fetch(`${config.YC_API_URL}/shit`);
    const dt: string = await aw.text();
    console.warn(dt);

    console.warn(label1);
    console.warn(label2);

    let isLogin = false;

    const requestOptions =
    {
        method: 'POST',
        headers: {

            "Content-Type": "application/json",
        },
        body: JSON.stringify(credentials),
    };

    //http post
    const response: void = await fetch(`${config.YC_API_URL}/shit`, requestOptions)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            console.log('API response:', data);
            isLogin = data.isAuth;
        })
        .catch(error => {
            console.error('There was an error:', error);
        });
    
    // Сохранение данных в Session Storage
    sessionStorage.setItem('isLogin', isLogin.toString());

    // Получение данных из Session Storage
    const value = sessionStorage.getItem('isLogin');
    console.log(value);
    
    return isLogin;
}