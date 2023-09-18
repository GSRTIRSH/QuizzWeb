<template>
    <div class="quiz">
        <div class="quiz__wrapper">
            <h1 class="quiz__title"> <span>Java script </span><span>questions</span></h1>
            <div class="quiz__question-container">
                <p>Inside which HTML element do we put the JavaScript?</p>
            </div>
            <div class="quiz__answers-container">
                <button class="quiz__answer"><span>JS</span></button>
                <button class="quiz__answer"><span>SCRIPT</span></button>
                <button class="quiz__answer"><span>SCRIPTING</span></button>
                <button class="quiz__answer"><span>JAVASCRIPT</span></button>
            </div>
            <div class="quiz__counter">
                <button class="quiz__question-number"><span>question 1</span></button>
                <button class="quiz__question-number"><span>question 2</span></button>
                <button class="quiz__question-number"><span>question 3</span></button>
                <button class="quiz__question-number"><span>question 4</span></button>
                <button class="quiz__question-number"><span>question 5</span></button>
                <button class="quiz__question-number"><span>question 6</span></button>
                
            </div>
            <button class="quiz__button">submit answers</button>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
    setup () {
        

        return {}
    }
})
</script>

<style lang="scss" scoped>
.quiz__wrapper {
    display: flex;
    flex-direction: column;
}
.quiz__title {
    @include stroke(2px, #000);

    margin-top: 30px;
    margin-bottom: 15px;
    text-align: center;
    font-family: $base-font;
    font-size: 36px;
    font-style: normal;
    font-weight: 400;
    line-height: normal;
    letter-spacing: 0.972px;

    span:first-child {
        color: $base-yellow;
    }
  
    span:last-child {
        color: $base-orange;
    }
}
.quiz__question-container {
    border: 3px solid $base-yellow;
    background: $base-gray;
    width: 884px;
    min-height: 100px;
    margin: 0 auto;
    display: flex;

    margin-bottom: 30px;

    p {
        @include stroke(2px, #000);
        
        align-self: center;
        color: $base-yellow;
        text-align: center;
        font-family: $base-font;
        font-size: 30px;
        font-style: normal;
        font-weight: 400;
        line-height: normal;
        letter-spacing: 2.01px;
    }
}
.quiz__answers-container {
    display: grid;
    grid-template-columns: 1fr 1fr;
    grid-template-rows: 1fr 1fr;
    column-gap: 84px;
    row-gap: 40px;

    margin-bottom: 40px;
}
.quiz__answer {
    border: 3px solid $base-yellow;
    background-color: $base-gray;
    min-height: 70px;
    display: flex;
    justify-content: center;
    align-items: center;

    span {
        @include stroke(2px, #000);

        color: $base-yellow;
        text-align: center;
        font-family: $base-font;
        font-size: 30px;
        font-style: normal;
        font-weight: 400;
        line-height: normal;
        letter-spacing: 2.01px;
    }
}
.quiz__counter {
    display: flex;
    justify-content: space-between;
}
.quiz__question-number {
    width: 140px;
    height: 60px;
    background-color: $base-gray;
    border: 3px solid $base-yellow;
    display: flex;
    align-items: center;
    justify-content: center;

    span {
        display: block;
        width: 96px;

        color: $base-yellow;
        text-align: center;
        font-family: $base-font;
        font-size: 20px;
        font-style: normal;
        font-weight: 400;
        line-height: normal;
        letter-spacing: 1.34px;
    }
}
.quiz__button {
    border: 3px solid $base-yellow;
    background: $base-gray;

    color: $base-yellow;
    text-align: center;
    font-family: $base-font;
    font-size: 26px;
    font-style: normal;
    font-weight: 400;
    line-height: normal;
    letter-spacing: 1.742px;

    margin: 0 auto;
    min-width: 884px;    
    padding: 2px 0px 7px 0px;

    margin-top: 20px;
}
</style>