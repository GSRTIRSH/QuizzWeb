<script setup lang="ts">
import { useForm, useField } from 'vee-validate'
import { ref } from 'vue';
import { object, ObjectSchema, string } from 'yup'

interface RegistrationForm {
    login: string
    email: string
    password: string
    confPassword: string
}

const isLoginFocused = ref(false)
const isEmailFocused = ref(false)
const isPasswordFocused = ref(false)
const isConfPasswordFocused = ref(false)

const emit = defineEmits()

const registrationSchema: ObjectSchema<RegistrationForm> = object({
    login: string().required(),
    email: string().required(),
    password: string().min(6).required(),
    confPassword: string().required()
})

const { handleSubmit } = useForm<RegistrationForm>({ validationSchema: registrationSchema })
const { value: login, errors: loginErrors } = useField<RegistrationForm['login']>('login')
const { value: email, errors: emailErrors } = useField<RegistrationForm['email']>('email')
const { value: password, errors: passwordErrors } =
    useField<RegistrationForm['password']>('password')
const { value: confPassword, errors: confPasswordErrors } =
    useField<RegistrationForm['confPassword']>('confPassword');

const onSubmit = handleSubmit((values) => {
    console.log(values)
})
</script>

<template>
    <form
        @submit="onSubmit"
        class="tw-flex tw-flex-col tw-self-center tw-w-full tw-max-w-sm"
    >
        <div class="tw-flex tw-flex-col tw-self-center tw-w-full tw-max-w-sm tw-gap-2">
            <InputText
                v-model="login"
                size="large"
                name="login"
                id="login"
                placeholder="Login"
                autocomplete="login"
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
                v-model="email"
                size="large"
                name="email"
                id="email"
                placeholder="Email"
                autocomplete="email"
                @focusin="isEmailFocused = true"
                @focusout="isEmailFocused = false"
                :class="{ 'p-invalid': emailErrors.length }"
            />
            <InlineMessage v-if="isEmailFocused && emailErrors.length">
                <span v-for="(error, index) in emailErrors">
                    {{ error }}
                </span>
            </InlineMessage>
            <InputText
                v-model="password"
                size="large"
                type="password"
                name="password"
                id="password"
                autocomplete="password"
                placeholder="Password"
                @focusin="isPasswordFocused = true"
                @focusout="isPasswordFocused = false"
                :class="{ 'p-invalid': passwordErrors.length }"
            />
            <InlineMessage v-if="isPasswordFocused && passwordErrors.length">
                <span v-for="(error, index) in passwordErrors">
                    {{ error }}
                </span>
            </InlineMessage>
            <InputText
                v-model="confPassword"
                size="large"
                type="password"
                name="consfPassword"
                id="confPassword"
                autocomplete="password"
                placeholder="Confirm password"
                @focusin="isConfPasswordFocused = true"
                @focusout="isConfPasswordFocused = false"
                :class="{
                    'p-invalid': confPasswordErrors.length
                }"
            />
            <InlineMessage v-if="isConfPasswordFocused && confPasswordErrors.length">
                <span v-for="(error, index) in confPasswordErrors">
                    {{ error }}
                </span>
            </InlineMessage>
            <Button class="tw-justify-center tw-text-2xl"><span>continue</span></Button>
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
    </form>
</template>

<style lang="scss" scoped></style>
