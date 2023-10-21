<template>
    <header class="tw-h-16 tw-bg-secondary-background">
        <div class="tw-mx-auto tw-max-w-6xl tw-h-full tw-flex tw-justify-between tw-items-center tw-px-4">
            <span class="tw-relative tw-bottom-1 tw-font-base-ui tw-text-4xl tw-cursor-pointer" 
                @click="$router.push({name: 'Main'})"
            >
                <span class="tw-text-base-orange">CODE</span>
                <span class="tw-text-base-yellow">BUSTERS</span>
            </span>
            <div class="tw-flex">
                <Button @click="$router.push({name: 'Auth'})">
                    Log in
                </Button>
            </div>
        </div>
    </header>
</template>

<script setup lang="ts"></script>


