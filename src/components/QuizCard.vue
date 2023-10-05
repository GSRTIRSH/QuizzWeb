<template>
    <div class="card" @click="rotateCard">
        <div class="card__front" :class="{ 'rotate-to-front': isRotate }">
            <div class="card__image-container">
                <img class="card__image" :src="card.img" />
            </div>
            <h3 class="card__title">{{ card.title }}</h3>
            <div class="card__tags-container">
                <div class="card__tag">random</div>
                <div class="card__tag">random</div>
                <div class="card__tag">random</div>
                <div class="card__tag">random</div>
            </div>
        </div>
        <div class="card__back" :class="{ 'rotate-to-back': isRotate }">
            <div class="card__background-container">
                <h3 class="card__background-title">choose difficulty</h3>
                <div class="card__button-container">
                    <button @click.stop class="card__button">easy</button>
                    <button @click.stop class="card__button">medium</button>
                    <button @click.stop class="card__button">hard</button>
                </div>
                <div class="card__description">{{ card.description }}</div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { defineProps, ref } from 'vue';

interface Props {
    card: {
        img: string;
        title: string;
        description: string;
    };
}

const isRotate = ref(false);

const rotateCard = () => {
    isRotate.value = !isRotate.value;
};

defineProps<Props>();
</script>

<style lang="scss" scoped>
.card {
    max-width: 340px;
    position: relative;
    transition: 0.2s;

    &:hover {
        transform: scale(1.02);
    }
    .rotate-to-back {
        transform: rotateY(0deg) !important;
    }
    .rotate-to-front {
        transform: rotateY(180deg);
    }
    .card__front {
        background-color: $base-lightpurple;
        position: relative;
        backface-visibility: hidden;
        transition: transform 1s cubic-bezier(0.2, 0.85, 0.4, 1.275);
        height: 100%;

        .card__image-container {
            padding: 30px 20px 35px 20px;
            max-width: 280px;
            margin: 0 auto;
            overflow: hidden;
            height: 320px;
            display: flex;
            align-items: center;
        }
        .card__image {
            object-fit: contain;
        }
        .card__title {
            @include stroke(1px, #000);

            text-align: center;
            color: $base-orange;
            font-family: $base-text-font;
            font-size: 30px;
            font-style: normal;
            font-weight: bold;
            line-height: normal;

            margin-bottom: 30px;
        }
        .card__tags-container {
            margin: 0 auto;
            display: grid;
            grid-template-columns: repeat(2, 1fr);
            gap: 10px;
            max-width: 270px;

            .card__tag {
                text-align: center;
                background: #fa9c10;
                border: 2px solid $base-gray;
                border-radius: 6px;

                color: $base-gray;
                font-family: $base-title-font;
                font-size: 24px;
                font-style: normal;
                font-weight: 400;
                line-height: normal;
                letter-spacing: 0.648px;

                padding-bottom: 4px;
            }

            margin-bottom: 30px;
        }
    }

    .card__back {
        background-color: $base-lightpurple;
        position: absolute;
        top: 0;
        width: 100%;
        height: 100%;
        backface-visibility: hidden;
        transform: rotateY(-180deg);
        transition: transform 1s cubic-bezier(0.2, 0.85, 0.4, 1.275);

        .card__background-container {
            margin: 0 auto;
            max-width: 280px;
            .card__background-title {
                @include stroke(1px, #000);

                text-align: center;
                color: $base-orange;
                font-family: $base-text-font;
                font-size: 23px;
                font-style: normal;
                font-weight: 700;
                line-height: normal;

                margin-top: 30px;
                margin-bottom: 15px;
            }
            .card__button-container {
                margin: 0 auto;
                display: flex;
                flex-direction: column;
                gap: 18px;
                margin-bottom: 40px;
            }
            .card__button {
                font-family: $base-title-font;
                font-size: 27px;
                font-style: normal;
                font-weight: 400;
                line-height: normal;
                letter-spacing: 0.729px;
                padding-top: 12px;
                padding-bottom: 12px;
                background: $base-gray;
                
                &:nth-of-type(1) {
                    border: 2px solid #5bfa10;
                    color: #5bfa10;
                }
                &:nth-of-type(2) {
                    border: 2px solid $base-orange;
                    color: $base-orange;
                }
                &:nth-of-type(3) {
                    border: 2px solid #fa3a10;
                    color: #fa3a10;
                }
            }
            .card__description {
                @include stroke(1px, #000);

                color: $base-yellow;
                font-family: $base-text-font;
                font-size: 23px;
                font-style: normal;
                font-weight: 700;
                line-height: normal;
            }
        }
    }
}
</style>
