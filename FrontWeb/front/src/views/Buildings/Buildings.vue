<template>
    <div>
        <div>
            <h2>BUILDINGS:</h2>
            <div class="buildings__cards" v-if="buildings">

                <div
                    v-for="building in buildings"
                    :key="building.id"
                >
                    <div class="building__container">
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
                    <form class="building__creation__form" @submit.prevent="createBuilding">
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
            ...mapGetters(['buildings'])
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


<style lang="scss" scoped>
    @import 'Buildings.scss';
</style>