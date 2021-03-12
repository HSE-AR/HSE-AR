<template>
  <div id="app">
    <vue-progress-bar></vue-progress-bar>
    <component :is="layout">
      <router-view />
    </component>
    <component v-if="!this.$route.meta.layout">
      <Editor/>
    </component>

  </div>
</template>

<script>
  import LandingLayout from "./layouts/LandingLayout/LandingLayout"
  import SignLayout from "./layouts/SignLayout/SignLayout";
  import MainLayout from "./layouts/MainLayout/MainLayout";
  import Editor from "./views/Editor/Editor";
  import axios from "axios";
  export default {
    name: 'App',

    computed: {
      layout() {
        return (this.$route.meta.layout || 'sign') + '-layout'
      },
    },

    components: {
      SignLayout, MainLayout, LandingLayout, Editor
    }



  }
</script>

<style lang="css">
  @import 'assets/css/reset.css';
</style>

