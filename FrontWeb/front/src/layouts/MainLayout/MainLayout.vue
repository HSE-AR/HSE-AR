<template>
    <div class="app-main-layout">
        <Header />
        <div class="wrapper">
            <Sidebar />
            <main class="app-content">
                <div class="app-page">
                    <router-view />
                </div>
            </main>
        </div>

    </div>
</template>

<script>
    import Header from '../../components/app/Header'
    import Sidebar from '../../components/app/Sidebar'
    import axios from "axios";
    export default {
        name: "AdminLayout",
        components: {
            Header, Sidebar
        },

    }
</script>

<style lang="scss" scoped>
    @import 'MainLayout.scss';
</style>