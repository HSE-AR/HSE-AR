<template>
  <div id="app">
    <component :is="layout">
      <router-view />
    </component>

  </div>
</template>

<script>
  import LandingLayout from "./layouts/LandingLayout/LandingLayout"
  import SignLayout from "./layouts/SignLayout/SignLayout";
  import MainLayout from "./layouts/MainLayout/MainLayout";
  export default {
    name: 'App',
    computed: {
      layout() {
        return (this.$route.meta.layout || 'sign') + '-layout'
      }
    },
    components: {
      SignLayout, MainLayout, LandingLayout
    }


    // async created() {
    //   const response = await axios.get('user')
    //   await this.$store.dispatch('user', response.data)
    // },
  }
</script>

<style lang="css">
  @import 'assets/css/reset.css';
</style>

