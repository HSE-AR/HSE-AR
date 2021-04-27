<template>
  <div>


    <div class="buildings-container">
      <div v-if="loadingStatus" class="loading-div">
        <vue-spinner text-fg-color="white" line-fg-color="#B55CFE" message="Loading floors..."/>
      </div>
      <div v-else-if="building_info.floors.length !== 0" class="building__info">
        <div style="font-size: 20px; margin: 20px; text-align: center">{{building_info.title}}</div>
        <button
                type="button"
                class="btn-create"
                @click="showModal"
        >
          Create another floor
        </button>
        <hooper  :vertical="true" style="width: 240px; height: 600px; margin-top: 20px"  :itemsToShow=2>
          <slide
                  v-for="floor in building_info.floors"
                  :key="floor.id"
                  class="floor-card"

          >
            <div class="floor-image-wrapper">
              <img style="border-top-left-radius: 15px; border-top-right-radius: 15px; width: 100%; height: 100%;" :src="$store.state.port + floor.floorPlanImage">
            </div>
            <div class="floor_content">
              <h1>{{floor.title}}</h1>
              <p>Floor number: {{floor.number}}</p>
              <div class="building-actions">
                <button @click="deleteFloor(floor.id)" class="floor__deletion">
                  <svg width="19" height="18" viewBox="0 0 19 18" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <g clip-path="url(#clip0)">
                      <path d="M14.25 17.4375H4.75C4.27758 17.4375 3.82452 17.2597 3.49047 16.9432C3.15642 16.6268 2.96875 16.1976 2.96875 15.75V5.0625C2.96875 4.91332 3.03131 4.77024 3.14266 4.66475C3.25401 4.55926 3.40503 4.5 3.5625 4.5C3.71997 4.5 3.87099 4.55926 3.98234 4.66475C4.09369 4.77024 4.15625 4.91332 4.15625 5.0625V15.75C4.15625 15.8992 4.21881 16.0423 4.33016 16.1477C4.44151 16.2532 4.59253 16.3125 4.75 16.3125H14.25C14.4075 16.3125 14.5585 16.2532 14.6698 16.1477C14.7812 16.0423 14.8438 15.8992 14.8438 15.75V5.0625C14.8438 4.91332 14.9063 4.77024 15.0177 4.66475C15.129 4.55926 15.28 4.5 15.4375 4.5C15.595 4.5 15.746 4.55926 15.8573 4.66475C15.9687 4.77024 16.0312 4.91332 16.0312 5.0625V15.75C16.0312 16.1976 15.8436 16.6268 15.5095 16.9432C15.1755 17.2597 14.7224 17.4375 14.25 17.4375Z" fill="white"/>
                      <path d="M16.625 3.9375H2.375C2.21753 3.9375 2.06651 3.87824 1.95516 3.77275C1.84381 3.66726 1.78125 3.52418 1.78125 3.375C1.78125 3.22582 1.84381 3.08274 1.95516 2.97725C2.06651 2.87176 2.21753 2.8125 2.375 2.8125H16.625C16.7825 2.8125 16.9335 2.87176 17.0448 2.97725C17.1562 3.08274 17.2188 3.22582 17.2188 3.375C17.2188 3.52418 17.1562 3.66726 17.0448 3.77275C16.9335 3.87824 16.7825 3.9375 16.625 3.9375Z" fill="white"/>
                      <path d="M11.875 3.9375C11.7175 3.9375 11.5665 3.87824 11.4552 3.77275C11.3438 3.66726 11.2812 3.52418 11.2812 3.375V1.6875H7.71875V3.375C7.71875 3.52418 7.65619 3.66726 7.54484 3.77275C7.43349 3.87824 7.28247 3.9375 7.125 3.9375C6.96753 3.9375 6.81651 3.87824 6.70516 3.77275C6.59381 3.66726 6.53125 3.52418 6.53125 3.375V1.125C6.53125 0.975816 6.59381 0.832742 6.70516 0.727252C6.81651 0.621763 6.96753 0.5625 7.125 0.5625H11.875C12.0325 0.5625 12.1835 0.621763 12.2948 0.727252C12.4062 0.832742 12.4688 0.975816 12.4688 1.125V3.375C12.4688 3.52418 12.4062 3.66726 12.2948 3.77275C12.1835 3.87824 12.0325 3.9375 11.875 3.9375Z" fill="white"/>
                      <path d="M9.5 14.625C9.34253 14.625 9.19151 14.5657 9.08016 14.4602C8.96881 14.3548 8.90625 14.2117 8.90625 14.0625V6.1875C8.90625 6.03832 8.96881 5.89524 9.08016 5.78975C9.19151 5.68426 9.34253 5.625 9.5 5.625C9.65747 5.625 9.8085 5.68426 9.91985 5.78975C10.0312 5.89524 10.0938 6.03832 10.0938 6.1875V14.0625C10.0938 14.2117 10.0312 14.3548 9.91985 14.4602C9.8085 14.5657 9.65747 14.625 9.5 14.625Z" fill="white"/>
                      <path d="M12.4688 13.5C12.3113 13.5 12.1603 13.4407 12.0489 13.3352C11.9376 13.2298 11.875 13.0867 11.875 12.9375V7.3125C11.875 7.16332 11.9376 7.02024 12.0489 6.91475C12.1603 6.80926 12.3113 6.75 12.4688 6.75C12.6262 6.75 12.7772 6.80926 12.8886 6.91475C12.9999 7.02024 13.0625 7.16332 13.0625 7.3125V12.9375C13.0625 13.0867 12.9999 13.2298 12.8886 13.3352C12.7772 13.4407 12.6262 13.5 12.4688 13.5Z" fill="white"/>
                      <path d="M6.53125 13.5C6.37378 13.5 6.22276 13.4407 6.11141 13.3352C6.00006 13.2298 5.9375 13.0867 5.9375 12.9375V7.3125C5.9375 7.16332 6.00006 7.02024 6.11141 6.91475C6.22276 6.80926 6.37378 6.75 6.53125 6.75C6.68872 6.75 6.83974 6.80926 6.95109 6.91475C7.06244 7.02024 7.125 7.16332 7.125 7.3125V12.9375C7.125 13.0867 7.06244 13.2298 6.95109 13.3352C6.83974 13.4407 6.68872 13.5 6.53125 13.5Z" fill="white"/>
                      <line x1="7.62012" y1="0.720215" x2="7.6201" y2="-227.52" stroke="white" stroke-width="3"/>
                    </g>
                    <defs>
                      <clipPath id="clip0">
                        <rect width="19" height="18" fill="white"/>
                      </clipPath>
                    </defs>
                  </svg>
                </button>
              </div>
              <router-link :to="{path: `/admin/editor/`, query: {floorId: floor.id}}" class="editFloor">Open Editor</router-link>
            </div>


          </slide>
        </hooper>
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
              <option value="" hidden>Select pointcloud</option>
              <option :value="null" hidden>Without pointcloud</option>
              <option selected="selected" v-for="pointcloud in pointclouds" :value="pointcloud.id">{{pointcloud.name}}</option>
            </select>
            </span>
            <div style="height: 40px;" class="form__input">
              <input type="file" ref="floorPlanImg" id="floorPlanImg" @change="convertImage" accept="image/*" class="input" placeholder="floorPlanImg..." required>
            </div>
          </form>
          <div class="image-preview" v-if="floorPlanImg.length === 0">
            <img style="max-width: 100%; max-height: 100%;" class="preview" src="@/assets/unnamed.jpg">
          </div>
          <div class="image-preview" v-if="floorPlanImg.length > 0">
            <img style="max-width: 100%; max-height: 100%;" class="preview" :src="floorPlanImg">
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
    import Spinner from "vue-simple-spinner";
    import Swal from "sweetalert2";

    export default {
        name: "BuildingInfo",
        components: {
            ModalWindow,
            Hooper,
            Slide,
            vueSpinner: Spinner

        },
        data() {
          return {
              title: null,
              number: null,
              pointCloudId: null,
              floorPlanImg: "",
              buildingId: this.$route.query.buildingId,
              isModalVisible: false,
          }
        },
        computed: {
            ...mapGetters(['building_info', 'buildings', 'pointclouds', 'loadingStatus'])
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
                    this.isModalVisible = false;
                      Swal.fire(
                          'Success',
                          'Floor is created successfully!',
                          'success'
                      )
                  })
                  .catch(err => {
                      console.log(err)
                      this.$Progress.fail()
                    this.isModalVisible = false;
                      Swal.fire(
                          'Error',
                          'Please try one more time',
                          'error'
                      )
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
                        Swal.fire(
                            'Success',
                            'Floor is deleted successfully!',
                            'success'
                        )
                    })
                    .catch(err => {
                        console.log(err)
                        this.$Progress.fail()
                        Swal.fire(
                            'Error',
                            'Please try one more time',
                            'error'
                        )
                    })
            }
        },
    }
</script>

<style lang="scss" scoped>
  @import 'Buildings.scss';
</style>