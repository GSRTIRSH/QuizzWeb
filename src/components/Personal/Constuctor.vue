<script setup lang="ts">
import { FileUploadUploaderEvent } from 'primevue/fileupload';
import { ref } from 'vue'

interface Answer {
    text: string
    isCorrect: boolean
}

interface Question {
    text: string
    answers: Answer[]
}

interface Quiz {
    name: string
    description: string
    cover: string
    questions: Question[]
}

const dialogVisible = ref(false)
const coverImage = ref('')

const quiz = ref<Quiz>({
    name: '',
    description: '',
    cover: '',
    questions: []
})

const addQuestion = () => {
    quiz.value.questions.push({
        text: '',
        answers: []
    })
}

const addAnswer = (question: Question) => {
    question.answers.push({
        text: '',
        isCorrect: false
    })
}

const clearQuiz = () => {
    quiz.value = {
        name: '',
        description: '',
        cover: '',
        questions: []
    }
}

const coverSelect = (event: FileUploadUploaderEvent) => {
    const files = event.files
    const file = (Array.isArray(files) ? files[0] : files)
    if (file) coverImage.value = URL.createObjectURL(file)
}

const correctAnswerSelect = (qIndex: number, aIndex: number) => {
    quiz.value.questions[qIndex].answers[aIndex].isCorrect = !quiz.value.questions[qIndex].answers[aIndex].isCorrect
}

const openDialog = () => (dialogVisible.value = true)

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
        <template #header>
            <div class="tw-flex tw-justify-between tw-items-center tw-w-full">
                <h1 class="tw-font-base-title tw-text-2xl tw-font-bold tw-text-base-yellow">Creating a quiz</h1>
                <div class="tw-mr-1 tw-flex tw-gap-1">
                    <Button icon="pi pi-save" outlined class="tw-px-5"/>
                    <Button @click="clearQuiz" icon="pi pi-eraser" outlined class="tw-px-5"/>
                </div>
            </div>
        </template>
        <form class="tw-flex tw-flex-col tw-gap-2">
            <div class="tw-grid tw-grid-cols-[2.5fr_1fr] tw-grid-rows-[auto_1fr] tw-gap-2">
                <div>
                    <div class="tw-mb-1">Name:</div>
                    <InputText
                        v-model="quiz.name"
                        placeholder="For example: Test your cat's temperament"
                        class="tw-w-full"
                    />
                </div>
                <div class="tw-flex tw-flex-col tw-flex-grow">
                    <div class="tw-mb-1">Description:</div>
                    <Textarea
                        v-model="quiz.description"
                        rows="4"
                        placeholder="For example: Does your cat walk around you with a strange look? Maybe she wants to eat you? Or does he see ghosts? Check your pet's mental health!"
                        class="tw-w-full tw-grow tw-resize-none"
                    />
                </div>
                <div
                    class="tw-mt-[27.5px] tw-p-2 tw-gap-2 tw-max-h-min tw-border-base-yellow tw-border-[1px] tw-rounded-sm tw-flex tw-flex-col tw-flex-grow tw-row-start-1 tw-row-end-3 tw-col-start-2 tw-col-end-3"
                >
                    <img
                        class="tw-grow tw-h-full tw-rounded-sm tw-object-cover tw-aspect-square"
                        :src="coverImage"
                    />
                    <FileUpload
                        customUpload
                        chooseLabel="upload cover"
                        accept="image/png"
                        mode="basic"
                        :auto="true"
                        @uploader="coverSelect"
                        class="tw-w-full"
                    />
                </div>
            </div>
            <div
                v-for="(question, qIndex) in quiz.questions"
                :key="qIndex"
                class="tw-flex tw-flex-col tw-gap-2"
            >
                <div class="tw-h-[1px] tw-bg-base-yellow" />
                <div>
                    <div class="tw-mb-1">Question text:</div>
                    <div class="tw-flex tw-gap-1">
                        <InputText v-model="question.text" class="tw-w-full" />
                        <Button @click="quiz.questions.splice(qIndex, 1)" icon="pi pi-trash" outlined class="tw-px-5" />
                    </div>
                </div>
                <div v-if="question.answers.length">Answers:</div>
                <div v-for="(answer, aIndex) in question.answers" :key="aIndex" class="">
                    <div class="tw-flex tw-flex-col tw-gap-2">
                        <div class="tw-flex tw-gap-1">
                            <InputText v-model="answer.text" class="tw-w-full" />
                            <Button 
                                icon="pi pi-check" 
                                class="tw-px-5" 
                                :outlined="answer.isCorrect ? false : true" 
                                @click="correctAnswerSelect(qIndex, aIndex)" 
                            />
                            <Button @click="quiz.questions[qIndex].answers.splice(aIndex, 1)" icon="pi pi-trash" outlined class="tw-px-5" />
                        </div>
                    </div>
                </div>
                <Button @click="addAnswer(question)" class="tw-justify-center tw-w-full" outlined>Add answer</Button>
            </div>
            <Button @click="addQuestion" class="tw-justify-center">Add question</Button>
        </form>
    </Dialog>
</template>

<style scoped lang="scss"></style>
