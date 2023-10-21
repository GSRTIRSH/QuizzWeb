<template>
    <div class="tw-flex tw-items-center tw-max-w-6xl tw-mx-auto tw-w-full">
        <div class="tw-flex tw-justify-between tw-items-center tw-w-full">
            <img
                src="@/assets/images/auth-image.png"
                alt=""
                class="auth__image"
            />
            <form class="auth__input-container tw-self-center tw-w-full tw-max-w-sm tw-mx-auto">
                <InputText
                    class="tw-mb-3"
                    size="large"
                    name="email"
                    autocomplete="email"
                    id="login"
                    placeholder="Email"
                />
                <InputText
                    class="tw-mb-3"
                    size="large"
                    type="password"
                    name="password"
                    id="password"
                    autocomplete="current-password"
                    placeholder="Password"
                />
                <Button class="tw-justify-center tw-text-2xl"><span>sign up</span></Button>
                <div class="tw-flex tw-items-center">
                    <div class="tw-h-1 tw-w-full tw-bg-black"/>
                    <div class="tw-font-base-ui tw-text-xl tw-mx-3 tw-text-black tw-relative tw-bottom-[2px]">or</div>
                    <div class="tw-h-1 tw-w-full tw-bg-black"/>
                </div>
                <Button class="tw-justify-center tw-text-2xl" outlined>
                    <span>log in as a guest</span>
                </Button>
            </form>
        </div>
    </div>
</template>
<!-- <template>
    <div class="auth">
        <div class="auth__wrapper">
            <img
                src="@/assets/images/auth-image.png"
                alt=""
                class="auth__image"
            />
            <div class="auth__input-container">
                <input type="email" id="e1" class="auth__input" />
                <input type="email" id="e2" class="auth__input" />
                <button @click="getData" class="auth__button"><span>log in</span></button>
                <img
                    src="@/assets/images/auth-delimiter.svg"
                    class="auth__delimiter"
                />
                <button class="auth__button"><span>sign up</span></button>
                <button class="auth__button">
                    <span>log in as a guest</span>
                </button>
            </div>
        </div>
    </div>
</template> -->

<script setup lang="ts">
import { sendRegistrationDetails } from '@/api/sendRegistrationDetails';
import { ref } from 'vue';



// //TODO: refactor this shitcode
const getData = async () => {
    var e1 = (<HTMLInputElement>document.getElementById("e1")).value;
    var e2 = (<HTMLInputElement>document.getElementById("e2")).value;
    const data = await sendRegistrationDetails(e1, e2);
}

</script>

<style lang="scss" scoped>
.auth {
    display: flex;
    align-items: center;

}
.auth__wrapper {
    display: flex;
    justify-content: space-between;
    align-items: center;
    height: 500px;
    width: 100%;
}
.auth__image {
}
.auth__input-container {
    max-height: 409px;
    display: flex;
    flex-direction: column;
}
.auth__input {
    width: 305px;
    height: 64px;
    border: 5px solid #000;
    background: #e5e5e5;
    margin-bottom: 15px;
}
.auth__button {
    width: 305px;
    height: 64px;
    border: 2px solid $base-orange;
    background: $base-gray;

    color: $base-orange;
    font-family: $base-title-font;
    font-size: 27px;
    font-style: normal;
    font-weight: 400;
    line-height: normal;
    letter-spacing: 0.729px;

    span {
        position: relative;
        bottom: 3px;
    }

    &:nth-of-type(2) {
        margin-bottom: 15px;
    }
}
.auth__delimiter {
    width: 304px;
    margin-bottom: 4px;
}
</style>
