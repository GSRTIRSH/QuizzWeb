<template>
    <div class="main">
        <div class="main__wrapper">
            <div class="main__contant">
                <h1 class="main__title">
                    <span>Ultimate</span>
                    <span>Quiz Adventure for Programming Enthusiasts</span>
                </h1>
                <p class="main__subtitle">
                    Embark on a Quest of Coding Knowledge with Codebusters Quiz
                    Game!
                </p>
                <button @click="scrollToQuizSelect" class="main__button">let’s try</button>
            </div>
            <img
                src="@/assets/images/main-image.gif"
                alt=""
                class="main__image"
            />
        </div>
    </div>
</template>

<script setup lang="ts">
const scrollToQuizSelect = () => {
    document.getElementById('scrollPoint')?.scrollIntoView({ behavior: 'smooth' });
}
</script>
<style lang="scss" scoped>
.main {
    display: flex;
    align-items: center;
}
.main__wrapper {
    display: flex;
    justify-content: space-between;
    height: 383px;
    width: 100%;
    margin: 120px auto;
}
.main__contant {
    align-self: center;
}
.main__title {
    @include stroke(2px, #000);

    font-family: $base-title-font;
    font-size: 30px;
    font-style: normal;
    font-weight: 400;
    line-height: normal;
    letter-spacing: 2.4px;

    margin-bottom: 18px;

    span:first-child {
        color: $base-yellow;
    }

    span:last-child {
        color: $base-orange;
    }
}
.main__subtitle {
    @include stroke(1px, #000);
    color: $base-yellow;
    font-family: $base-title-font;
    font-size: 26px;
    font-style: normal;
    font-weight: 400;
    line-height: normal;

    margin-bottom: 66px;
}
.main__button {
    padding: 10px 108px 14px 108px;
    border: 2px solid $base-yellow;
    background: #322f32;
    color: $base-yellow;
    font-family: $base-title-font;
    font-size: 27px;
    font-style: normal;
    font-weight: 400;
    line-height: normal;
    letter-spacing: 0.729px;
}

.main__image {
    margin-right: -100px;
}
</style>
