import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    portBack:'https://192.168.1.45:4444/', //тут нужно указать свой ip и на беке тоже в ArClient.Api.launchSettings.json
    floorId: 'bbf35695-5cdc-4f25-86fd-fd2841df7db3', //id взятый из базы данных
    arClientKey: '6d9878e1-672f-4f38-91d7-1d6807c97f18' //постоянный id
  },
  mutations: {
  },
  actions: {
  },
  modules: {
  }
})
