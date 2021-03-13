<template>
  <div>
    <div>
      <h2>BuildingInfo</h2>
      <h1>{{this.$route.query.buildingId}}</h1>
      <h1 v-if="building_info">{{building_info}}</h1>
      <button @click="isHidden = !isHidden">{{ isHidden ? 'Show Floors' : 'Hide Floors' }}</button>

      <div class="building__info" v-if="!isHidden">
      <ul>
          <li
                  v-for="floor in building_info.floors"
                  :key="floor.id"
          >
              <h1>{{floor.title}}</h1>
              <router-link :to="{path: `/admin/editor/`, query: {floorId: floor.id}}" class="editFloor">Edit Floor</router-link>
              <button @click="deleteFloor(floor.id)" class="floor__deletion">
                <svg xmlns="http://www.w3.org/2000/svg" width="14" height="14" viewBox="0 0 24 24"><path d="M24 20.188l-8.315-8.209 8.2-8.282-3.697-3.697-8.212 8.318-8.31-8.203-3.666 3.666 8.321 8.24-8.206 8.313 3.666 3.666 8.237-8.318 8.285 8.203z"/></svg>
              </button>

          </li>
      </ul>

      </div>
      <div class="floor__creation">
        <form class="floor__creation__form" @submit.prevent="createFloor">
                        <span class="form__input">
                          <input v-model="title" type="text" id="title" class="input" placeholder="Title..." required>
                        </span>
                        <span class="form__input">
                          <input v-model="number" type="number" id="number" class="input" placeholder="Number..." required>
                        </span>
                        <span class="form__input">
                          <input v-model="pointCloudId" type="text" id="pointCloudId" class="input" placeholder="pointCloudId..." required>
                        </span>
                        <span class="form__input">
                          <input type="file" ref="floorPlanImg" id="floorPlanImg" @change="convertImage" accept="image/*" class="input" placeholder="floorPlanImg..." required>
                        </span>
          <button class="building__creation__submit">Create Floor</button>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
    import {mapGetters} from "vuex";

    export default {
        name: "BuildingInfo",
        data() {
          return {
              title: null,
              number: null,
              pointCloudId: null,
              floorPlanImg: null,
              buildingId: this.$route.query.buildingId,
              isHidden: true,
          }
        },
        computed: {
            ...mapGetters(['building_info', 'buildings'])
        },

        async created() {
          if(localStorage.getItem('building_info') === null) {
              this.$Progress.start()
                  await this.$store.dispatch('getBuildingInfo', this.buildingId)
                      .then(response => {
                          console.log(response)
                          this.$Progress.finish()
                      })
                      .catch(err => {
                          console.log(err)
                          this.$Progress.fail()

                      })
          } else if (this.buildingId !== JSON.parse(localStorage.getItem('building_info')).id) {
              this.$Progress.start()
              await this.$store.dispatch('getBuildingInfo', this.buildingId)
                  .then(response => {
                      console.log(response)
                      this.$Progress.finish()
                  })
                  .catch(err => {
                      console.log(err)
                      this.$Progress.fail()

                  })
          } else return false


        },
        methods: {
            convertImage(event) {
              let input = event.target
              if (input.files && input.files[0]) {
                  let reader = new FileReader()
                  reader.onload = (e) => {
                      this.floorPlanImg = e.target.result
                  }
                  reader.readAsDataURL(input.files[0]);
              }
            },
            async createFloor() {
                this.$Progress.start()
                const data = {
                    title: this.title,
                    number: this.number,
                    pointCloudId: this.pointCloudId,
                    floorPlanImg: this.floorPlanImg.toString(),
                    buildingId: this.buildingId,
                }
                await this.$store.dispatch('createFloor', data )
                  .then(response => {
                      console.log(response)
                      this.$Progress.finish()
                      this.title = ""
                      this.number = ""
                      this.pointCloudId = ""
                      this.floorPlanImg = ""
                  })
                  .catch(err => {
                      console.log(err)
                      this.$Progress.fail()
                  })
            },
            async deleteFloor(floorId) {
                this.$Progress.start()
                const data = {
                    buildingId: this.buildingId,
                    floorId: floorId
                }
                await this.$store.dispatch('deleteFloor', data)
                    .then(response => {
                        console.log(response)
                        this.$Progress.finish()
                    })
                    .catch(err => {
                        console.log(err)
                        this.$Progress.fail()
                    })
            }
        },
    }
</script>

<style lang="scss" scoped>
  @import 'Buildings.scss';
</style>