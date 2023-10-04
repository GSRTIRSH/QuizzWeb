<template>
    <div class="auth">
        {{ result }}
        <div class="auth__wrapper">
            <img
                src="@/assets/images/auth-image.png"
                alt=""
                class="auth__image"
            />
            <div class="auth__input-container">
                <input type="email" class="auth__input" />
                <input type="email" class="auth__input" />
                <button class="auth__button"><span>log in</span></button>
                <img
                    src="@/assets/images/auth-delimiter.svg"
                    class="auth__delimiter"
                />
                <button @click="getData" class="auth__button"><span>sign up</span></button>
                <button class="auth__button">
                    <span>log in as a guest</span>
                </button>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { sendRegistrationDetails } from '@/api/sendRegistrationDetails';
import { ref } from 'vue';


const result = ref('')

const getData = async () => {
    console.error("fuckyou")
    const data = await sendRegistrationDetails()
    result.value = data
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
    font-family: $base-font;
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
