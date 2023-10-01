<template>
    <div class="popup">
        <h2 class="popup__title">Select difficulty</h2>
        <div class="popup__buttons-container">
            <button @click="selectDifficulty('easy')" class="popup__button">easy</button>
            <button @click="selectDifficulty('medium')" class="popup__button">medium</button>
            <button @click="selectDifficulty('hard')" class="popup__button">hard</button>
        </div>
    </div>
</template>

<script setup lang="ts">
import { defineEmits } from 'vue'

const emit = defineEmits()

const selectDifficulty = (difficulty: string) => {
    emit('difficulty-selected', difficulty)
}
</script>

<style lang="scss" scoped>
.popup {
    width: 310px;
    height: 275px;
    border: 2px solid #FA8D12;
    background: #4E5289;
    position: fixed;
    bottom: -2px;
    left: 50%;
    transform: translateX(-50%);
}
.popup__title {
    color: $base-orange;
    font-family: $base-font;
    font-size: 25px;
    font-style: normal;
    font-weight: 400;
    line-height: normal;
    letter-spacing: 1.675px;
    text-align: center;

    margin: 20px 0px 18px 0px;
}
.popup__buttons-container {
    margin: 0 auto;
    display: flex;
    flex-direction: column;
    max-width: 149px;
}
.popup__button {
    background: #322F32;
    padding: 8px 0px 10px 0px;
    font-family: $base-font;
    font-size: 27px;
    font-style: normal;
    font-weight: 400;
    line-height: normal;
    letter-spacing: 0.729px;

    &:nth-child(1) {
        border: 2px solid $base-yellow;
        color: $base-yellow;
    }
    &:nth-child(2) {
        border: 2px solid $base-orange;
        color: $base-orange;
    }
    &:nth-child(3) {
        border: 2px solid #FA3C12;
        color: #FA3C12;
    }
    &:not(:last-child) {
        margin-bottom: 10px;
    }
}
</style>