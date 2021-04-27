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
  import Spinner from 'vue-simple-spinner'
  import axios from "axios";
  import {mapGetters} from "vuex";
  export default {
    name: 'App',

    computed: {
      layout() {
        return (this.$route.meta.layout || 'sign') + '-layout'
      },

    },

    components: {
      SignLayout, MainLayout, LandingLayout, Editor,
    }



  }
</script>

<style lang="css">
  @import 'assets/css/reset.css';

  .myActionButton{
   cursor: pointer;
   text-align: center;
    padding: 5px;
    border: 0px solid black;
    border-radius: 10px;
  }

  .myActionButton:hover{
    background-color: rgba(0,0,0,0.5);

  }

  div#map a,
  div#map button,
  div.gm-bundled-control div.gmnoprint,
  div#map .gm-svpc{
    display: none !important;
  }

  .hooper-slide{
    border-radius: 15px;
    border: 1px solid white;
    display: flex;
    justify-content: center;
    align-items: center;
    flex-flow: column;
  }

  input:-webkit-autofill {
    -webkit-box-shadow: inset 0 0 0 50px white; /* цвет вашего фона */
    -webkit-text-fill-color: black; /* цвет текста */
  }

  input:-webkit-autofill:focus {
    -webkit-box-shadow: 0 0 5px 0    blue,   /* ваш box-shadow для :focus */
    inset 0 0 0   50px white;  /* цвет вашего фона */
    -webkit-text-fill-color: black;          /* цвет текста */
  }

</style>

