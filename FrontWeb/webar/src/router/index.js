import Vue from 'vue'
import VueRouter from 'vue-router'
import MainAR from '../views/MainAR.vue'
import StartPage from '../views/StartPage.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'StartPage',
    component: StartPage
  },
  /*{
    path: '/ar/:buildingId/:floorId',
    name: 'MainAR',
    component: MainAR
  }*/
  {
    path: '/ar/:buildingId/:floorId',
    name: 'MainAR',
    component: MainAR,
    props: true
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
