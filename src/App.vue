<template>
    <div class="app">
        <TheHeader />
        <router-view />
        <TheFooter />
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import TheHeader from './components/TheHeader.vue';
import TheFooter from './components/TheFooter.vue';

export default defineComponent({
    setup() {
        return {};
    },
    components: { TheHeader, TheFooter },
});
</script>

<style scoped>
.app {
    min-height: 100%;
    display: grid;
    grid-template-rows: auto 1fr auto;
}
</style>
