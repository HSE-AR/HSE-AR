<template>
    <div>


        <Popup
            v-if="isPopupVisible"
            @closePopup="closePopup"
        >
            <Floor />
        </Popup>



        <div class="buildings__header">
              <h1 class="buildings__heading">Buildings</h1>
              <button
                  class="buildings__button"
                  @click="showPopup"
              >Add Floor
              </button>
        </div>
        <div>
            <h1>{{ buildingsCount }}</h1>
            <div v-for="building in allBuildings" :key="building.id">
                <h2>{{ building.title }}</h2>
            </div>
        </div>
        <div class=""></div>
    </div>
</template>




<script>
    import axios from "axios"
    import { mapGetters, mapActions } from 'vuex'
    import Popup from "../components/app/Popup";
    import Floor from "./Floor";


    export default {
        data() {
            return {
                isPopupVisible: false
            }
        },
        components: {
          Popup, Floor
        },
        computed: mapGetters(['allBuildings', 'buildingsCount']),
        methods: {
            ...mapActions(['getBuildingsFromBack']),

            showPopup() {
                this.isPopupVisible = true
            },

            closePopup() {
                this.isPopupVisible = false
            }
        },
        mounted() {
            this.getBuildingsFromBack()
        },
    }
</script>

<style scoped>

</style>