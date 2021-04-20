<template>
  <div>
    <div class="buildings-container">
      <div v-if="building_info.floors.length !== 0" class="building__info">
        <hooper :vertical="true" style="height: 400px" :itemsToShow="1.5" :centerMode="true">
          <slide
                  v-for="floor in building_info.floors"
                  :key="floor.id"
          >
            <h1>{{floor.title}}</h1>
            <router-link :to="{path: `/admin/editor/`, query: {floorId: floor.id}}" class="editFloor">Edit Floor</router-link>
            <button @click="deleteFloor(floor.id)" class="floor__deletion">
              <svg xmlns="http://www.w3.org/2000/svg" width="14" height="14" viewBox="0 0 24 24"><path d="M24 20.188l-8.315-8.209 8.2-8.282-3.697-3.697-8.212 8.318-8.31-8.203-3.666 3.666 8.321 8.24-8.206 8.313 3.666 3.666 8.237-8.318 8.285 8.203z"/></svg>
            </button>

          </slide>
        </hooper>
        <button
                style="margin-top: 40px;"
                type="button"
                class="btn-create"
                @click="showModal"
        >
          Create another floor
        </button>
      </div>
      <div v-else class="buildings-not-created">
        <span>You haven't created any floors yet</span>
        <button
                style="margin-top: 40px;"
                type="button"
                class="btn-create"
                @click="showModal"
        >
          Add floor
        </button>
      </div>
      <ModalWindow
              v-show="isModalVisible"
              @close="closeModal"
      >
        <template v-slot:header>
          New Floor
        </template>

        <template v-slot:body>
          <form class="floor__creation__form" @submit.prevent="createFloor">
          <span class="form__input">
            <input v-model="title" type="text" id="title" class="input" placeholder="Title..." required>
          </span>
          <span class="form__input">
            <input v-model="number" type="number" id="number" class="input" placeholder="Number..." required>
          </span>
            <span class="form__input">
            <select v-model="pointCloudId" type="text" id="pointCloudId" class="select_point" required>
              <option selected="selected" v-for="pointcloud in pointclouds" :value="pointcloud.id">{{pointcloud.name}}</option>
            </select>
            </span>
            <div style="height: 40px;" class="form__input">
              <input type="file" ref="floorPlanImg" id="floorPlanImg" @change="convertImage" accept="image/*" class="input" placeholder="floorPlanImg..." required>
            </div>
          </form>
          <div class="image-wrapper">
            <img src="#" alt="image">
          </div>
        </template>
        <template v-slot:footer>
          <button class="building__creation__submit" @click="createFloor">Save</button>
        </template>
      </ModalWindow>
    </div>
  </div>
</template>

<script>
    import {mapGetters} from "vuex";
    import { Hooper, Slide } from 'hooper';
    import 'hooper/dist/hooper.css';
    import ModalWindow from "../../components/app/ModalWindow"

    export default {
        name: "BuildingInfo",
        components: {
            ModalWindow,
            Hooper,
            Slide
        },
        data() {
          return {
              title: null,
              number: null,
              pointCloudId: null,
              floorPlanImg: null,
              buildingId: this.$route.query.buildingId,
              isModalVisible: false,
          }
        },
        computed: {
            ...mapGetters(['building_info', 'buildings', 'pointclouds'])
        },

        async created() {
          this.$Progress.start()
          await this.$store.dispatch('getPointClouds')
              .then(response => {
                  console.log(response)
                  this.$Progress.finish()
              })
              .catch(err => {
                  console.log(err)
                  this.$Progress.fail()

              })
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
            showModal() {
                this.isModalVisible = true;
            },
            closeModal() {
                this.isModalVisible = false;
            },
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