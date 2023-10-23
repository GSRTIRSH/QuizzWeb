<script setup lang="ts">
import { useForm, useField } from 'vee-validate'
import { ref } from 'vue'
import { object, ObjectSchema, string } from 'yup'

interface LoginForm {
    login: string
    password: string
}

const isLoginFocused = ref(false)
const isPasswordFocused = ref(false)

const emit = defineEmits()

const loginSchema: ObjectSchema<LoginForm> = object({
    login: string().required(),
    password: string().min(6).required()
})

const { handleSubmit } = useForm<LoginForm>({
    validationSchema: loginSchema
})
const { value: login, errors: loginErrors } = useField<LoginForm['login']>('login')
const { value: password, errors: passwordErrors } = useField<LoginForm['password']>('password')

const onSubmit = handleSubmit((values) => {
    console.log(values, 'hello')
})
</script>

<template>
    <form
        @submit="onSubmit"
        class="tw-flex tw-flex-col auth__input-container tw-self-center tw-w-full tw-max-w-sm"
    >
        <div class="tw-flex tw-flex-col tw-self-center tw-w-full tw-max-w-sm tw-gap-2">
            <InputText
                v-model="login"
                size="large"
                name="email"
                autocomplete="login"
                id="login"
                placeholder="Email"
                @focusin="isLoginFocused = true"
                @focusout="isLoginFocused = false"
                :class="{ 'p-invalid': loginErrors.length }"
            />
            <InlineMessage v-if="isLoginFocused && loginErrors.length">
                <span v-for="(error, index) in loginErrors">
                    {{ error }}
                </span>
            </InlineMessage>
            <InputText
                v-model="password"
                size="large"
                type="password"
                name="password"
                id="authPassword"
                autocomplete="current-password"
                placeholder="Password"
                @focusin="isPasswordFocused = true"
                @focusout="isPasswordFocused = false"
                :class="{ 'p-invalid': passwordErrors.length }"
            />
            <InlineMessage v-if="isPasswordFocused && passwordErrors.length">
                <span v-for="(error, index) in passwordErrors">
                    <div>{{ error }}</div>
                </span>
            </InlineMessage>
            <Button class="tw-justify-center tw-text-2xl"><span>log in</span></Button>
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
    </form>
</template>

<style scoped></style>
