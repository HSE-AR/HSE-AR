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

<!--                        <div class="image-preview" v-if="floorPlanImg.length > 0">-->
<!--                          <img class="preview" :src="floorPlanImg">-->
<!--                        </div>-->
                        <span class="form__input">
                          <input v-model="buildingId" type="text" id="buildingId" class="input" placeholder="buildingId..." required>
                        </span>
          <button class="building__creation__submit">Create Building</button>
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
              buildingId: null,
              isHidden: true,
          }
        },
        computed: {
            ...mapGetters(['building_info', 'buildings'])
        },
        props: (route) => ({ query: route.query.buildingId }),
        async created() {
          await this.$store.dispatch('getBuildingInfo', this.$route.query.buildingId)
            .then(response => {
                console.log(response)
            })
            .catch(err => console.log(err))


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
                const data = {
                    title: this.title,
                    number: this.number,
                    pointCloudId: this.pointCloudId,
                    floorPlanImg: this.floorPlanImg.toString(),
                    buildingId: this.buildingId,
                }
                await this.$store.dispatch('createFloor', data )
                  .then(response => console.log(response))
                  .catch(err => console.log(err))
            }
        },
    }
</script>

<style lang="scss" scoped>
  @import 'Buildings.scss';
</style>