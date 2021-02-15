import axios from "axios";

export default {
    state: {
        buildings: [],
    },

    getters: {
        allBuildings(state) {
            return state.buildings
        },
        buildingsCount(state) {
            return state.buildings.length
        }
    },

    actions: {
        getBuildingsFromBack({commit}) {
            return axios.get('building')
                .then(buildings => {
                    commit('setBuildingsToState', buildings.data)
                    return buildings
                })
                .catch(err => console.log(err))
        }
    },

    mutations: {
        setBuildingsToState(state, buildings) {
            state.buildings = buildings
        },

        createBuilding(state, newBuilding) {
            axios.post('building', newBuilding)
                .then(response => {
                    console.log(response)
                    state.buildings.unshift(newBuilding)
                })
                .catch(err => console.log(err))

        }
    },
}