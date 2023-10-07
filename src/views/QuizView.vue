<script setup lang="ts">
import { Swiper, SwiperSlide } from 'swiper/vue';
import { Pagination, Navigation } from 'swiper/modules';
import 'swiper/scss';
import 'swiper/scss/pagination';

const test = [1,2,3,4,5,6,7,8,9]
</script>

<template>
    <div class="quiz">
        <swiper
            class="quiz__swiper"
            :grabCursor="false"
            :pagination="{
                type: 'progressbar',
            }"
            :navigation="{
                nextEl: '.swiper-button-next',
                prevEl: '.swiper-button-prev',
            }"
            :modules="[Pagination, Navigation]"
        >
            <swiper-slide v-for="(slide, index) in test" :key="index" class="quiz__slide">{{ slide }}</swiper-slide>
        </swiper>
    </div>
    <div class="quiz__button">
        <button class="swiper-button-prev">Prev</button>
        <button class="swiper-button-next">Next</button>
    </div>
</template>

<style lang="scss">
.quiz {
    display: flex;
    flex-direction: column;
}
.quiz__swiper {
    height: 100%;
    width: 100vw;
}
.quiz__slide {
    font-size: 100px;
    text-align: center;
}

.swiper-horizontal {
    .swiper-pagination-progressbar {
        height: 7px;
        .swiper-pagination-progressbar-fill {
            background-color: $base-orange;
        }
    }
}

.quiz__button {
    display: flex;
    justify-content: space-between;
}
</style>
