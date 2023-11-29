<script setup lang="ts">
// import { Swiper, SwiperSlide, useSwiper } from 'swiper/vue';
// import { Pagination, Navigation } from 'swiper/modules';
import 'swiper/scss';
import 'swiper/scss/pagination';
// import QuizLoader from '@/components/QuizLoader.vue';
// import { getListOfQuizzes } from '@/api/getListOfQuizzes';
import { useAsyncState } from '@vueuse/core';
import { ref, computed, watch } from 'vue';
import { useRoute } from 'vue-router';
// import { $ref } from 'vue/macros';

const route = useRoute();

// const { state: quizState, isLoading: isQuizLoading } = useAsyncState<any>(getListOfQuizzes(route.params.topic, route.params.difficulty), []);

const QuizSwiper = ref();
const currentSlideIndex = ref(0);
const selectedAnswers = ref([]);

// const addAnswer = (questionIndex: any, answerIndex: any) =>
//     (selectedAnswers.value[questionIndex] = answerIndex);
</script>

<template>
    <!-- <QuizLoader v-if="isQuizLoading" />
    <div
        class="tw-flex tw-flex-col tw-max-w-none tw-px-0"
        v-if="!isQuizLoading"
    >
        <swiper
            class="tw-max-w-[100vw] tw-h-full"
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
            @activeIndexChange="
                () => (currentSlideIndex = QuizSwiper.activeIndex)
            "
        >
            <swiper-slide
                class="tw-text-xl tw-text-center"
                v-for="(question, questionIndex) in quizState"
                :key="questionIndex"
            >
                <div class="tw-px-4 tw-max-w-6xl tw-mx-auto">
                    <h1 class="tw-text-base-yellow tw-font-bold tw-text-3xl tw-text-start tw-mt-10 tw-mb-6">
                        {{ question.question }}
                    </h1>
                    <div class="tw-grid tw-gap-3 tw-columns-1">
                        <Button
                            v-for="(answer, answerIndex) in question.answers"
                            :key="answerIndex"
                            @click="addAnswer(questionIndex, answerIndex)"
                            v-show="answer"
                            :outlined="answerIndex === selectedAnswers[questionIndex] ? false : true"
                            class="tw-font-base-title tw-justify-center tw-text-xl"
                            :class="{
                                'quiz__answer-active':
                                    answerIndex ===
                                    selectedAnswers[questionIndex],
                            }"
                        >
                            <span>{{ answer }}</span>
                        </Button>
                    </div>
                </div>
            </swiper-slide>
        </swiper>
        <div class="tw-max-w-6xl tw-mx-auto tw-flex tw-justify-between tw-w-full tw-mb-5">
            <Button class="quiz__button-prev"><span>Prev</span></Button>
            <Button
                v-show="currentSlideIndex !== quizState.length - 1"
                class="quiz__button-next"
            >
                <span>Next</span>
            </Button>
            <Button
                @click="$router.push({ name: 'Results' })"
                v-show="currentSlideIndex === quizState.length - 1"
                class=""
                severity="success"
            >
                <span>Submit</span>
            </Button>
        </div>
    </div> -->
</template>

<style lang="scss">
.swiper-horizontal {
    .swiper-pagination-progressbar {
        height: 7px;
        .swiper-pagination-progressbar-fill {
            background-color: $base-orange;
        }
    }
}
</style>
