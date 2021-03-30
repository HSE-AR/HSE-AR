<template>
  <div>
    {{buildings}}

    <div id="app" v-cloak>

     <div v-if="errorStr">
       Sorry, but the following error
        occurred: {{errorStr}}
      </div>-->

      <div v-if="gettingLocation">
        <i>Getting your location...</i>
     </div>

    <div v-if="location">
       Your location data is {{ location.coords.latitude }}, {{ location.coords.longitude}}
      </div>


    </div>
    <gmap-map
        :center="center"
        :zoom="17"
        style="width: 500px; height: 300px"
    >
      <gmap-marker
          :key="index"
          v-for="(m, index) in markers"
          :position="m.position"
          :clickable="true"
          :draggable="true"
          @click="getSelectedBuilding(m.building)"
      ></gmap-marker>
    </gmap-map>
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
      building.floors.forEach((floor) => {
        floorsForSelect[floor.floorId] = floor.floorNumber
      })

      const { value: floorId } = await Swal.fire({
        title: 'Нужно выбрать номер этажа',
        input: 'select',
        inputOptions: floorsForSelect,
        inputPlaceholder: 'Выберите этаж',
        showCancelButton: true,
        inputValidator: (value) => {
          return new Promise((resolve) => {
            if(!value){
              resolve("Не выбран этаж")
            }
            resolve()
          })
        }
      })

      if(floorId){
        this.$router.push('/ar/'+building.buildingId + '/' +  floorId)
      }

    }
  }
}
</script>

<style scoped>

</style>