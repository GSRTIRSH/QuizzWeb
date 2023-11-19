<template>
    <div class="tw-bg-secondary-background tw-flex tw-flex-col tw-flex-grow tw-w-full tw-h-full tw-rounded-sm base-shadow tw-p-4">
        <div class="tw-flex tw-items-center tw-justify-between tw-mb-3">
            <div class="tw-font-base-text tw-font-bold tw-text-center tw-text-base-yellow">
                Account details
            </div>
            <Button icon="pi pi-save" outlined />
        </div>
        <form class="tw-flex tw-flex-col tw-grow">
            <div class="tw-flex tw-flex-col  tw-self-center tw-w-full tw-max-w-sm tw-gap-2 tw-flex-grow">
                <InputText
                    placeholder="Login"
                    class="tw-bg-secondary-background tw-border-base-yellow tw-w-full"
                />
                <InputText
                    placeholder="Email"
                    class="tw-bg-secondary-background tw-border-base-yellow tw-w-full"
                />
                <InputText
                    placeholder="Telegram"
                    class="tw-bg-secondary-background tw-border-base-yellow tw-w-full"
                />
                <InputText
                    placeholder="VK"
                    class="tw-bg-secondary-background tw-border-base-yellow tw-w-full"
                />
                <Textarea
                    placeholder="About me"
                    class="tw-bg-secondary-background tw-resize-none tw-border-base-yellow tw-grow"
                />
            </div>
        </form>
    </div>
</template>

<script setup lang="ts"></script>
