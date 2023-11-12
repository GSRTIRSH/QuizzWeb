import type { 
    getUserResponse,
    signinArgs,
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

export const loginRequest = async (credentials: signinArgs) => {
    const result = await fetch(`${config.API_URL}/v2/Auth/login`, {
        method: 'POST',
        headers: {
            'x-api-key': config.TOKEN,
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(credentials)
    })
    const data = await result.json()
    return data
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
    const data = await result.json()
    return data
}

export const getUserInfoRequest = async (id: string, token: string): Promise<getUserResponse> => {
    const result = await fetch(
        `${config.API_URL}/v2/Auth/user?id=${id}`,
        {
            method: 'GET',
            headers: {
                'x-api-key': config.TOKEN,
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`
            }
        }
    )
    const data = await result.json()
    return data
}
