import type { getUserResponse, } from '@/types/user'
import { config } from '@/config'

export const getUserInfoRequest = async (id: string, token: string): Promise<getUserResponse> => {
    const result = await fetch(
        `${config.API_URL}/v2/Auth/${id}`,
        {
            method: 'GET',
            headers: {
                'x-api-key': config.TOKEN,
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`
            }
        }
    )
    return await result.json()
}

export const uploadAvatarRequest = async (formData: any, userId: string, token: string) => {
    const result = await fetch(
        `${config.API_URL}/v2/User/${userId}/avatar`,
        {
            method: 'POST',
            headers: {
                'x-api-key': config.TOKEN,
                Authorization: `Bearer ${token}`
            },
            body: formData
        }
    )
    if (result.status === 201) {
        return true;
    } else {
        return false;
    }

}

export const getAvatarRequest = async (userId: string, token: string) => {
    const result = await fetch(
        `${config.API_URL}/v2/User/${userId}/avatar`,
        {
            method: 'GET',
            headers: {
                'x-api-key': config.TOKEN,
                Authorization: `Bearer ${token}`
            }
        }
    )
    return result
}