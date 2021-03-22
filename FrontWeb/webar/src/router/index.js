import Vue from 'vue'
import VueRouter from 'vue-router'
import MainAR from '../views/MainAR.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: MainAR
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
