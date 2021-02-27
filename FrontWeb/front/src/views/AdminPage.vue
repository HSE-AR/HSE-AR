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
    <h2>BUILDINGS:</h2>
    <hr>
    <h1 v-if="buildings">{{buildings}}</h1>
    <hr>
    <h2>BUILDING_INFO:</h2>
    <hr>
    <button @click="isHidden = !isHidden">{{ isHidden ? 'Show' : 'Hide' }}</button>
    <div class="building__info" v-if="!isHidden">
      <ul>
        <li
          v-for="floor in building_info.buildingInfo.floors"
          :key="floor.id"
        >
          <h1>{{floor.title}}</h1>
          <router-link :to="{path: `/adminpage/editor/`, query: {floorId: floor.id}}" class="editFloor">Edit Floor</router-link>
        </li>
      </ul>

    </div>

    <router-view/>
  </div>
</template>

<script>


import {mapGetters} from 'vuex'

export default {
  name: 'AdminPage',
  data() {
    return {
        isHidden: true,
    }
  },
  computed: {
    ...mapGetters(['user', 'company_actions', 'buildings', 'building_info'])
  },
  async created() {
      await this.$store.dispatch('getUserFromToken')
        .then(response => {
            console.log(response)
        })
      await this.$store.dispatch('getCompanyFromToken')
        .then(response => {
            console.log(response)
        })
      await this.$store.dispatch('getBuildingsFromUser')
        .then(response => {
            console.log(response)
            let buildingId = response.data.buildings[0].id
            this.$store.dispatch('getBuildingInfo', buildingId)
                .then(response => {
                    console.log(response)
                })
        })
        .catch(err => console.log(err))

  },
  methods: {
  },


}
</script>
