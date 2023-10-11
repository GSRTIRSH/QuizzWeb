<template>
    <div class="select ">
        <div class="select__wrapper">
            <div id="scrollPoint" class="select__cards-container">
                <QuizCard v-for="(card, index) in baseCards" :key="index" :card="card" @cardData="handleCardData" />
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import QuizCard from '@/components/QuizCard.vue';
import { useRouter } from 'vue-router';
import img_js from '@/assets/images/JavaScriptLogo.png';
import img_html5 from '@/assets/images/HtmlLogo.png';
import img_php from '@/assets/images/PHPLogo.png';
import img_devops from '@/assets/images/DevOpsLogo.png';
import img_docker from '@/assets/images/DockerLogo.png';
import img_laravel from '@/assets/images/LaravelLogo.png';
import img_sql from '@/assets/images/SqlLogo.png';
import img_kubernetes from '@/assets/images/KubernetesLogo.png';
import img_linux from '@/assets/images/LinuxLogo.png';
const router = useRouter();

const handleCardData = (data: string[]) => {
    router.push({
        name: 'Quiz',
        params: { topic: data[0], difficulty: data[1] }
    });
}

const baseCards = [
    {
        img: img_js,
        title: 'JavaScript',
        description: 'JavaScript Test: unleashing the power of obscure syntax and random bugs',
        topic: 'JavaScript'
    },
    {
        img: img_html5,
        title: 'HTML5',
        description: 'HTML Test: Can you spot the missing closing tag? Good luck finding Waldo!',
        topic: 'HTML'
    },
    {
        img: img_php,
        title: 'PHP',
        description: 'CSS Test: Think its just about colors and fonts? Time to dive into the Bermuda Triangle of Cascading Style Sheets!',
        topic: 'PHP'
    },
    {
        img: img_devops,
        title: 'DevOps',
        description: 'DevOps Test: Are you a master of juggling flaming chainsaws while balancing on a tightrope? This quiz is childs play for you!',
        topic: 'DevOps'
    },
    {
        img: img_docker,
        title: 'Docker',
        description: 'Docker Test: Can you keep up with the pace of containers appearing and disappearing faster than Houdinis vanishing act?',
        topic: 'Docker'
    },
    {
        img: img_laravel,
        title: 'Laravel',
        description: 'Laravel Test: Do you have the patience of a Zen master to untangle the labyrinth of Laravels elegant yet mysterious code?',
        topic: 'Laravel'
    },
    {
        img: img_sql,
        title: 'MySQL',
        description: 'SQL Test: Ready to navigate through the twisted maze of database queries with just your wits and a handful of wildcards?',
        topic: 'MySQL'
    },
    {
        img: img_kubernetes,
        title: 'Kubernetes',
        description: 'Kubernetes Test: Think you can orchestrate a cluster of virtual minions like a puppet master? Time to prove your Kubernetes command!',
        topic: 'Kubernetes'
    },
    {
        img: img_linux,
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