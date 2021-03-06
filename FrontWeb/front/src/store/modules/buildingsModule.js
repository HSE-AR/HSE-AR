import axios from "axios";

export default {
    state: {
        buildings: null,
        building_info: null,
    },

    getters: {
        buildings: state => state.buildings,
        building_info: state => state.building_info,
    },

    actions: {
        getBuildingsFromUser(context) {
            return new Promise((resolve, reject) => {
                axios.get('https://localhost:5555/wapi/building', {
                    headers: {
                        'X-Company-Key': context.getters.company_actions[0].id
                    }
                })
                    .then(response => {
                        const buildings = response.data.buildings
                        const token = context.getters.token
                        axios.defaults.headers.common['Authorization'] = 'Bearer ' + token
                        context.commit('set_buildings_success', buildings)
                        resolve(response)
                    })
                    .catch(err => {
                        console.log(err)
                        reject(err)
                    })
            })
        },

        getBuildingInfo(context, payload) {
            return new Promise((resolve, reject) => {
                axios.get(`https://localhost:5555/wapi/building/${payload}`, {
                    headers: {
                        'X-Company-Key': context.getters.company_actions[0].id
                    }
                })
                        .then(response => {
                            const buildingInfo = response.data.buildingInfo
                            const token = context.getters.token
                            axios.defaults.headers.common['Authorization'] = 'Bearer ' + token
                            context.commit('set_buildingInfo_success', buildingInfo)
                            resolve(response)
                        })
                        .catch(err => {
                            console.log(err)
                            reject(err)
                        })
                })
        },

        createBuilding(context, payload) {
            return new Promise((resolve, reject) => {
                axios.post(`https://localhost:5555/wapi/building`, payload, {
                    headers: {
                        'X-Company-Key': context.getters.company_actions[0].id
                    }
                })
                    .then(response => {
                        const buildings = response.data.buildings
                        const token = context.getters.token
                        axios.defaults.headers.common['Authorization'] = 'Bearer ' + token
                        context.commit('set_buildings_success', buildings )
                        resolve(response)
                    })
                    .catch(err => {
                        console.log(err)
                        reject(err)
                    })
            })
        },
        createFloor(context, payload) {
            return new Promise((resolve, reject) => {
                axios.post(`https://localhost:5555/wapi/floor`, payload, {
                    headers: {
                        'X-Company-Key': context.getters.company_actions[0].id
                    }
                })
                    .then(response => {
                        const buildingInfo = response.data.buildingInfo
                        const token = context.getters.token
                        axios.defaults.headers.common['Authorization'] = 'Bearer ' + token
                        context.commit('set_buildingInfo_success', buildingInfo)
                        resolve(response)
                    })
                    .catch(err => {
                        console.log(err)
                        reject(err)
                    })
            })
        }

    },

    mutations: {
        set_buildings_success(state, buildings) {
            state.buildings = buildings
        },
        set_buildingInfo_success(state, buildingInfo) {
            state.building_info = buildingInfo
        },

    },
}