import Vue from 'vue'
import Vuex from 'vuex'
import axios from "axios";
import buildingsModule from "./modules/buildingsModule";
import userModule from "./modules/userModule";
import generalModule from "./modules/generalModule";
Vue.use(Vuex)



export default new Vuex.Store({
  modules: {
    generalModule, buildingsModule, userModule
  },
  state: {
    port:'https://localhost:5555/wapi/', //ip адрес компа,(https://192.168.0.103:5555)
    sceneTestId: '5fea51e7c4c61e2e98e97794' //id можно посмотреть в монге
  },
})
