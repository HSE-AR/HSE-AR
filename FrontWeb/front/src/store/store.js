import Vue from 'vue'
import Vuex from 'vuex'
import axios from "axios";
import building from "./modules/building";
import user from './modules/user'
Vue.use(Vuex)



export default new Vuex.Store({
  modules: {
    building, user
  },
  state: {
    port:'https://localhost:5555/wapi/', //ip адрес компа,(https://192.168.0.103:5555)
    sceneTestId: '5fea51e7c4c61e2e98e97794' //id можно посмотреть в монге
  },
})
