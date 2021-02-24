<template>
  <div id="app">
    <component v-if="this.$route.meta.layout" :is="layout">
      <router-view :user="user" />
    </component>
    <component v-else>
      <Editor />
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

