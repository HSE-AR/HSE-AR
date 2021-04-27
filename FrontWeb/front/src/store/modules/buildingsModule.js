import axios from "axios";
import store from "@/store/store";

export default {
    state: {
        buildings: JSON.parse(localStorage.getItem('buildings')) || null,
        building_info: JSON.parse(localStorage.getItem('building_info')) || null,
        pointclouds:  null,
        loadingStatus: false
    },

    getters: {
        buildings: state => state.buildings,
        building_info: state => state.building_info,
        pointclouds: state => state.pointclouds,
        loadingStatus: state => state.loadingStatus
    },

    actions: {
        getBuildingsFromUser(context) {
            return new Promise((resolve, reject) => {
                context.commit('set_loading_status', true)
                axios.get(store.state.port + 'wapi/building', {
                    headers: {
                        'X-Company-Key': JSON.parse(localStorage.getItem('company_actions'))[0].id
                    }
                })
                    .then(response => {
                        const buildings = response.data.buildings
                        const token = context.getters.token
                        localStorage.setItem('buildings', JSON.stringify(buildings))
                        axios.defaults.headers.common['Authorization'] = 'Bearer ' + token
                        context.commit('set_buildings_success', buildings)
                        context.commit('set_loading_status', false)
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
                context.commit('set_loading_status', true)
                axios.get(store.state.port + `wapi/building/${payload}`, {
                    headers: {
                        'X-Company-Key': JSON.parse(localStorage.getItem('company_actions'))[0].id
                    }
                })
                        .then(response => {
                            const buildingInfo = response.data.buildingInfo
                            const token = context.getters.token
                            localStorage.setItem('building_info', JSON.stringify(buildingInfo))
                            axios.defaults.headers.common['Authorization'] = 'Bearer ' + token
                            context.commit('set_buildingInfo_success', buildingInfo)
                            context.commit('set_loading_status', false)
                            resolve(response)
                        })
                        .catch(err => {
                            console.log(err)
                            reject(err)
                        })
                })
        },

        getPointClouds(context) {
            return new Promise((resolve, reject) => {
                context.commit('set_loading_status', true)
                axios.get(store.state.port + `wapi/pointcloud`, {
                    headers: {
                        'X-Company-Key': JSON.parse(localStorage.getItem('company_actions'))[0].id
                    }
                })
                    .then(response => {
                        const pointclouds = response.data
                        const token = context.getters.token
                        localStorage.setItem('pointclouds', JSON.stringify(pointclouds))
                        axios.defaults.headers.common['Authorization'] = 'Bearer ' + token
                        context.commit('set_pointclouds_success', pointclouds)
                        context.commit('set_loading_status', false)
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

                axios.post(store.state.port + `wapi/building`, payload, {
                    headers: {
                        'X-Company-Key': JSON.parse(localStorage.getItem('company_actions'))[0].id
                    }
                })
                    .then(response => {
                        const buildings = response.data.buildings
                        const token = context.getters.token
                        console.log(payload)
                        context.commit("set_buildings_success",buildings)
                        localStorage.setItem('buildings', JSON.stringify(buildings)) //delete
                        axios.defaults.headers.common['Authorization'] = 'Bearer ' + token
                        resolve(response)
                    })
                    .catch(err => {
                        console.log(err)
                        reject(err)
                    })
            })
        },

        deleteBuilding(context, payload) {
              return new Promise((resolve, reject) => {
                  axios.delete(store.state.port + `wapi/building/${payload}`, {
                      headers: {
                          'X-Company-Key': JSON.parse(localStorage.getItem('company_actions'))[0].id
                      }
                  })
                          .then(response => {
                              const buildings = response.data.buildings
                              const token = context.getters.token
                              localStorage.setItem('buildings', JSON.stringify(buildings))
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
        deleteFloor(context, payload) {
            return new Promise((resolve, reject) => {
                axios.delete(store.state.port + `wapi/floor/${payload.floorId}/building/${payload.buildingId}`, {
                    headers: {
                        'X-Company-Key': JSON.parse(localStorage.getItem('company_actions'))[0].id
                    }
                })
                    .then(response => {
                        const buildingInfo = response.data.buildingInfo
                        const token = context.getters.token
                        localStorage.setItem('building_info', JSON.stringify(buildingInfo))
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
        createFloor(context, payload) {
            return new Promise((resolve, reject) => {
                axios.post(store.state.port + `wapi/floor`, payload, {
                    headers: {
                        'X-Company-Key': JSON.parse(localStorage.getItem('company_actions'))[0].id
                    }
                })
                    .then(response => {
                        const buildingInfo = response.data.buildingInfo
                        const token = context.getters.token
                        localStorage.setItem('building_info', JSON.stringify(buildingInfo))
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
        set_pointclouds_success(state, pointclouds) {
            state.pointclouds = pointclouds
        },
        set_loading_status(state, newLoadingStatus) {
            state.loadingStatus = newLoadingStatus
        }

    },
}