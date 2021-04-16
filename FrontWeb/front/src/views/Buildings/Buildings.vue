<template>
    <div>
        <div>
            <div class="buildings__heading_container">
                <h1 class="buildings__heading">Your Buildings:</h1>
            </div>
                <div class="buildings__cards">
                    <div
                        v-for="building in buildings"
                        :key="building.id"
                    >
                        <div class="building__container">
                            <button @click="deleteBuilding(building.id)" class="building__deletion">
                                <svg xmlns="http://www.w3.org/2000/svg" width="14" height="14" viewBox="0 0 24 24"><path d="M24 20.188l-8.315-8.209 8.2-8.282-3.697-3.697-8.212 8.318-8.31-8.203-3.666 3.666 8.321 8.24-8.206 8.313 3.666 3.666 8.237-8.318 8.285 8.203z"/></svg>
                            </button>
                            <div class="building__header">
                                {{building.title}}
                            </div>
                            <div class="building__main">
                                {{building.address}}
                            </div>
                            <div class="building__footer">
                                {{building.coordinate}}
                            </div>
                            <router-link :to="{path: `/admin/buildinginfo/`, query: {buildingId: building.id}}">
                                Info
                            </router-link>


                        </div>
                    </div>
                </div>
            <hr>
            <div class="building__creation_container">
                <div class="building__creation">
                  <div  style="width: 500px;">
                        <div class="form__input">
                          <input v-model="title" type="text" id="title" class="input" placeholder="Title..." required>
                        </div>
                        <div class="form__input">
                          <input v-model="address" type="text" id="address" class="input" placeholder="Address..." required>
                        </div>


                        <gmap-map
                            id="map"
                            ref="Map"
                            :center="center"
                            :zoom="2"
                            style="width: 100%; height: 300px"
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


                        <button class="building__creation__submit" @click="createBuilding">Create Building</button>
                </div>
            </div>
        </div>
        </div>
      </div>
</template>

<script>
    import {mapGetters} from "vuex";
    import {gmapApi} from 'vue2-google-maps'
    import Swal from 'sweetalert2'

    export default {

        name: 'Buildings',
        data() {
            return {
              center: {lat: 10.0, lng: 10.0},
              markers: [],
              map:null,

                title: null,
                address: null,
                isHidden: true,
            }
        },
      mounted() {
        //this.initMap()
      },
      computed: {
        google: gmapApi,
        ...mapGetters(['buildings']),

        },
        methods: {
          onMapClick(e) {
            this.markers=[{
              id: 1 + Math.max(0, ...this.markers.map(n => n.id)),
              position: e.latLng}]

          },
          onMarkerClick(e) {
            this.$refs.Map.panTo(e.latLng);
          },

            async createBuilding() {

                if(this.markers.length == 0){
                  Swal.fire('put a marker on the map')
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
            async deleteBuilding(buildingId) {
                this.$Progress.start()
                await this.$store.dispatch('deleteBuilding', buildingId)
                    .then(response => {
                        console.log(response)
                        this.$Progress.finish()
                    })
                    .catch(err => {
                        console.log(err)
                        this.$Progress.fail()
                    })
            }

        }

    }

</script>


<style lang="scss" scoped>
    @import 'Buildings.scss';

</style>