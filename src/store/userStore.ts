import type { getUserResponse } from '@/types/user'
import { getAvatarRequest, getUserInfoRequest, uploadAvatarRequest } from '@/api/userRequests'
import { defineStore } from 'pinia'
import { ref } from 'vue'
import { FileUploadUploaderEvent } from 'primevue/fileupload'

export const useUserStore = defineStore('userStore', () => {
    const userData = ref<getUserResponse>()
    const isAvatarUploadet = ref<boolean>()
    const avatar = ref<any>()

    const getUserInfo = async (id: string) => {
        const token = localStorage.getItem('token')

        const data = await getUserInfoRequest(id, token!)
        userData.value = data
    }

    const uploadAvatar = async (event: FileUploadUploaderEvent) => {
        const token = localStorage.getItem('token')

        const files = event.files
        const file = (Array.isArray(files) ? files[0] : files);
        const formData = new FormData();
        formData.append('file', file);

        isAvatarUploadet.value = await uploadAvatarRequest(formData, userData.value?.id!, token!)
        if(isAvatarUploadet.value) {
            await getAvatar()
        }
    }

    const getAvatar = async () => {
        const token = localStorage.getItem('token')
        const response = await getAvatarRequest(userData.value?.id!, token!)
        const imageUrl = await response.blob()
        avatar.value = URL.createObjectURL(imageUrl)
        console.log(avatar.value)
    }

    return { 
        getUserInfo,
        uploadAvatar,
        getAvatar,
        avatar,
        isAvatarUploadet,
        userData,
    }
})
