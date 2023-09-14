import Main from '@/pages/Main.vue';
import Auth from '@/pages/Auth.vue';
import Select from '@/pages/Select.vue';
import Quiz from '@/pages/Quiz.vue';
import { createRouter, createWebHistory } from 'vue-router';

const routes = [
    {
        path: '/',
        component: Main,
    },
    {
        path: '/auth',
        component: Auth,
    },
    {
        path: '/select',
        component: Select,
    },
    {
        path: '/quiz',
        component: Quiz,
    },
];

const router = createRouter({
    routes,
    history: createWebHistory(),
});

export default router;
