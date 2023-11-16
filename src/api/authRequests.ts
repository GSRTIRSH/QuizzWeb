import type { 
    loginArgs,
    signupArgs 
} from '@/types/auth'
import { config } from '@/config'

export const checkTokenValidityRequest = async (token: string): Promise<boolean> => {
    try {
        const response = await fetch(`${config.API_URL}/v2/Auth/token`, {
            method: 'GET',
            headers: {
                'x-api-key': config.TOKEN,
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`
            }
        });

        if (response.status === 200) {
            return true;
        } else {
            return false;
        }
    } catch (error) {
        console.error('Error in checkTokenValidityRequest:', error);
        throw error;
    }
}

export const loginRequest = async (credentials: loginArgs) => {
    try {
        const result = await fetch(`${config.API_URL}/v2/Auth/login`, {
            method: 'POST',
            headers: {
                'x-api-key': config.TOKEN,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(credentials)
        });
        return await result.json();
    } catch (error) {
        console.error('Error in loginRequest:', error);
        throw error;
    }
}

export const regRequest = async (credentials: signupArgs) => {
    try {
        const result = await fetch(`${config.API_URL}/v2/Auth/Register`, {
            method: 'POST',
            headers: {
                'x-api-key': config.TOKEN,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(credentials)
        });
        return await result.json();
    } catch (error) {
        console.error('Error in regRequest:', error);
        throw error;
    }
}
