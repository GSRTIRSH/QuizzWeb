<script setup lang="ts">
import AccountDetailsForm from '@/components/Forms/AccountDetailsForm.vue'
import AdditionalInfoForm from '@/components/Forms/AdditionalInfoForm.vue'
</script>

<template>
    <div class="tw-mt-16 tw-max-w-6xl tw-mx-auto tw-px-4">
        <div 
            class="
                tw-grid
                tw-grid-cols-1
                sm:tw-grid-cols-2
                sm:tw-grid-rows-[1fr_1fr_auto]
                md:tw-grid-cols-3 
                md:tw-grid-rows-[auto_1fr] 
                tw-gap-y-10 
                tw-gap-x-8

                tw-justify-items-center  
                tw-mb-10
            "
        >
            <Card class="tw-bg-secondary-background sm:tw-row-start-1 sm:tw-row-end-3">
                <template #header>
                    <img
                        src="../assets/images/DevOpsLogo.png"
                        class="tw-h-56 tw-w-full tw-object-cover tw-p-3"
                    />
                </template>
                <template #title>
                    <div>Garvard</div>
                </template>
                <template #subtitle>
                    <div>Ochky123@gmail.com</div>
                </template>
                <template #footer>
                    <Button outlined class="tw-w-full tw-justify-center"> log out </Button>
                </template>
            </Card>
            <div 
                class="
                    tw-grid
                    tw-grid-cols-1
                    tw-grid-rows-[1fr_auto_1fr_auto_1fr]
                    tw-gap-4
                    sm:tw-grid-cols-[1fr_auto_1fr_auto_1fr]
                    sm:tw-grid-rows-none
                    sm:tw-row-start-3
                    sm:tw-row-end-5
                    sm:tw-col-start-1 
                    sm:tw-col-end-3
                    md:tw-row-start-1
                    md:tw-row-end-2
                    md:tw-col-start-2 
                    md:tw-col-end-4 

                    tw-p-4
                    sm:tw-p-2 
                    sm:tw-h-24 
                    tw-w-full
                    tw-bg-secondary-background 
                    tw-rounded-sm 
                    base-shadow
                "
            >
                <div class="tw-self-center">
                    <div class="tw-flex sm:tw-justify-center tw-gap-6">
                        <img src="@/assets/images/heart.png" class="tw-w-10 tw-object-contain">
                        <div class="">
                            <div class="tw-font-base-text tw-text-xl">0</div>
                            <div class="tw-font-base-text tw-text-[0.7rem]">Rating</div>
                        </div>
                    </div>
                </div>
                <div class="tw-bg-base-yellow tw-h-[1px] sm:tw-h-full sm:tw-w-[1px]"></div>
                <div class="tw-self-center">
                    <div class="tw-flex sm:tw-justify-center tw-gap-6">
                        <img src="@/assets/images/graduation-cap.png" class="tw-w-10 tw-object-contain">
                        <div class="">
                            <div class="tw-font-base-text tw-text-xl">0</div>
                            <div class="tw-font-base-text tw-text-[0.7rem]">Tests passed</div>
                        </div>
                    </div>
                </div>
                <div class="tw-bg-base-yellow tw-h-[1px] sm:tw-h-full sm:tw-w-[1px]"></div>
                <div class="tw-self-center">
                    <div class="tw-flex sm:tw-justify-center tw-gap-6">
                        <img src="@/assets/images/trophy.png" class="tw-w-10 tw-object-contain">
                        <div class="">
                            <div class="tw-font-base-text tw-text-xl">0</div>
                            <div class="tw-font-base-text tw-text-[0.7rem]">Tests created</div>
                        </div>
                    </div>
                </div> 
            </div>
            <div class="tw-bg-secondary-background tw-h-full tw-w-full tw-rounded-sm base-shadow tw-p-4">
                <AccountDetailsForm/>
            </div>
            <div class="tw-bg-secondary-background tw-h-full tw-w-full tw-rounded-sm base-shadow tw-p-4">
                <AdditionalInfoForm/>
            </div>
        </div>
    </div>
</template>