import Vue from 'vue'
import App from './App.vue'
import router from './router/router'
import store from './store/store'
// import * as THREE from './js/three/build/three.module.js';
/* eslint-disable */
import * as THREE from '../public/threejs/build/three.module'
import { Editor } from '../public/threejs/editor/js/Editor.js';
import { Viewport } from '../public/threejs/editor/js/Viewport.js';
import { Toolbar } from '../public/threejs/editor/js/Toolbar.js';
import { Script } from '../public/threejs/editor/js/Script.js';
import { Player } from '../public/threejs/editor/js/Player.js';
import { Sidebar } from '../public/threejs/editor/js/Sidebar.js';
import { Menubar } from '../public/threejs/editor/js/Menubar.js';
import { Resizer } from '../public/threejs/editor/js/Resizer.js';
import Axios from 'axios'


Vue.prototype.$http = Axios;
const token = localStorage.getItem('token')
if (token) {
  Vue.prototype.$http.defaults.headers.common['Authorization'] = 'Bearer ' + token
}


Vue.config.productionTip = false

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')



export { THREE,Editor,Viewport,Toolbar,Script,Player,Sidebar,Menubar,Resizer};