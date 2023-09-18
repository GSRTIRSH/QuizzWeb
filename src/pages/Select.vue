<template>
    <div class="select">
        <div class="select__wrapper">
            <h1 class="select__title">select topic</h1>
            <div class="select__grid-container">
                <div class="select__grid-item"><img src="../assets/images/linux-logo.png"></div>
                <div class="select__grid-item"><img src="../assets/images/linux-logo.png"></div>
                <div class="select__grid-item"><img src="../assets/images/linux-logo.png"></div>
                <div class="select__grid-item"><img src="../assets/images/linux-logo.png"></div>
                <div class="select__grid-item"><img src="../assets/images/linux-logo.png"></div>
                <div class="select__grid-item"><img src="../assets/images/linux-logo.png"></div>
                <div class="select__grid-item"><img src="../assets/images/linux-logo.png"></div>
                <div class="select__grid-item"><img src="../assets/images/linux-logo.png"></div>
                <div class="select__grid-item"><img src="../assets/images/linux-logo.png"></div>
                <div class="select__grid-item"><img src="../assets/images/linux-logo.png"></div>
                <div class="select__grid-item"><img src="../assets/images/linux-logo.png"></div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
    setup () {
        

        return {}
    }
})
</script>

<style lang="scss" scoped>
.select__title {
    @include stroke (2px, #000);

    color: $base-yellow;
    font-family: $base-font;
    font-size: 50px;
    font-style: normal;
    font-weight: 400;
    line-height: normal;
    letter-spacing: 1.35px;
    text-align: center;
    margin: 55px 0px 30px 0px;
}
.select__grid-container {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    grid-template-rows: repeat(5, 1fr);
    gap: 20px;
    margin-bottom: 50px;
}
.select__grid-item {
    background-color: $base-gray;
    min-height: 264px;

    img {
        height: 100%;
        width: 100%;
        object-fit: cover;
    }

    &:nth-child(1) {
        grid-row: 1/3;
    }
    &:nth-child(2) {
        grid-column: 2/4;
    }
    &:nth-child(6) {
        grid-column: 2/4;
    }
    &:nth-child(7) {
        grid-column: 1/3;
    }
}
</style>