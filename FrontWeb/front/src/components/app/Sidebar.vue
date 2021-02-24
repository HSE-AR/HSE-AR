<template>
    <div class="app-sidenav">
        <div class="sidenav__wrapper">
            <ul class="sidenav__content">
                <router-link
                    v-for="link in links"
                    :key="link.url"
                    tag="li"
                    active-class="active"
                    :to="link.url"
                    class="sidenav__item"
                >
                    <a class="sidenav__link" href="">{{link.title}}</a>
                </router-link>
            </ul>
        </div>
    </div>
</template>

<script>
    export default {
        name: "SidebarAdmin",
        data: () => ({
            links: [
                {title: 'Buildings', url: '/adminka/buildings'},
                {title: 'About', url: '/adminka/about'},
                {title: 'Help', url: '/adminka/help'},
            ]
        })
    }
</script>

<style lang="scss" scoped>
    @import 'Sidebar.scss';
</style>