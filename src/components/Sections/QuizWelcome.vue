<template>
    <div class="tw-flex tw-items-center">
        <div class="tw-flex justify-content-between tw-items-center tw-h-96 tw-w-full tw-my-28">
            <div>
                <h1 class=" tw-font-bold tw-text-3xl tw-mb-3">
                    <span class="tw-text-base-orange">Ultimate </span>
                    <span class="tw-text-base-yellow">Quiz Adventure for Programming Enthusiasts</span>
                </h1>
                <p class=" tw-text-base-yellow tw-text-2xl tw-mb-8">
                    Embark on a Quest of Coding Knowledge with Codebusters Quiz
                    Game!
                </p>
                <Button @click="scrollToQuizSelect" class="tw-px-28 tw-py-3 tw-text-2xl">
                    let’s try
                </Button>
            </div>
            <img
                src="@/assets/images/main-image.gif"
                alt=""
                class="tw-object-contain tw-h-72"
            />
        </div>
    </div>
</template>

<script setup lang="ts">
const scrollToQuizSelect = () => {
    document.getElementById('scrollPoint')?.scrollIntoView({ behavior: 'smooth' });
}
</script>
