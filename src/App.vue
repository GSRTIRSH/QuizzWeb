<template>
    <div class="app tw-bg-base-background tw-relative">
        <Header/>
        <router-view/>
        <Footer/>
        <ParticleSystem class="tw-z-[-10]"/>
    </div>
</template>

<script setup lang="ts">
import Header from '@/components/General/Header.vue'
import Footer from '@/components/General/Footer.vue'
import ParticleSystem from './components/General/ParticleSystem.vue';
</script>

<style scoped>
.app {
    z-index: 10;
    min-height: 100%;
    display: grid;
    grid-template-rows: auto 1fr auto;
}
</style>
