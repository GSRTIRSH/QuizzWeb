import type { getUserResponse } from '@/types/user'
import { config } from '@/config'

export const getUserInfoRequest = async (id: string): Promise<getUserResponse> => {
    try {
        const result = await fetch(
            `${config.BASE_URL}/api/User/${id}/get`,
            {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                },
                credentials: 'include'
            }
        );
        return await result.json()
    } catch (error) {
        console.error('Error in getUserInfoRequest:', error);
        throw error;
    }
}

export const uploadAvatarRequest = async (formData: any, userId: string, token: string) => {
    try {
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
        );

        if (result.status === 201) {
            return true;
        } else {
            return false;
        }
    } catch (error) {
        console.error('Error in uploadAvatarRequest:', error);
        throw error;
    }
}

export const getAvatarRequest = async (userId: string, token: string) => {
    try {
        const result = await fetch(
            `${config.API_URL}/v2/User/${userId}/avatar`,
            {
                method: 'GET',
                headers: {
                    'x-api-key': config.TOKEN,
                    Authorization: `Bearer ${token}`
                }
            }
        );
        return result;
    } catch (error) {
        console.error('Error in getAvatarRequest:', error);
        throw error;
    }
}
