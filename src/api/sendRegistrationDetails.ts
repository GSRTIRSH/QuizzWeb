import { config } from '@/config'

export const sendRegistrationDetails = async() => {
    const response = await fetch(`${config.BASE_URL}/test`)
    const data = await response.json()
    return data
}