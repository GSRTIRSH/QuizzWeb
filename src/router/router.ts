import MainView from '@/views/MainView.vue';
import AuthView from '@/views/AuthView.vue';
import QuizView from '@/views/QuizView.vue';
import { RouteRecordRaw, createRouter, createWebHistory } from 'vue-router';

const routes: Array<RouteRecordRaw> = [
    {
        name: 'Main',
        path: '/',
        component: MainView,
    },
    {
        name: 'Auth',
        path: '/auth',
        component: AuthView,
    },
    {
        name: 'Quiz',
        path: '/quiz/:topic/:difficulty',
        component: QuizView,
        props: true
    },
];

const router = createRouter({
    routes,
    history: createWebHistory(),
});

export default router;
