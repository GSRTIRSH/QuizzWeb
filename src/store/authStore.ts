import type { 
    authResponse, 
    authResponseError, 
    authResponseSuccess,
    loginArgs,
    signupArgs
} from '@/types/auth'
import { checkTokenValidityRequest, loginRequest, regRequest } from '@/api/authRequests'
import { defineStore } from 'pinia'
import { ref, onBeforeMount, toRefs } from 'vue'
import { useUserStore } from '../store/userStore'
import { useRouter } from 'vue-router'


export const useAuthStore = defineStore('authStore', () => {
    const router = useRouter()
    const { getUserInfo, getAvatar } = useUserStore()
    const { avatar, userData } = toRefs(useUserStore())
    
    const errors = ref([''])
    const isAuth = ref(false)

    onBeforeMount(async() => {
        const token = localStorage.getItem('token')
        const id = localStorage.getItem('id')
        if (token && id) {
            checkTokenValidityRequest(token).then((isTokenValidity) => isAuth.value = isTokenValidity)

            await getUserInfo(id)
            await getAvatar()
        }
      });

    const login = async (credentials: loginArgs) => {
        const data: authResponse = await loginRequest(credentials)
        if (data.result) {
            const successData: authResponseSuccess = data as authResponseSuccess
            isAuth.value = successData.result

            localStorage.setItem('token', successData.token)
            localStorage.setItem('id', successData.id)

            await getUserInfo(successData.id)
            await getAvatar()

            router.push({name: 'Main'})
        } else {
            const errorData: authResponseError = data as authResponseError;
            errors.value = errorData.errors
        }
    }

    const logout = () => {
        isAuth.value = false
        avatar.value = null

        localStorage.removeItem('token')
        localStorage.removeItem('id')
        
        router.push({name: 'Main'})
    }

    const signup = async (credentials: signupArgs) => {
        const data = await regRequest(credentials)
        if (data.result) {
            const successData: authResponseSuccess = data as authResponseSuccess;
            isAuth.value = successData.result

            localStorage.setItem('token', successData.token)
            localStorage.setItem('id', successData.id)
            
            getUserInfo(successData.id)

            router.push({name: 'Main'})
        } else {
            const errorData: authResponseError = data as authResponseError;
            errors.value = errorData.errors
        }
    }

    return { 
        signup, 
        login,
        logout, 
        errors, 
        isAuth
    }
})
