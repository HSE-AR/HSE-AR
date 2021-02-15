<template>
    <div class="container sign-layout">
        <div class="form__container">
            <main class="main form__content">
                <router-view />
            </main>
        </div>
    </div>
</template>

<script>
    export default {
        name: "SignLayout"
    }
</script>

<style lang="scss" scoped>
    @import 'SignLayout.scss';
</style>