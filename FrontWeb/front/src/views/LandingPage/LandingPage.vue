<template>
    <section class="intro">

    </section>
</template>

<script>

import router from '../../router/router'

export default {
  name: 'Home',
  methods:{

    signInAdmin(){
      router.push('/signin/admin')
    },

    signUpAdmin(){
      router.push('/signup/admin')
    },

    getARWeb(){
        router.push('/arweb/') 
    }

  }
}
</script>

<style lang="scss" scoped>
    @import 'LandingPage.scss';
</style>


