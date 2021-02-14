<template>
    <nav class="header__nav">
        <ul class="header__list">
            <router-link
                tag="li"
                to="/signin/admin"
                exact
                class="header__item"
            >
                <a class="header__link">To Admin Panel</a>
            </router-link>
            <router-link
                    tag="li"
                    to="/signup/admin"
                    exact
                    class="header__item"
            >
                <a class="header__link">Become Admin</a>
            </router-link>
            <router-link
                    tag="li"
                    to="#"
                    exact
                    class="header__item"
            >
                <a class="header__link">AR Page</a>
            </router-link>
            <router-link
                    tag="li"
                    to="#"
                    exact
                    class="header__item"
            >
                <a class="header__link">About us</a>
            </router-link>
        </ul>
    </nav>
</template>

<script>
    export default {
        name: "Navbar"
    }
</script>

<style lang="scss" scoped>
    @import 'LandingNavbar.scss';
</style>
