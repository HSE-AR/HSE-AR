import Vue from 'vue'
import Vuex from 'vuex'
import axios from "axios";

Vue.use(Vuex)



export default new Vuex.Store({
  state: {
    user: null,
    port:'https://localhost:5555/wapi/', //ip адрес компа,(https://192.168.0.103:5555)
    sceneTestId: '5fea51e7c4c61e2e98e97794' //id можно посмотреть в монге
  },
  getters: {
    user: state => {
      return state.user
    }
  },
  actions: {
    user(context, user) {
      context.commit('user', user)
    }
  },
  mutations: {
    user(state, user) {
      state.user = user
    }
  },


})
