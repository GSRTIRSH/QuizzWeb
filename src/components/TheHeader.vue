<template>
    <header class="header">
        <div class="header__wrapper">
            <span class="header__logo">
                <span>CODE</span>
                <span>BUSTERS</span>
            </span>
            <div class="header__button-container">
                <button class="header__button btn-1" @click="$router.push({name: 'Auth'})">
                    LOG IN
                </button>
                <button class="header__button btn-2" @click="$router.push({name: 'Auth'})">
                    SIGN IN
                </button>
            </div>
        </div>
    </header>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
    setup() {
        return {};
    },
});
</script>

<style lang="scss" scoped>
.header {
    height: 70px;
    background-color: #4e5289;
}
.header__wrapper {
    height: 100%;
    display: flex;
    justify-content: space-between;
    align-items: center;
}
.header__logo {
    @include stroke(2px, #000);

    position: relative;
    bottom: 4px;
    font-family: $base-font;
    font-size: 40px;
    font-style: normal;
    font-weight: 400;
    line-height: normal;
    letter-spacing: 2.68px;

    span:first-child {
        color: $base-orange;
    }

    span:last-child {
        color: $base-yellow;
    }
}
.header__button-container {
    display: flex;
}
.header__button {
    font-family: $base-font;
    background-color: $base-gray;
    font-size: 20px;
    font-style: normal;
    font-weight: 400;
    line-height: normal;
    letter-spacing: 0.54px;

    width: 140px;
    padding: 2px 0px 6px 0px;
}
.btn-1 {
    margin-right: 25px;
    color: $base-orange;
    border: 2px solid $base-orange;
}
.btn-2 {
    color: $base-yellow;
    border: 2px solid $base-yellow;
}
</style>
