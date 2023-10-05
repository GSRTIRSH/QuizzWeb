<template>
    <footer class="footer">
        <div class="footer__wrapper">
            <div class="footer__signature">
                <span>created by </span><span>khanov iskander</span>
            </div>
            <div class="footer__logo-container">
                <img src="@/assets/icons/VK-logo.svg" alt="VK">
                <img src="@/assets/icons/TG-logo.svg" alt="Telegramm">
            </div>
        </div>
    </footer>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
    setup() {
        return {};
    },
});
</script>

<style lang="scss" scoped>
.footer {
    min-height: 71px;
}
.footer__wrapper {
    display: flex;
    justify-content: space-between;
    align-items: center;
}
.footer__signature {
    font-family: $base-title-font;
    font-size: 20px;
    font-style: normal;
    font-weight: 400;
    line-height: normal;
    letter-spacing: 0.54px;

    span:first-child {
        color: $base-orange;
    }
  
    span:last-child {
        color: $base-yellow;
    }
}
.footer__logo-container {
    display: flex;

    img:first-child {
        margin-right: 25px;
    }
}
</style>
