<template>
    <div class="select">
        <div class="select__wrapper">
            <h1 class="select__title">select topic</h1>
            <div class="select__grid-container">
                <div @click="selectTopic('BASH')" class="select__grid-item">
                    <img src="../assets/images/git-logo.jpg" />
                </div>
                <div @click="selectTopic('HTML')" class="select__grid-item">
                    <img src="../assets/images/HTML-logo.jpg" />
                </div>
                <div @click="selectTopic('BASH')" class="select__grid-item">
                    <img src="../assets/images/linux-logo.png" />
                </div>
                <div
                    @click="selectTopic('JavaScript')"
                    class="select__grid-item"
                >
                    <img src="../assets/images/js-logo.png" />
                </div>
                <div @click="selectTopic('Laravel')" class="select__grid-item">
                    <img src="../assets/images/laravel-logo.jpg" />
                </div>
                <div @click="selectTopic('Docker')" class="select__grid-item">
                    <img src="../assets/images/docker-logo.jpg" />
                </div>
                <div
                    @click="selectTopic('WordPress')"
                    class="select__grid-item"
                >
                    <img src="../assets/images/wordpress-logo.png" />
                </div>
                <div @click="selectTopic('MySQL')" class="select__grid-item">
                    <img src="../assets/images/mysql-logo.jpg" />
                </div>
                <div
                    @click="selectTopic('Kubernetes')"
                    class="select__grid-item"
                >
                    <img src="../assets/images/kubernetes-logo.png" />
                </div>
                <div @click="selectTopic('DevOps')" class="select__grid-item">
                    <img src="../assets/images/devops-logo.jpg" />
                </div>
                <div @click="selectTopic('PHP')" class="select__grid-item">
                    <img src="../assets/images/PHP-logo.jpg" />
                </div>
            </div>
        </div>
    </div>
    <GamePopup
        v-if="popupVisible"
        @difficulty-selected="handleSelectDifficulty"
    />
</template>

<script setup lang="ts">
import GamePopup from '@/components/GamePopup.vue';
import { ref } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();
const popupVisible = ref<boolean>(false);
const options = {
    topic: '',
    difficulty: '',
};

const popupOpen = () => (popupVisible.value = true);
const popupClose = () => (popupVisible.value = false);
const selectTopic = (topic: string) => {
    options.topic = topic;
    popupOpen();
};
const handleSelectDifficulty = (difficulty: string) => {
    options.difficulty = difficulty;
    router.push({name: 'Quiz', params: options})
    popupClose();
};
</script>

<style lang="scss" scoped>
.select__title {
    @include stroke(2px, #000);

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
    grid-template-rows: repeat(5, 264px);
    gap: 20px;
    margin-bottom: 50px;
}
.select__grid-item {
    background-color: $base-gray;
    min-height: 264px;
    border: 2px solid;

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
