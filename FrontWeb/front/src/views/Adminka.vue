<template>
  <div>
    <h1>Админка</h1>
    <h3 v-if="user">Hi, {{user.name}}</h3>
    <h3 v-if="!user">You are not logged in</h3>
    <router-view/>
  </div>
</template>

<script>

import router from '../router/router'
import {mapGetters} from 'vuex'
import axios from 'axios'

export default {
  name: 'Adminka',
  data() {
    return {
        user: null
    }
  },

  async created() {
      await axios.get('account')
        .then(response => {
            console.log(response)
            this.user = response.data
        })
  },
  methods: {

  },
  computed: {
  },
}
</script>
