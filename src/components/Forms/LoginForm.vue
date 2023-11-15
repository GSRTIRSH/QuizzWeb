<script setup lang="ts">
import { useForm, useField } from 'vee-validate'
import * as Yup from 'yup'
import { useAuthStore } from '@/store/authStore'
import { toRefs, watch } from 'vue'
import { useToast } from 'primevue/usetoast'
import { useAsyncState } from '@vueuse/core'

const { errors } = toRefs(useAuthStore())
const { login } = useAuthStore()
const toast = useToast()

interface LoginForm {
    name: string
    password: string
}

const emit = defineEmits()

const loginSchema: Yup.ObjectSchema<LoginForm> = Yup.object({
    name: Yup.string().required(),
    password: Yup.string().min(6).required()
})

const { handleSubmit } = useForm<LoginForm>({
    validationSchema: loginSchema
})
const { value: name, errors: loginErrors } = useField<LoginForm['name']>('name')
const { value: password, errors: passwordErrors } = useField<LoginForm['password']>('password')

const {
  isLoading: isLoginLoading,
  execute: loginExecute
} = useAsyncState((values) => login(values), null, { immediate: false })

const onSubmit = handleSubmit((values) => loginExecute(undefined, values))


watch(errors, () => {
    errors.value.forEach(error => {
        toast.add({ severity: 'error', summary: 'Error', detail: error, life: 5000 })
    });
})
</script>

<template>
    <form
        @submit="onSubmit"
        class="tw-flex tw-flex-col auth__input-container tw-self-center tw-w-full tw-max-w-sm"
    >
        <div class="tw-flex tw-flex-col tw-self-center tw-w-full tw-max-w-sm tw-gap-2">
            <InputText
                v-model="name"
                size="large"
                name="login"
                autocomplete="login"
                id="login"
                placeholder="Login"
                :class="{ 'p-invalid': loginErrors.length }"
            />
            <InputText
                v-model="password"
                size="large"
                type="password"
                name="password"
                id="authPassword"
                autocomplete="current-password"
                placeholder="Password"
                :class="{ 'p-invalid': passwordErrors.length }"
            />
            <Button 
                type="submit"
                class="tw-justify-center tw-text-2xl"
                :loading="isLoginLoading"
            >
                <span>log in</span>
            </Button>
        </div>
        <div class="tw-flex tw-items-center">
            <div class="tw-h-1 tw-w-full tw-bg-black" />
            <div
                class="tw-font-base-ui tw-text-xl tw-mx-3 tw-text-black tw-relative tw-bottom-[2px]"
            >
                or
            </div>
            <div class="tw-h-1 tw-w-full tw-bg-black" />
        </div>
        <Button
            @click="emit('switchLoginForm')"
            class="tw-justify-center tw-text-2xl tw-mb-3"
            outlined
        >
            <span>sign up</span>
        </Button>
        <Toast />
    </form>
</template>
