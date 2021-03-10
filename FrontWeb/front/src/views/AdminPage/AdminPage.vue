<template>
  <div>
    <h1>Админка</h1>
    <hr>
    <h2>USER:</h2>
    <hr>
    <h1 v-if="user">Hello, {{user.name}} </h1>
    <hr>
    <h2>YOUR COMPANY:</h2>
    <hr>
    <h1 v-if="company_actions">{{company_actions}}</h1>
    <hr>
    <router-view/>
  </div>
</template>

<script>


import {mapGetters} from 'vuex'

export default {
  name: 'AdminPage',
  computed: {
    ...mapGetters(['user', 'company_actions'])
  },
}
</script>

<style lang="scss" scoped>
  @import 'AdminPage.scss';
</style>
