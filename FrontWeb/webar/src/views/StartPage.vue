<template>

  <div style="text-align: center">

    <div style="width: 100%; background-color: #04040C; height: 30px;text-align: center;padding-top: 20px;position: fixed;z-index: 100">
      <span style="font-size: 20px; color:white;" >Web.AR</span>
    </div>

    <div style="background-image: linear-gradient(#04040C, rgb(35,34,114));padding-top: 100px;height: 100vh;padding-left: 30px;padding-right: 30px">

<!--      {{buildings}}-->

<!--      <div  v-cloak>-->

<!--        <div v-if="errorStr">-->
<!--          Sorry, but the following error-->
<!--          occurred: {{errorStr}}-->
<!--        </div>&ndash;&gt;-->

<!--        <div v-if="gettingLocation">-->
<!--          <i>Getting your location...</i>-->
<!--        </div>-->

<!--        <div v-if="location">-->
<!--          Your location data is {{ location.coords.latitude }}, {{ location.coords.longitude}}-->
<!--        </div>-->
<!--      </div>-->

      <div style="margin-bottom: 20px">
        <span>Buildings on the map:</span>
      </div>

      <div style="border-radius:15px; border: 0px solid white;overflow: hidden  ">

        <div>
          <gmap-map
              id="map"
              :center="center"
              :zoom="12"
              style="width: 100%; height: 300px"
          >
            <gmap-marker
                :key="index"
                v-for="(m, index) in markers"
                :position="m.position"
                :clickable="true"
                :draggable="false"
                @click="getSelectedBuilding(m.building)"
            ></gmap-marker>
          </gmap-map>
        </div>

        <div style="background-color: rgba(0,0,0,0.4); padding: 20px">
            <span v-for="(building,i) in buildings" :key="i">
              {{building.buildingTitle}}
            </span>

            <hr style="color: white;margin-top: 20px">

            <span>
              AR-buildings found: {{buildings.length}}
            </span>
        </div>

      </div>
    </div>


  </div>

</template>


<script>
import axios from 'axios'
import {gmapApi} from 'vue2-google-maps'
import Swal from 'sweetalert2'

export default {
  computed: {
    google: gmapApi
  },
  name: "StartPage",
  data() {
    return{
      center: {lat: 10.0, lng: 10.0},
      markers: [],
      buildings: [],
      gettingLocation: false,
      location: null,
      errorStr: null
    }
  },

  async created() {
       if(!("geolocation" in navigator)) {
         this.errorStr = 'Geolocation is not available.';
         return;
       }

       this.gettingLocation = true;
       // get position
       navigator.geolocation.getCurrentPosition(pos => {
         this.gettingLocation = false;
         this.location = pos;
         this.center ={lat: pos.coords.latitude, lng: pos.coords.longitude}

       }, err => {
         this.gettingLocation = false;
         this.errorStr = err.message;
       })

      await axios.get(this.$store.state.portBack +'arapi/arplace',{
        headers: {
          'X-ArClient-Key':this.$store.state.arClientKey
        }
      }).then(response =>{
        this.buildings = response.data.arPlaces

        console.log(this.buildings)
        this.buildings.forEach((building) =>{
          this.markers.push({
            position: {lat: building.latitude, lng: building.longitude},
            building: building
          })
        })
      });
  },

  methods:{
    async getSelectedBuilding(building){

      let floorsForSelect = {}


      Swal.fire({
        title: building.buildingTitle,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'go to building'
      }).then((result) => {
        if (result.isConfirmed) {
          this.$router.push('/ar/'+building.buildingId )
        }
      })
    }
  }
}
</script>

<style scoped>

</style>