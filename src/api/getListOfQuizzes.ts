import { config } from '@/config'

export const getListOfQuizzes = async(tag: string | string[], difficulty: string | string[]) => {
  const response = await fetch(`${config.BASE_URL}?limit=${10}&tags=${tag}&difficulty=${difficulty}&apiKey=${config.MY_CLIENT_ID}`)
  const data = await response.json()
  return data
}