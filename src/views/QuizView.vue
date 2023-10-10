<script setup lang="ts">
import { Swiper, SwiperSlide, useSwiper } from 'swiper/vue';
import { Pagination, Navigation } from 'swiper/modules';
import 'swiper/scss';
import 'swiper/scss/pagination';
import type { Question } from '@/types/types';
import QuizLoader from '@/components/UI/QuizLoader.vue';
import { getListOfQuizzes } from '@/api/getListOfQuizzes';
import { useAsyncState } from '@vueuse/core';
import { ref, computed, watch } from 'vue';
import { useRoute } from 'vue-router';
import { $ref } from 'vue/macros';

const route = useRoute();

const { 
    state: quizState, 
    isLoading: isQuizLoading 
} = useAsyncState<Question[]>(getListOfQuizzes(route.params.topic, route.params.difficulty), []);

const QuizSwiper = ref();
const currentSlideIndex = ref(0);
const selectedAnswers = ref<string[]>([]);

const addAnswer = (questionIndex: number, answerIndex: string) =>
    (selectedAnswers.value[questionIndex] = answerIndex);
</script>

<template>
    <QuizLoader v-if="isQuizLoading" />
    <div class="quiz" v-if="!isQuizLoading">
        <swiper
            class="quiz__swiper"
            :grabCursor="false"
            :pagination="{
                type: 'progressbar',
            }"
            :navigation="{
                nextEl: '.quiz__button-next',
                prevEl: '.quiz__button-prev',
            }"
            :modules="[Pagination, Navigation]"
            :allowTouchMove="false"
            @swiper="(e) => (QuizSwiper = e)"
            @activeIndexChange="() => (currentSlideIndex = QuizSwiper.activeIndex)" 
        >
            <swiper-slide
                class="quiz__slide"
                v-for="(question, questionIndex) in quizState"
                :key="questionIndex"
            >
                <div class="quiz__slide-container slide__wrapper">
                    <h1 class="quiz__question">
                        {{ question.question }}
                    </h1>
                    <div class="quiz__answers-container">
                        <div
                            class="quiz__answer"
                            v-for="(answer, answerIndex) in question.answers"
                            :key="answerIndex"
                            v-show="answer"
                            @click="addAnswer(questionIndex, answerIndex)"
                            :class="{
                                'quiz__answer-active': 
                                    answerIndex === selectedAnswers[questionIndex]
                                }"
                        >
                            <span>{{ answer }}</span>
                        </div>
                    </div>
                </div>
            </swiper-slide>
        </swiper>
        <div class="quiz__buttons buttons__wrapper">
            <button class="quiz__button-prev"><span>Prev</span></button>
            <button v-show="currentSlideIndex !== quizState.length - 1" class="quiz__button-next"><span>Next</span></button>
            <button @click="$router.push({name: 'Results'})" v-show="currentSlideIndex === quizState.length - 1" class="quiz__button-submit"><span>Submit</span></button>
        </div>
    </div>
</template>

<style lang="scss">
.quiz {
    display: flex;
    flex-direction: column;

    .quiz__swiper {
        height: 100%;
        width: 100vw;
    }

    .quiz__slide {
        font-size: 20px;
        text-align: center;

        .quiz__slide-container {
            display: flex;
            flex-direction: column;
            height: 100%;
            .quiz__question {
                @include stroke(2px, #000000);

                color: $base-yellow;
                font-family: $base-text-font;
                font-size: 26px;
                font-style: normal;
                font-weight: bold;
                line-height: normal;
                letter-spacing: 2.345px;
                text-align: start;

                margin-top: 40px;
                margin-bottom: 30px;
            }

            .quiz__answers-container {
                flex: 1;
                display: grid;
                grid-template-columns: 1fr;
                gap: 10px;
                margin-bottom: 50px;
            }

            .quiz__answer {
                background: $base-lightpurple;
                height: 100%;
                display: flex;
                align-items: center;
                justify-content: center;
                border: 3px solid $base-gray;
                cursor: pointer;

                span {
                    @include stroke(1px, #000000);
                    padding: 10px;
                    font-family: $base-text-font;
                    color: $base-yellow;
                    font-size: 1.4vw;
                    font-style: normal;
                    font-weight: bold;
                    line-height: normal;
                    letter-spacing: 0.5px;
                    text-align: center;
                }
            }

            .quiz__answer-active {
                border: 3px solid $base-yellow;
            }

            .quiz__answer::-webkit-scrollbar {
                display: none;
            }
        }
    }

    .swiper-horizontal {
        .swiper-pagination-progressbar {
            height: 7px;

            .swiper-pagination-progressbar-fill {
                background-color: $base-orange;
            }
        }
    }

    .quiz__buttons {
        display: flex;
        justify-content: space-between;
        width: 100%;
        margin-bottom: 50px;

        button {
            width: 200px;
            height: 50px;
            background: $base-gray;
            font-family: $base-title-font;
            font-size: 26px;
            font-weight: 400;
            letter-spacing: 1.742px;

            span {
                position: relative;
                bottom: 2px;
            }
        }

        .quiz__button-prev {
            border: 2px solid $base-yellow;
            color: $base-yellow;
        }
        .quiz__button-next {
            border: 2px solid $base-orange;
            color: $base-orange;
        }
        .quiz__button-submit {
            border: 2px solid rgb(30, 157, 30);
            color: rgb(30, 157, 30);
        }
    }
}
</style>
