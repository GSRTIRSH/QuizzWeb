import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [vue()],
    resolve: {
        alias: [{ find: '@', replacement: '/src' }],
    },
    css: {
        preprocessorOptions: {
            scss: {
                additionalData: `
                @import "./src/assets/layouts/_vars.scss";
                @import "./src/assets/layouts/_mixins.scss";
                `
            },
        },
    },
});
