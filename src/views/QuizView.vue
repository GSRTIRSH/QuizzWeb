<script setup lang="ts">
import type { Question } from '@/types/types';
import QuizLoader from '@/components/UI/QuizLoader.vue'
import { getListOfQuizzes } from '@/api/getListOfQuizzes';
import { useAsyncState } from '@vueuse/core';
import {ref, computed, watch } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute()

const { state: quizState, isLoading: isQuizLoading } = useAsyncState<Question[]>(
    getListOfQuizzes(route.params.tag, route.params.difficulty), []
);

const currentQuestionIndex = ref(0);
const selectedAnswer = ref('');
const isAnswerChecked = ref(false)
const correctAnswersCounter = ref(0)
const progressbarValue = ref(0)

watch(isQuizLoading, () => {
    progressbarValue.value = 100 / quizState.value.length
})

const currentQuestion = computed(() => quizState.value[currentQuestionIndex.value]);
const nextQuestion = () => {
    progressbarValue.value += 100 / quizState.value.length
    currentQuestionIndex.value++;
    selectedAnswer.value = ''
};

const selectAnswer = (key:string) => {
    if(!isAnswerChecked.value) {
        selectedAnswer.value = key
    }
}

const checkAnswer = () => {
    if(selectedAnswer.value) {
        isAnswerChecked.value = true
        setTimeout(() => {
            isAnswerChecked.value = false
            nextQuestion()
        }, 2000)
    if (selectedAnswer.value == currentQuestion.value.correct_answer) {
        correctAnswersCounter.value++
        } 
    }
};
</script>

<template>
    <QuizLoader v-if="isQuizLoading"/>
    <div v-if="!isQuizLoading" class="quiz">
        <div class="quiz__wrapper">
            <button class="quiz__return-btn">go back</button>
            <div class="quiz__progress-wrapper">
                <div class="quiz__progress-bar">
                    <div 
                        class="quiz__per"
                        :style="{width: progressbarValue + '%'}"
                    />
                </div>
                <div class="quiz__progress-count">
                    {{ currentQuestionIndex + 1 }}/{{ quizState.length }}
                </div>
            </div>
            <h2 class="quiz__question">
                {{ currentQuestion.question }}
            </h2>
            <div class="quiz__answers-container">
                <button
                    v-for="(answer, key) in currentQuestion.answers"
                    :key="key"
                    @click="selectAnswer(key)"
                    v-show="answer !== null"
                    class="quiz__answer"
                    :class="{
                    'quiz__answer_active': 
                        key === selectedAnswer &&
                        !isAnswerChecked,
                    'quiz__answer_correct': 
                        key === currentQuestion.correct_answer && 
                        isAnswerChecked,
                    'quiz__answer_incorrect': 
                        key !== currentQuestion.correct_answer && 
                        isAnswerChecked &&
                        key === selectedAnswer
                    }"
                >{{ answer }}</button>
            </div>
            <div class="quiz__btn-wrapper">
                <button @click="nextQuestion">skip</button>
                <button @click="checkAnswer">submit</button>
            </div>
        </div>
    </div>
</template>

<style lang="scss" scoped>
.quiz__wrapper {
    // min-height: 100%;
    // display: grid;
    // grid-template-rows: auto auto auto 1fr auto;
}
.quiz__return-btn {
    @include stroke(2px, #000);

    color: $base-yellow;
    font-family: Visitor_Rus;
    font-size: 28px;
    font-style: normal;
    font-weight: 400;
    line-height: normal;
    letter-spacing: 0.756px;

    margin-top: 20px;
    margin-bottom: 30px;
}
.quiz__progress-wrapper {
    margin-bottom: 40px;

    display: flex;
    align-items: center;

    .quiz__progress-bar {
        height: 10px;
        width: 100%;
        background: #514c51;

        .quiz__per {
            height: 100%;
            width: 10%;
            background: $base-orange;
            animation: progress 0.8s ease-in-out forwards;
        }
    }

    .quiz__progress-count {
        position: relative;
        bottom: 1.5px;
        margin: 0px 5px 0px 5px;

        color: $base-yellow;
        text-align: center;
        font-family: $base-font;
        font-size: 20px;
        font-style: normal;
        font-weight: 400;
        line-height: normal;
        letter-spacing: 1.34px;
    }

    @keyframes progress {
        0% {
            width: 0;
            opacity: 1;
        }
        100% {
            opacity: 1;
        }
    }
}
.quiz__question {
    @include stroke(2px, #000);

    // text-align: center;
    color: $base-yellow;
    font-family: $base-font;
    font-size: 30px;
    font-style: normal;
    font-weight: 400;
    line-height: normal;
    letter-spacing: 2.345px;

    margin-bottom: 15px;
}
.quiz__answers-container {
    margin-bottom: 30px;
    display: grid;
    grid-template-columns: 1fr 1fr;
    column-gap: 30px;
    row-gap: 20px;
}
.quiz__answer {
    @include stroke(2px, #000);

    padding: 5px 0px 10px 0px;
    background: $base-gray;

    color: $base-yellow;
    text-align: center;
    font-family: $base-font;
    font-size: 20px;
    font-style: normal;
    font-weight: 400;
    line-height: normal;
    letter-spacing: 2.01px;
    border: 2px solid $base-yellow;
}
.quiz__answer_active {
    color: $base-orange;
    border: 2px solid $base-orange;
}
.quiz__answer_correct {
    color: green;
    border: 2px solid green;
}
.quiz__answer_incorrect {
    color: red;
    border: 2px solid red;
}
.quiz__btn-wrapper {
    display: flex;
    justify-content: space-between;

    button {
        width: 250px;
        background: $base-gray;
        padding: 6px 0px 10px 0px;

        text-align: center;
        font-family: $base-font;
        font-size: 26px;
        font-style: normal;
        font-weight: 400;
        line-height: normal;
        letter-spacing: 1.742px;

        &:first-child {
            border: 3px solid $base-orange;
            color: $base-orange;
        }
        &:last-child {
            border: 3px solid $base-yellow;
            color: $base-yellow;
        }
    }
}
</style>
