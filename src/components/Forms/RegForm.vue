<script setup lang="ts">
import { useForm, useField } from 'vee-validate'
import * as Yup from 'yup'
import { useAuthStore } from '@/store/authStore'
import { toRefs, watch } from 'vue'
import { useToast } from 'primevue/usetoast'
import { useAsyncState } from '@vueuse/core'

const { signup } = useAuthStore()
const { errors } = toRefs(useAuthStore())
const toast = useToast()

interface RegistrationForm {
    name: string
    email: string
    password: string
    confPassword: string
}

const emit = defineEmits()

const registrationSchema: Yup.ObjectSchema<RegistrationForm> = Yup.object({
    name: Yup.string().required(),
    email: Yup.string().email().required(),
    password: Yup.string().min(6).required(),
    confPassword: Yup.string()
        .label('confirm password')
        .required()
        .oneOf([Yup.ref('password')], 'Passwords must match')
})

const { handleSubmit } = useForm<RegistrationForm>({ validationSchema: registrationSchema })
const { value: name, errors: loginErrors } = useField<RegistrationForm['name']>('name')
const { value: email, errors: emailErrors } = useField<RegistrationForm['email']>('email')
const { value: password, errors: passwordErrors } =
    useField<RegistrationForm['password']>('password')
const { value: confPassword, errors: confPasswordErrors } =
    useField<RegistrationForm['confPassword']>('confPassword')

const { isLoading: isSignupLoading, execute: signupExecute } = useAsyncState(
    (values) => signup(values),
    null,
    { immediate: false }
)

const onSubmit = handleSubmit((values) => 
    signupExecute(undefined, {
        name: values.name,
        email: values.email,
        password: values.password
    })
)

watch(errors, () => {
    errors.value.forEach((error) => {
        toast.add({ severity: 'error', summary: 'Error', detail: error, life: 5000 })
    })
})
</script>

<template>
    <form @submit="onSubmit" class="tw-flex tw-flex-col tw-self-center tw-w-full tw-max-w-sm">
        <div
            class="tw-flex tw-flex-col tw-self-center tw-w-full tw-max-w-sm tw-gap-2 tw-text-center"
        >
            <InputText
                v-model="name"
                size="large"
                name="login"
                id="login"
                placeholder="Login"
                autocomplete="login"
                :class="{ 'p-invalid': loginErrors.length }"
            />
            <InputText
                v-model="email"
                size="large"
                name="email"
                id="email"
                placeholder="Email"
                autocomplete="email"
                :class="{ 'p-invalid': emailErrors.length }"
            />

            <InputText
                v-model="password"
                size="large"
                type="password"
                name="password"
                id="password"
                autocomplete="password"
                placeholder="Password"
                :class="{ 'p-invalid': passwordErrors.length }"
            />

            <InputText
                v-model="confPassword"
                size="large"
                type="password"
                name="consfPassword"
                id="confPassword"
                autocomplete="password"
                placeholder="Confirm password"
                class="w-full"
                :class="{
                    'p-invalid': confPasswordErrors.length
                }"
            />

            <Button type="submit" class="tw-justify-center tw-text-2xl" :loading="isSignupLoading">
                <span>continue</span>
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
            @click="emit('switchRegForm')"
            class="tw-justify-center tw-text-2xl tw-mb-3"
            outlined
        >
            <span>log in</span>
        </Button>
        <Toast />
    </form>
</template>

<style lang="scss" scoped></style>
