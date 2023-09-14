<template>
    <header class="header">
        <div class="header__wrapper">
            <span class="header__logo">
                <span>CODE</span>
                <span>BUSTERS</span>
            </span>
            <div class="header__button-container">
                <button class="header__button">LOG IN</button>
                <button class="header__button">SIGN IN</button>
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
}
.header__wrapper {
    height: 100%;
    display: flex;
    justify-content: space-between;
    align-items: center;
}
.header__logo {
    @include stroke(2px, #000000);

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
    color: #FAC710;
    font-family: Visitor_Rus;
    font-size: 20px;
    font-style: normal;
    font-weight: 400;
    line-height: normal;
    letter-spacing: 0.54px;
    
    width: 140px;
    padding: 2px 0px 6px 0px;
    background-color: $base-gray;

    &:first-child {
        margin-right: 25px;
        color: $base-orange;
        border: 2px solid $base-orange;
    }
    &:last-child {
        color: $base-yellow;
        border: 2px solid $base-yellow;
    }
}
</style>
