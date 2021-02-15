<template>
    <form @submit.prevent="submit">
        <input type="text" placeholder="Building name" v-model="title">
        <input type="text" placeholder="Building number" v-model="number">
        <input type="text" placeholder="Building address" v-model="address">
        <input type="text" placeholder="Building coordinate" v-model="coordinate">
        <p>Floors</p>

        <input type="text" placeholder="Floor name" v-model="floorTitle">
        <button type="submit">Create building</button>
    </form>
</template>

<script>
    import { mapMutations } from 'vuex'
    import {v4 as uuidv4 } from 'uuid'
    export default {
        name: "Floor",
        data() {
            return {
                title: "",
                number: "",
                address: "",
                coordinate: "",
                floorTitle: "",
            }
        },
        methods: {
            ...mapMutations(["createBuilding"]),
          submit() {
            this.createBuilding({
                buildingId: uuidv4(),
                title: this.title,
                address: this.address,
                coordinate: this.coordinate,
                floors: {
                    id: uuidv4(),
                    number: this.number,
                    title: this.floorTitle,
                    createdAtUtc: Date.now(),
                    sceneId: uuidv4(),
                    buildingId: this.buildingId
                }
            })
          }
        },
    }
</script>

<style scoped>

</style>