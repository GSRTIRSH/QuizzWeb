<template>
    <div class="quiz">
        <div class="quiz__wrapper">
            <button class="quiz__return-btn">go back</button>
            <div class="quiz__progress-wrapper">
                <div class="quiz__progress-bar">
                    <div class="quiz__per"/>
                </div>
                <div class="quiz__progress-count">1/10</div>
            </div>
            <h2 class="quiz__question">
                Inside which HTML element do we put the JavaScript?
            </h2>
            <div class="quiz__answers-container">
                <button class="quiz__answer">some answer</button>
                <button class="quiz__answer">some answer</button>
                <button class="quiz__answer">some answer</button>
                <button class="quiz__answer">some answer</button>
                <button class="quiz__answer">some answer</button>
                <button class="quiz__answer">some answer</button>
            </div>
            <div class="quiz__btn-wrapper">
                <button>skip</button>
                <button>submit</button>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { useRoute } from 'vue-router';

const route = useRoute()
console.log(route.params.difficulty)


</script>

<style lang="scss" scoped>
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
        background: #514C51;

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
    font-size: 35px;
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
    font-size: 30px;
    font-style: normal;
    font-weight: 400;
    line-height: normal;
    letter-spacing: 2.01px;
    border: 2px solid $base-yellow;
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

        &:first-child{
            border: 3px solid $base-orange;
            color: $base-orange;
        }
        &:last-child{
            border: 3px solid $base-yellow;
            color: $base-yellow;
        }
    }
}
</style>