import type { 
    loginArgs,
    signupArgs 
} from '@/types/auth'
import { config } from '@/config'

export const checkTokenValidityRequest = async (token: string): Promise<boolean> => {
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
}

export const loginRequest = async (credentials: loginArgs) => {
    const result = await fetch(`${config.API_URL}/v2/Auth/login`, {
        method: 'POST',
        headers: {
            'x-api-key': config.TOKEN,
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(credentials)
    })
    return await result.json()
}

export const regRequest = async (credentials: signupArgs) => {
    const result = await fetch(`${config.API_URL}/v2/Auth/Register`, {
        method: 'POST',
        headers: {
            'x-api-key': config.TOKEN,
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(credentials)
    })
    return await result.json()
}