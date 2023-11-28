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
import Cookies from 'js-cookie'


export const useAuthStore = defineStore('authStore', () => {
    const router = useRouter()
    const { getUserInfo, getAvatar } = useUserStore()
    const { avatar, userData } = toRefs(useUserStore())
    
    const errors = ref([''])
    const isAuth = ref(false)

    onBeforeMount(async() => {
        const token = Cookies.get('token')
        const id = Cookies.get('id')
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

            Cookies.set('token', successData.token, { expires: 7 })
            Cookies.set('id', successData.id, { expires: 7 })

            await getUserInfo(successData.id)
            router.push({name: 'Main'})

            await getAvatar()

        } else {
            const errorData: authResponseError = data as authResponseError;
            errors.value = errorData.errors
        }
    }

    const logout = () => {
        isAuth.value = false
        avatar.value = null

        Cookies.remove('token')
        Cookies.remove('id')
        
        router.push({name: 'Main'})
    }

    const signup = async (credentials: signupArgs) => {
        const data = await regRequest(credentials)
        if (data.result) {
            const successData: authResponseSuccess = data as authResponseSuccess;
            isAuth.value = successData.result

            Cookies.set('token', successData.token, { expires: 7 }) // где expires - срок жизни cookie в днях
            Cookies.set('id', successData.id, { expires: 7 })
            
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