<template>
  <div >
    <h1> Админка</h1>
    <h3 v-if="user">Welcome, admin {{user.name}}</h3>
    <h2 v-if="user">
      <a @click="handleClick">Logout</a>
    </h2>
    <button v-if="user" @click="openEditor()">Открыть редактор</button>
    <router-view/>
  </div>
</template>

<script>

import router from '../router/router'
import {mapGetters} from 'vuex'

export default {
  name: 'Home',
  methods: {

    handleClick() {
      localStorage.removeItem('token')
      this.$store.dispatch('user', null)
      this.$router.push('/signin/admin')
    },

    openEditor(){
      console.log("редактор открыт")
      router.push('/adminka/editor')
    }
  },
  computed: {
    ...mapGetters(['user'])
  }
}
</script>
