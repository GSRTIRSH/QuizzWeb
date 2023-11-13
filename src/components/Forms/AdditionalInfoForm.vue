<template>
    <div class="tw-flex tw-items-center tw-justify-between tw-mb-3">
        <div class="tw-font-base-text tw-font-bold tw-text-center tw-text-base-yellow">Additional info</div>
        <Button icon="pi pi-save" outlined/>
    </div>
    <form
        class="tw-flex tw-flex-col"
    >
        <div class="tw-flex tw-flex-col tw-self-center tw-w-full tw-max-w-sm tw-gap-2">
            <Textarea 
                rows="3" 
                autoResize 
                placeholder="About me"
                class="tw-bg-secondary-background tw-border-base-yellow" 
            />
            <InputText 
                placeholder="Telegram ID"
                class="tw-bg-secondary-background tw-border-base-yellow"
            />
            <InputText 
                placeholder="VK ID"
                class="tw-bg-secondary-background tw-border-base-yellow"
            />
        </div>
    </form>
</template>