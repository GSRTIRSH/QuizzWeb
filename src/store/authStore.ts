import type { 
    authResponse, 
    authResponseError, 
    authResponseSuccess,
    getUserResponse,
    signinArgs,
    signupArgs
} from '@/types/auth'
import { defineStore } from 'pinia'
import { ref, onBeforeMount } from 'vue'
import { 
    checkTokenValidityRequest,
    loginRequest, 
    regRequest, 
    getUserInfoRequest
} from '@/api/authRequests'

export const useAuthStore = defineStore('authStore', () => {
    const errors = ref([''])
    const isAuth = ref(false)
    const userData = ref<getUserResponse>()

    onBeforeMount(() => {
        const token = localStorage.getItem('token')
        const id = localStorage.getItem('id')
        if (token && id) {
            checkTokenValidityRequest(token).then((isTokenValidity) => isAuth.value = isTokenValidity)
            getUserInfo(id, token)
        }
      });

    const signin = async (credentials: signinArgs) => {
        const data: authResponse = await loginRequest(credentials)
        if (data.result) {
            const successData: authResponseSuccess = data as authResponseSuccess
            isAuth.value = successData.result

            localStorage.setItem('token', successData.token)
            localStorage.setItem('id', successData.id)

            getUserInfo(successData.id, successData.token)
        } else {
            const errorData: authResponseError = data as authResponseError;
            errors.value = errorData.errors
        }
        
    }

    const signup = async (credentials: signupArgs) => {
        const data = await regRequest(credentials)
        if (data.result) {
            const successData: authResponseSuccess = data as authResponseSuccess;
            isAuth.value = successData.result

            localStorage.setItem('token', successData.token)
            localStorage.setItem('id', successData.id)

            getUserInfo(successData.id, successData.token)
        } else {
            const errorData: authResponseError = data as authResponseError;
            errors.value = errorData.errors
        }
    }
    const getUserInfo = async (id: string, token: string) => {
        const data = await getUserInfoRequest(id, token)
        userData.value = data
        
    }

    return { 
        signin, 
        signup, 
        getUserInfo, 
        errors, 
        isAuth,
        userData
    }
})
