import Vue from 'vue'
import Vuex from 'vuex'
import VuexPersist from 'vuex-persist';
import buildingsModule from "./modules/buildingsModule";
import userModule from "./modules/userModule";
Vue.use(Vuex)

const vuexLocalStorage = new VuexPersist({
  key: 'vuex',
  storage: window.localStorage,
})



export default new Vuex.Store({
  modules: {
    buildingsModule, userModule
  },
  state: {
    port:'https://localhost:5555/', //ip адрес компа,(https://192.168.0.103:5555)
    sceneTestId: '5fea51e7c4c61e2e98e97794', //id можно посмотреть в монге
    pointCloudsInfo:[],

  },
  mutations:{
    setPointCloudsInfo(state ,newPcds){
      state.pointCloudsInfo = newPcds
    },

  },
  plugins: [vuexLocalStorage.plugin]
})
