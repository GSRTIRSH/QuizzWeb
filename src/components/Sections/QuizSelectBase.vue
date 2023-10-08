<template>
    <div class="select ">
        <div class="select__wrapper">
            <div id="scrollPoint" class="select__cards-container">
                <QuizCard 
                    v-for="(card, index) in baseCards" 
                    :key="index" 
                    :card="card"
                    @cardData="handleCardData"
                />
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import QuizCard from '@/components/QuizCard.vue';
import { useRouter } from 'vue-router';

const router = useRouter();

const handleCardData = (data: string[]) => {
    router.push({
        name: 'Quiz', 
        params: {topic: data[0], difficulty: data[1]}
    });
}

const baseCards = [
    {
        img: '/src/assets/images/JavaScriptLogo.png',
        title: 'JavaScript',
        description: 'JavaScript Test: unleashing the power of obscure syntax and random bugs',
        topic: 'JavaScript'
    },
    {
        img: '/src/assets/images/HtmlLogo.png',
        title: 'HTML5',
        description: 'HTML Test: Can you spot the missing closing tag? Good luck finding Waldo!',
        topic: 'HTML'
    },
    {
        img: '/src/assets/images/PHPLogo.png',
        title: 'PHP',
        description: 'CSS Test: Think its just about colors and fonts? Time to dive into the Bermuda Triangle of Cascading Style Sheets!',
        topic: 'PHP'
    },
    {
        img: '/src/assets/images/DevOpsLogo.png',
        title: 'DevOps',
        description: 'DevOps Test: Are you a master of juggling flaming chainsaws while balancing on a tightrope? This quiz is childs play for you!',
        topic: 'DevOps'
    },
    {
        img: '/src/assets/images/DockerLogo.png',
        title: 'Docker',
        description: 'Docker Test: Can you keep up with the pace of containers appearing and disappearing faster than Houdinis vanishing act?',
        topic: 'Docker'
    },
    {
        img: '/src/assets/images/LaravelLogo.png',
        title: 'Laravel',
        description: 'Laravel Test: Do you have the patience of a Zen master to untangle the labyrinth of Laravels elegant yet mysterious code?',
        topic: 'Laravel'
    },
    {
        img: '/src/assets/images/SqlLogo.png',
        title: 'MySQL',
        description: 'SQL Test: Ready to navigate through the twisted maze of database queries with just your wits and a handful of wildcards?',
        topic: 'MySQL'
    },
    {
        img: '/src/assets/images/KubernetesLogo.png',
        title: 'Kubernetes',
        description: 'Kubernetes Test: Think you can orchestrate a cluster of virtual minions like a puppet master? Time to prove your Kubernetes command!',
        topic: 'Kubernetes'
    },
    {
        img: '/src/assets/images/LinuxLogo.png',
        title: 'Linux',
        description: 'Linux Test: Are you a Linux wizard who can recite every terminal command while blindfolded? Brace yourself for the ultimate Linux showdown!',
        topic: 'Linux'
    },
]
</script>

<style lang="scss" scoped>
.select {
    .select__wrapper {
        .select__cards-container {
            justify-items: center;
            display: grid;
            grid-template-columns: repeat(3, 1fr);
            column-gap: 40px;
            row-gap: 30px;
            margin-bottom: 40px;
        }
    }
}
</style>