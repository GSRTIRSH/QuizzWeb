<script setup lang="ts">
import { defineProps } from 'vue'

const props = defineProps({
    userName: String,
    email: String,
    avatar: String,
    uploadAvatar: Function,
    logout: Function,
})
</script>

<template>
    <div class="tw-w-full tw-bg-secondary-background tw-p-4 tw-rounded-sm base-shadow tw-flex tw-flex-col tw-flex-grow">
            <div class="tw-grow tw-aspect-square tw-mb-4">
                <img class="tw-object-cover tw-rounded-sm" v-if="props.avatar" :src="props.avatar" />
                <Skeleton v-else size="100%"/>
            </div>
            <div v-if="props.userName" class="tw-mb-2 tw-font-base-text tw-font-bold tw-text-2xl">{{ props.userName }}</div>
            <Skeleton v-else width="100%" height="1.5rem" class="tw-mb-2"/>
            <div v-if="props.email" class="tw-mb-4 tw-font-base-text tw-text-md">{{ props.email }}</div>
            <Skeleton v-else width="100%" height="1.5rem" class="tw-mb-4"/>
            <FileUpload
                customUpload
                chooseLabel="upload image"
                accept="image/png"
                mode="basic"
                :auto="true"
                @uploader="props.uploadAvatar?.($event)"
                class="tw-w-full tw-mb-2"
            />
            <Button class="tw-w-full tw-justify-center" @click="props.logout" outlined> log out </Button>
    </div>
</template>
