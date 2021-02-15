<template>
    <div class="landing__container">
        <Header/>
        <main class="main">
            <router-view />
        </main>
    </div>
</template>

<script>
    import Header from "../../components/app/LandingHeader";
    export default {
        name: "LandingLayout",
        components: {
            Header
        }
    }
</script>

<style lang="scss" scoped>
    @import 'LandingLayout.scss';
</style>
