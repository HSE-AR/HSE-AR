<template>
    <div class="buildings-container">
        <div v-if="buildings.length !== 0">
          <div class="buildings-length-container">
            <span>You have already created {{buildings.length}} map(s)</span>
          </div>
          <div class="buildings_wrapper">
            <BuildingsSlider :buildings="buildings"/>
            <button
                    style="margin-top: 20px;"
                    type="button"
                    class="btn-create"
                    @click="showModal"
            >
              Create another map
            </button>
          </div>
        </div>

        <div v-else>
          <div class="buildings-not-created">
            <span>You haven't created any maps yet</span>
            <button
                    style="margin-top: 40px;"
                    type="button"
                    class="btn-create"
                    @click="showModal"
            >
              Create map
            </button>
          </div>

        </div>
      <ModalWindow
              v-show="isModalVisible"
              @close="closeModal"
      >
        <template v-slot:header>
          New Map
        </template>

        <template v-slot:body>
          <div class="form__container">
            <div class="form__input">
              <input v-model="title" type="text" id="title" class="input" placeholder="Map name*" required>
            </div>
            <div class="form__input">
              <input v-model="address" type="text" id="address" class="input" placeholder="Map address*" required>
            </div>
            <span class="form__input">
              <input type="file" ref="buildingImage" id="buildingImage" @change="convertImage" accept="image/*" class="input" placeholder="Image*" required>
            </span>
          </div>
          <gmap-map
                  id="map"
                  ref="Map"
                  :center="center"
                  :zoom="2"
                  style="width: 60%; height: 300px"
                  @click="onMapClick"
          >
            <gmap-marker
                    v-for="m in markers"
                    :key="m.id"
                    :position="m.position"
                    :clickable="true"
                    :draggable="true"
                    @click="onMarkerClick"
            />

          </gmap-map>
        </template>
        <template v-slot:footer>
          <button class="building__creation__submit" @click="createBuilding">Save</button>
        </template>
      </ModalWindow>
      </div>
</template>

<script>
    import { Glide, GlideSlide } from 'vue-glide-js'
    import {mapGetters} from "vuex";
    import {gmapApi} from 'vue2-google-maps'
    import Swal from 'sweetalert2'
    import Header from "../../components/app/Header";
    import Sidebar from "../../components/app/Sidebar";
    import ModalWindow from "../../components/app/ModalWindow"
    import BuildingsSlider from "./BuildingsSlider";

    export default {

        name: 'Buildings',
        components: {
            ModalWindow,
            BuildingsSlider,
            [Glide.name]: Glide,
            [GlideSlide.name]: GlideSlide
        },
        data() {
            return {
              center: {lat: 10.0, lng: 10.0},
              markers: [],
              map:null,
              title: null,
              address: null,
              isHidden: true,
              buildingImage: null,
              isModalVisible: false,
            }
        },
      computed: {
        google: gmapApi,
        ...mapGetters(['buildings']),
        },
      async created() {
          this.$Progress.start()
          await this.$store.dispatch('getBuildingsFromUser')
              .then(response => {
                  console.log(response)
                  this.$Progress.finish()
              })
              .catch(err => {
                  console.log(err)
                  this.$Progress.fail()

              })
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
          showModal() {
              this.isModalVisible = true;
          },
          closeModal() {
              this.isModalVisible = false;
          },
          onMapClick(e) {
            this.markers=[{
              id: 1 + Math.max(0, ...this.markers.map(n => n.id)),
              position: e.latLng}]

          },
          onMarkerClick(e) {
            this.$refs.Map.panTo(e.latLng);
          },

          async createBuilding() {
              if(this.markers.length === 0){
                  Swal.fire('Please put a marker on the map')
                  return
              }
              this.$Progress.start()
              const data = {
                  title: this.title,
                  address: this.address,
                  "latitude": this.markers[0].position.lat(),
                  "longitude": this.markers[0].position.lng()
              }
              await this.$store.dispatch('createBuilding', data)
                  .then(response => {
                      console.log(response)
                      this.$Progress.finish()
                      this.title = ""
                      this.address = ""
                      this.coordinate = ""
                  })
                  .catch(err => {
                      console.log(err)
                      this.$Progress.fail()
                  })
          },


        }

    }

</script>


<style lang="scss" scoped>
    @import 'Buildings.scss';

</style>