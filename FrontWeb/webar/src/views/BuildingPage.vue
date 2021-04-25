<template>
  <div >

    <div style="width: 100%; background-color: #04040C; height: 30px;text-align: center;padding-top: 20px;position: fixed;z-index: 100">
      <span style="font-size: 20px; color:white;" >Web.AR</span>
    </div>

    <div style="background-image: linear-gradient(#04040C, rgb(35,34,114));padding-top: 80px;height: 100vh;padding-left: 30px;padding-right: 30px">


      <div style="margin-bottom: 20px;">
        <span @click="backUp" class="backup" >Back up</span>
      </div>

      <div style="text-align: center">

        <div style="margin-bottom: 20px;">
          <span>{{building.arPlace.buildingTitle}}</span>
        </div>

      <carousel-3d  :width="200" :height="150">
        <slide style="border: 0px solid white;border-radius: 15px" :index="0">
          <img width="100%" height="100%" :src="$store.state.portBack +'/staticImgs/11.jpg'">
        </slide>
        <slide style="border: 0px solid white;border-radius: 15px" :index="1">
          <img width="100%" height="100%" :src="$store.state.portBack + '/staticImgs/2.jpg'">
        </slide>
        <slide style="border: 0px solid white;border-radius: 15px" :index="2">
          <img width="100%" height="100%" :src="$store.state.portBack + '/staticImgs/2.jpg'">
        </slide>
      </carousel-3d>



        <div style="border-radius: 15px;border:0px solid white; background-color: rgba(0,0,0,0.4); padding: 15px;margin-top: 40px">

          <div style="margin-bottom: 40px;">
            <span>Address: {{building.arPlace.address}}</span>
          </div>

          <div style="margin-bottom: 20px;">
            <span>Choose the floor:</span>
          </div>

          <div style="padding-right: 60px;padding-left: 60px">
            <v-select class="style-chooser" v-model="selectedFloor" :options="options"></v-select>
          </div>
        </div>

        <div style="margin-top: 20px">
          <a @click="startAr" href="#" class="button8">Start AR</a>
        </div>


      </div>

    </div>


  </div>
</template>

<script>
import { Carousel3d, Slide } from 'vue-carousel-3d';
import axios from 'axios'
import Swal from 'sweetalert2'

export default {
  name: "BuildingPage",
  data(){
    return{
      building: null,
      options: [],
      selectedFloor:null,
    }
  },
  components: {
    Carousel3d,
    Slide,
  },

  async created() {
    await this.GetBuildingInfoFromBack(this.$route.params.buildingId);
  },

  methods:{
    backUp(){
      this.$router.push('/');
    },

    startAr(){
      if(this.selectedFloor == null){
            Swal.fire('You must choose a floor')
      }
      else {

        this.$router.push('/ar/'+this.$route.params.buildingId + '/' +  this.selectedFloor.code)
      }
    },

    async GetBuildingInfoFromBack(buildingId){
      await axios.get(this.$store.state.portBack +"arapi/arPlace/"+ buildingId, {
        headers: {
          'X-ArClient-Key':this.$store.state.arClientKey
        }})
          .then((response) =>{
            console.log(response)
              this.building = response.data
              this.building.floors.forEach((floor)=>{
                this.options.push({code:floor.floorId, label:floor.floorNumber})
              })
          }).catch((e)=>{
            console.log(e)
          })
    }

  }
}
</script>

<style scoped>

</style>