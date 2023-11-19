<template>
    <div
        class="tw-bg-secondary-background tw-flex tw-flex-col tw-flex-grow tw-w-full tw-h-full tw-rounded-sm base-shadow tw-p-4"
    >
        <div class="tw-flex tw-items-center tw-justify-between tw-mb-3">
            <div class="tw-font-base-title tw-font-bold tw-text-center tw-text-base-yellow">
                Account details
            </div>
            <Button icon="pi pi-save" outlined class="tw-aspect-square" />
        </div>
        <form class="tw-flex tw-flex-col tw-grow">
            <div
                class="tw-flex tw-flex-col tw-self-center tw-w-full tw-max-w-sm tw-gap-2 tw-flex-grow"
            >
                <InputText placeholder="Login" class="tw-w-full" />
                <InputText placeholder="Email" class="tw-w-full" />
                <InputText placeholder="Telegram" class="tw-w-full" />
                <InputText placeholder="VK" class="tw-w-full" />
                <Textarea placeholder="About me" class="tw-resize-none tw-grow" />
            </div>
        </form>
    </div>
</template>

<script setup lang="ts"></script>
