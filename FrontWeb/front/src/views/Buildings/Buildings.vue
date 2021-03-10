<template>
    <div>
        <div>
            <h2>BUILDINGS:</h2>
            <hr>
            <h1 v-if="buildings">{{buildings}}</h1>
            <hr>
            <h2>BUILDING_INFO:</h2>
            <hr>
            <button @click="isHidden = !isHidden">{{ isHidden ? 'Show' : 'Hide' }}</button>
            <div class="building__info" v-if="!isHidden">
                <ul>
                    <li
                            v-for="floor in building_info.buildingInfo.floors"
                            :key="floor.id"
                    >
                        <h1>{{floor.title}}</h1>
                        <router-link :to="{path: `/admin/editor/`, query: {floorId: floor.id}}" class="editFloor">Edit Floor</router-link>
                    </li>
                </ul>

            </div>
            <hr>
            <div class="building__creation">
                <form @submit.prevent="createBuilding">
                    <span class="form__input">
                      <input v-model="title" type="text" id="title" class="input" placeholder="Title..." required>
                    </span>
                    <span class="form__input">
                      <input v-model="address" type="text" id="address" class="input" placeholder="Address..." required>
                    </span>
                    <span class="form__input">
                      <input v-model="coordinate" type="text" id="coordinate" class="input" placeholder="Coordinate..." required>
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
        name: 'Buildings',
        data() {
            return {
                title: null,
                address: null,
                coordinate: null,
                isHidden: true,
            }
        },
        computed: {
            ...mapGetters(['buildings', 'building_info'])
        },
        methods: {
            async createBuilding() {
                const data = {
                    title: this.title,
                    address: this.address,
                    coordinate: this.coordinate
                }
                await this.$store.dispatch('createBuilding', data)
                    .then(response => {
                        console.log(response)
                    })
                    .catch(err => console.log(err))
            },

        }

    }

</script>


<style scoped>

</style>