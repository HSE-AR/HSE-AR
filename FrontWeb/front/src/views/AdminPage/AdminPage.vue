<template>
  <div>
    <div class="admin__heading_container">
      <h1 class="admin__heading">Административная панель</h1>
      <div class="admin__heading_user">
        <h1 v-if="user">Hello, {{user.name}} </h1>
      </div>
    </div>
    <div class="admin__company">
      <h1>Ваша компания:</h1>
      <p>{{company_actions}}</p>
    </div>
    <router-view/>
  </div>
</template>

<script>


import {mapGetters} from 'vuex'

export default {
  name: 'AdminPage',
  computed: {
    ...mapGetters({
        user: 'user',
        company_actions: 'company_actions'
    })
  },
}
</script>

<style lang="scss" scoped>
  @import 'AdminPage.scss';
</style>
