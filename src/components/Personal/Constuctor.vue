<script setup lang="ts">
import { ref } from 'vue'

const dialogVisible = ref(false)

const openDialog = () => (dialogVisible.value = true)



const test = ref(false)

defineExpose({
    openDialog
})
</script>

<template>
    <Dialog
        modal
        header="Creating a quiz"
        v-model:visible="dialogVisible"
        :draggable="false"
        class="tw-w-[45rem] max-h-[80vh]"
    >
        <form class="tw-flex tw-flex-col tw-gap-2">
            <div class="tw-grid tw-grid-cols-[2.5fr_1fr] tw-grid-rows-[auto_1fr] tw-gap-2">
                <div>
                    <div class="tw-mb-1">Name:</div>
                    <InputText
                        placeholder="For example: Test your cat's temperament"
                        class="tw-w-full"
                    />
                </div>
                <div class="tw-flex tw-flex-col tw-flex-grow">
                    <div class="tw-mb-1">Description:</div>
                    <Textarea
                        rows="4"
                        placeholder="For example: Does your cat walk around you with a strange look? Maybe she wants to eat you? Or does he see ghosts? Check your pet's mental health!"
                        class="tw-w-full tw-grow tw-resize-none"
                    />
                </div>
                <div
                    class="tw-mt-[27px] tw-p-2 tw-gap-2 tw-max-h-min tw-border-base-yellow tw-border-[1px] tw-rounded-sm tw-flex tw-flex-col tw-flex-grow tw-row-start-1 tw-row-end-3 tw-col-start-2 tw-col-end-3"
                >
                    <img
                        class="tw-grow tw-rounded-sm tw-object-contain tw-aspect-square"
                        src="../../assets/images/JavaScriptLogo.png"
                    />
                    <FileUpload
                        customUpload
                        chooseLabel="upload cover"
                        accept="image/png"
                        mode="basic"
                        :auto="true"
                        @uploader=""
                        class="tw-w-full"
                    />
                </div>
            </div>
            <div class="tw-flex tw-flex-col tw-gap-2">
                <div class="tw-h-[1px] tw-bg-base-yellow" />
                <div>
                    <div class="tw-mb-1">Question text:</div>
                    <InputText
                        placeholder="For example: Test your cat's temperament"
                        class="tw-w-full"
                    />
                </div>
                <div class="">
                    <div>Answers:</div>
                    <div class="tw-flex tw-flex-col tw-gap-2">
                        <div class="tw-flex tw-gap-1">
                            <InputText placeholder="For example: Maned wolf" class="tw-w-full" />
                            <Button 
                                icon="pi pi-check" 
                                :outlined="test ? false : true" 
                                @click="test = !test"
                                class="tw-px-5"
                            />
                            <Button icon="pi pi-trash" outlined class="tw-px-5"/>
                        </div>
                        <div class="tw-flex tw-gap-1">
                            <InputText placeholder="For example: Maned wolf" class="tw-w-full" />
                            <Button 
                                icon="pi pi-check" 
                                :outlined="test ? false : true" 
                                @click="test = !test"
                                class="tw-px-5"
                            />
                            <Button icon="pi pi-trash" outlined class="tw-px-5"/>
                        </div>
                        <div class="tw-flex tw-gap-1">
                            <InputText placeholder="For example: Maned wolf" class="tw-w-full" />
                            <Button 
                                icon="pi pi-check" 
                                :outlined="test ? false : true" 
                                @click="test = !test"
                                class="tw-px-5"
                            />
                            <Button icon="pi pi-trash" outlined class="tw-px-5"/>
                        </div>
                        <div class="tw-flex tw-gap-1">
                            <InputText placeholder="For example: Maned wolf" class="tw-w-full" />
                            <Button 
                                icon="pi pi-check" 
                                :outlined="test ? false : true" 
                                @click="test = !test"
                                class="tw-px-5"
                            />
                            <Button icon="pi pi-trash" outlined class="tw-px-5"/>
                        </div>
                    </div>
                </div>
                <Button class="tw-justify-center tw-w-full" outlined>Add answer</Button>
            </div>

            <Button class="tw-justify-center">Add question</Button>
        </form>
    </Dialog>
</template>

<style scoped lang="scss">
</style>
