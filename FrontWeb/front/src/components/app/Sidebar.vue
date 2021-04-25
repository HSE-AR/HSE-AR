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
                        :exact="link.exact"
                >
                    <a href="#" class="sidenav__link">{{link.title}}</a>

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
                {title: 'Maps', url: '/admin/maps'},
                {title: 'PointClouds', url: '/admin/pcd'}

            ]
        })
    }
</script>

<style lang="scss" scoped>
    @import 'Sidebar.scss';
</style>