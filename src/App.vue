<template>
    <div class="app tw-bg-base-background tw-bg-no-repeat tw-bg-cover">
        <TheHeader />
        <div class="tw-max-w-5xl tw-mx-auto tw-px-4">
            <router-view />
        </div>
        <TheFooter v-if="!route.path.includes('/quiz')" />
    </div>
</template>

<script setup lang="ts">
import TheHeader from '@/components/TheHeader.vue'
import TheFooter from '@/components/TheFooter.vue'
import { useRoute } from 'vue-router'

const route = useRoute()
</script>

<style scoped>
.app {
    min-height: 100%;
    display: grid;
    grid-template-rows: auto 1fr auto;
}
</style>
