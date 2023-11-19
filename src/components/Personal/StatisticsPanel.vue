<template>
    <div
        class="
            tw-p-4 
            sm:tw-p-2 
            sm:tw-h-24 
            tw-bg-secondary-background 
            tw-rounded-sm base-shadow 
            tw-grid 
            tw-grid-cols-1 
            tw-grid-rows-[1fr_auto_1fr_auto_1fr] 
            tw-gap-4 
            sm:tw-grid-cols-[1fr_auto_1fr_auto_1fr] 
            sm:tw-grid-rows-none"
    >
        <div class="tw-self-center">
            <div class="tw-flex sm:tw-justify-center tw-gap-6">
                <img src="@/assets/images/heart.png" class="tw-w-10 tw-object-contain" />
                <div class="">
                    <div class="tw-font-base-text tw-text-xl">0</div>
                    <div class="tw-font-base-text tw-text-[0.7rem]">Rating</div>
                </div>
            </div>
        </div>
        <div class="tw-bg-base-yellow tw-h-[1px] sm:tw-h-full sm:tw-w-[1px]"></div>
        <div class="tw-self-center">
            <div class="tw-flex sm:tw-justify-center tw-gap-6">
                <img src="@/assets/images/graduation-cap.png" class="tw-w-10 tw-object-contain" />
                <div class="">
                    <div class="tw-font-base-text tw-text-xl">0</div>
                    <div class="tw-font-base-text tw-text-[0.7rem]">Tests passed</div>
                </div>
            </div>
        </div>
        <div class="tw-bg-base-yellow tw-h-[1px] sm:tw-h-full sm:tw-w-[1px]"></div>
        <div class="tw-self-center">
            <div class="tw-flex sm:tw-justify-center tw-gap-6">
                <img src="@/assets/images/trophy.png" class="tw-w-10 tw-object-contain" />
                <div class="">
                    <div class="tw-font-base-text tw-text-xl">0</div>
                    <div class="tw-font-base-text tw-text-[0.7rem]">Tests created</div>
                </div>
            </div>
        </div>
    </div>
</template>
