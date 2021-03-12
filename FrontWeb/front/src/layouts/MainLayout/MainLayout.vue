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
    export default {
        name: "MainLayout",
        components: {
            Header, Sidebar
        },
        async mounted() {

            if(localStorage.getItem('user') === null) {
                this.$Progress.start()
                await this.$store.dispatch('getUserFromToken')
                        .then(response => {
                            console.log(response)
                            this.$Progress.finish()
                        })
                        .catch(err => {
                            console.log(err)
                            this.$Progress.fail()
                        })
            }
            if(localStorage.getItem('company_actions') === null) {
                await this.$store.dispatch('getCompanyFromToken')
                        .then(response => {
                            console.log(response)
                            this.$Progress.finish()
                        })
                        .catch(err => {
                            console.log(err)
                            this.$Progress.fail()
                        })
            }

            if(localStorage.getItem('buildings') === null) {
                await this.$store.dispatch('getBuildingsFromUser')
                        .then(response => {
                            console.log(response)
                            this.$Progress.finish()
                        })
                        .catch(err => {
                            console.log(err)
                            this.$Progress.fail()
                        })
            }




        },

    }
</script>

<style lang="scss" scoped>
    @import 'MainLayout.scss';
</style>