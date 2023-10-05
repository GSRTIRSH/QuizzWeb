<template>
    <div class="select">
        <div class="select__wrapper">
            <div class="select__title">Randomized Developer Quiz</div>
            <div class="select__cards-container">
                <QuizCard 
                    v-for="(card, index) in baseCards" :key="index" :card="card"
                />
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import QuizCard from '@/components/QuizCard.vue';

const baseCards = [
    {
        img: '/src/assets/images/JavaScriptLogo.png',
        title: 'JavaScript',
        description: 'JavaScript Test: unleashing the power of obscure syntax and random bugs'
    },
    {
        img: '/src/assets/images/HtmlLogo.png',
        title: 'HTML5',
        description: 'JavaScript Test: unleashing the power of obscure syntax and random bugs'
    },
    {
        img: '/src/assets/images/CssLogo.png',
        title: 'CSS3',
        description: 'JavaScript Test: unleashing the power of obscure syntax and random bugs'
    },
    {
        img: '/src/assets/images/DevOpsLogo.png',
        title: 'DevOps',
        description: 'JavaScript Test: unleashing the power of obscure syntax and random bugs'
    },
    {
        img: '/src/assets/images/DockerLogo.png',
        title: 'Docker',
        description: 'JavaScript Test: unleashing the power of obscure syntax and random bugs'
    },
    {
        img: '/src/assets/images/LaravelLogo.png',
        title: 'Laravel',
        description: 'JavaScript Test: unleashing the power of obscure syntax and random bugs'
    },
    {
        img: '/src/assets/images/SqlLogo.png',
        title: 'SQL',
        description: 'JavaScript Test: unleashing the power of obscure syntax and random bugs'
    },
    {
        img: '/src/assets/images/KubernetesLogo.png',
        title: 'Kubernetes',
        description: 'JavaScript Test: unleashing the power of obscure syntax and random bugs'
    },
    {
        img: '/src/assets/images/LinuxLogo.png',
        title: 'Linux',
        description: 'JavaScript Test: unleashing the power of obscure syntax and random bugs'
    },
]

</script>

<style lang="scss" scoped>
.select {
    .select__wrapper {
        .select__title {
            @include stroke(2px, #000);

            text-align: center;
            color: $base-yellow;
            font-family: $base-text-font;
            font-size: 50px;
            font-style: normal;
            font-weight: bold;
            line-height: normal;
            letter-spacing: 1.728px;

            margin: 50px 0px 20px 0px;
        }
        .select__cards-container {
            display: grid;
            grid-template-columns: repeat(3, 1fr);
            column-gap: 40px;
            row-gap: 30px;
            margin-bottom: 40px;
        }
    }
}
</style>
