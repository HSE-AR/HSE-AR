import Vue from 'vue'
import App from './App.vue'
import router from './router/router'
import store from './store/store'
import VueGlide from 'vue-glide-js'
import 'vue-glide-js/dist/vue-glide.css'
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
import { Dialog } from '../public/threejs/editor/js/Dialog.js';
import Axios from 'axios'
import VueProgressBar from 'vue-progressbar'

import * as VueGoogleMaps from 'vue2-google-maps'

Vue.use(VueGoogleMaps, {
  load: {
    key: 'AIzaSyCq-JaV31-UXfDhvz5_FZMqIyffpWnx5Gs',
    libraries: 'places', // This is required if you use the Autocomplete plugin
  },

})
Vue.use(VueGlide)


Vue.prototype.$http = Axios;
const token = localStorage.getItem('token')
if (token) {
  Vue.prototype.$http.defaults.headers.common['Authorization'] = 'Bearer ' + token
}

Vue.use(VueProgressBar, {
  color: '#9774FF',
  failedColor: 'red',
  height: '4px'
})


Vue.config.productionTip = false

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')



export { THREE,Editor,Viewport,Toolbar,Script,Player,Sidebar,Menubar,Resizer, Dialog};