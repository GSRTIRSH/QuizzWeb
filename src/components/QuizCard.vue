<script setup lang="ts">
import { defineProps, ref } from 'vue';

interface Props {
    card: {
        img: string;
        title: string;
        description: string;
        topic: string;
    };
}

const props = defineProps<Props>();
const emit = defineEmits();

const isRotate = ref(false);

const rotateCard = () => (isRotate.value = !isRotate.value);

const emitCardData = (difficulty: string) =>
    emit('cardData', [props.card.topic, difficulty]);
</script>

<template>
<Card class="tw-w-full" @click.stop="emitCardData('Easy')">
    <template #header>
        <img :src="card.img" class="tw-h-56 tw-w-full tw-object-cover tw-p-3"/>
    </template>
    <template #title>
        <Chip label="Xuxue Feng" :image="card.img" />
         <div>Advanced Card</div> 
    </template>
    
    <template #footer>
        Card subtitle
    </template>
</Card>
</template>

<!-- <template>
    <div class="card" @click="rotateCard">
        <div
            class="card__front tw-bg-secondary-background card-container"
            :class="{ 'rotate-to-back': isRotate }"
        >
            <img class="card__image" :src="card.img" />
            <h3 class="card__title">{{ card.title }}</h3>
            <div class="card__tags-container">
                <div class="card__tag">random</div>
                <div class="card__tag">random</div>
                <div class="card__tag">random</div>
                <div class="card__tag">random</div>
            </div>
        </div>
        <div
            class="card__back card-container"
            :class="{ 'rotate-to-front': isRotate }"
        >
            <h3 class="card__background-title">choose difficulty</h3>
            <div class="card__button-container">
                <button @click.stop="emitCardData('Easy')" class="card__button">
                    easy
                </button>
                <button
                    @click.stop="emitCardData('Medium')"
                    class="card__button"
                >
                    medium
                </button>
                <button @click.stop="emitCardData('Hard')" class="card__button">
                    hard
                </button>
            </div>
            <div class="card__description">{{ card.description }}</div>
        </div>
    </div>
</template>

<style lang="scss" scoped>
.card {
    position: relative;
    transition: 0.2s;
    width: 100%;

    &:hover {
        transform: scale(1.02);
    }
    .rotate-to-front {
        transform: rotateY(0deg) !important;
    }
    .rotate-to-back {
        transform: rotateY(180deg);
    }
    .card-container {
        padding: 30px;
        display: flex;
        flex-direction: column;
        height: 100%;
    }
    .card__front {
        position: relative;
        backface-visibility: hidden;
        transition: transform 1s cubic-bezier(0.2, 0.85, 0.4, 1.275);
        height: 100%;

        .card__image {
            user-select: none;
            flex-grow: 1;
            object-fit: contain;
            margin-bottom: 18px;
        }

        .card__title {
            @include stroke(1px, #000);

            flex-grow: 0;
            text-align: center;
            color: $base-orange;
            font-family: $base-text-font;
            font-size: 30px;
            font-style: normal;
            font-weight: bold;
            line-height: normal;

            pointer-events: none;
            user-select: none;

            margin-bottom: 30px;
        }
        .card__tags-container {
            // margin: 0 auto;
            display: grid;
            grid-template-columns: repeat(2, 1fr);
            gap: 10px;
            flex-grow: 0;

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

                pointer-events: none;
                user-select: none;

                padding-bottom: 4px;
            }
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

        .card__background-title {
            @include stroke(1px, #000);

            text-align: center;
            color: $base-orange;
            font-family: $base-text-font;
            font-size: 18px;
            font-style: normal;
            font-weight: 700;
            line-height: normal;
            pointer-events: none;
            user-select: none;

            margin-top: 30px;
            margin-bottom: 15px;
        }
        .card__button-container {
            margin: 0 auto;
            display: flex;
            flex-direction: column;
            gap: 18px;
            margin-bottom: 40px;
            width: 100%;
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
            user-select: none;

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

            user-select: none;
            pointer-events: none;
            color: $base-yellow;
            font-family: $base-text-font;
            font-size: 15px;
            font-style: normal;
            font-weight: 700;
            line-height: normal;
        }
    }
}
</style> -->
