import axios from "axios";

export default {
    state: {
        buildings: null,
        building_info: null
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
                        const buildings = response.data
                        const token = context.getters.token
                        axios.defaults.headers.common['Authorization'] = 'Bearer ' + token
                        context.commit('get_buildings_success', buildings)
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
                            const buildingInfo = response.data
                            const token = context.getters.token
                            axios.defaults.headers.common['Authorization'] = 'Bearer ' + token
                            context.commit('get_buildingInfo_success', buildingInfo)
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
        get_buildings_success(state, buildings) {
            state.buildings = buildings
            state.status = 'success'
        },
        get_buildingInfo_success(state, buildingInfo) {
            state.building_info = buildingInfo
            state.status = 'success'
        }
    },
}