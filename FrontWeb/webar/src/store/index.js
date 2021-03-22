import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    portBack:'https://192.168.1.45:4444/', //тут нужно указать свой ip и на беке тоже в ArClient.Api.launchSettings.json
    floorId: '2930ae6d-a6b1-4189-8c80-662bffb965ee', //id взятый из базы данных
    arClientKey: '6d9878e1-672f-4f38-91d7-1d6807c97f18' //постоянный id
  },
  mutations: {
  },
  actions: {
  },
  modules: {
  }
})
