<script setup lang="ts">
import AccountDetailsForm from '@/components/Personal/AccountDetailsForm.vue'
import FriendsPanel from '@/components/Personal/FriendsPanel.vue'
import StatisticsPanel from '@/components/Personal/StatisticsPanel.vue'
import PersonalCard from '@/components/Personal/PersonalCard.vue'
import Constructor from '@/components/Personal/Constuctor.vue'
import { useAuthStore } from '@/store/authStore'
import { useUserStore } from '@/store/userStore'
import { ref, toRefs } from 'vue'

const { logout } = useAuthStore()
const { userData, avatar } = toRefs(useUserStore())
const { uploadAvatar } = useUserStore()
const ConstructorRef = ref();
</script>

<template>
    <Constructor ref="ConstructorRef"/>
    <div class="tw-mt-16 tw-max-w-6xl tw-mx-auto tw-px-4 tw-w-full">
        <div
            class="
                tw-grid 
                tw-grid-cols-1 
                sm:tw-grid-cols-2 
                sm:tw-grid-rows-[auto_auto_auto] 
                md:tw-grid-cols-3 
                md:tw-grid-rows-[auto_1fr_auto] 
                tw-gap-y-8 
                tw-gap-x-8 
                tw-justify-items-center 
                tw-mb-10"
            >
            <PersonalCard
                :userName="userData?.userName"
                :email="userData?.email"
                :avatar="avatar"
                :logout="logout"
                :uploadAvatar="uploadAvatar"
                class="
                    tw-row-start-1
                    tw-row-end-3
                    sm:tw-row-start-2 
                    sm:tw-row-end-4 
                    md:tw-row-start-1 
                    md:tw-row-end-3 
                "
            />
            <div 
                class="
                    md:tw-row-start-3
                    md:tw-row-end-4
                    sm:tw-row-start-4
                    sm:tw-row-end-6
                    tw-p-5 
                    tw-bg-secondary-background 
                    base-shadow 
                    tw-rounded-sm
                    tw-min-w-full
                "
            >
                <Button @click="ConstructorRef?.openDialog()" class="tw-w-full tw-justify-center">Create quiz</Button>
            </div>
            <StatisticsPanel 
                class="
                    sm:tw-row-start-1 
                    sm:tw-row-end-2 
                    sm:tw-col-start-1 
                    sm:tw-col-end-3 
                    md:tw-row-start-1 
                    md:tw-row-end-2 
                    md:tw-col-start-2 
                    md:tw-col-end-4 
                    tw-w-full
                "
            />
            <AccountDetailsForm 
                class="
                    md:tw-row-start-2
                    md:tw-row-end-4
                "
            />
            <FriendsPanel 
                class="
                    sm:tw-row-start-3
                    sm:tw-row-end-6
                    md:tw-row-start-2
                    md:tw-row-end-4
                "
            />
        </div>
    </div>
</template>
