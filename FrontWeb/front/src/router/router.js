import Vue from 'vue'
import Router from 'vue-router'




Vue.use(Router)

const routes = [
  {
    path: '/',
    name: 'Landing',
    meta: {layout: 'landing'},
    component: () => import('../views/LandingPage/LandingPage.vue')
  },
  {
    path: '/landing',
    name: 'Landing',
    meta: {layout: 'landing'},
    component: () => import('../views/LandingPage/LandingPage.vue')
  },
  {
    path: '/signin/admin',
    name: 'SignInAdmin',
    meta: {layout: 'sign'},
    component: () => import('../views/SignAdmin/SignInAdminForm.vue')
  },
  {
    path: '/signup/admin',
    name: 'SignUpAdmin',
    meta: {layout: 'sign'},
    component: () => import('../views/SignAdmin/SignUpAdminForm.vue')
  },
  {
    path: '/adminka/',
    name: 'adminka',
    meta: {layout: 'main'},
    component: () => import('../views/Adminka'),
    children : [
      {
        path: 'buildings',
        name: 'buildings-list',
        meta: {layout: 'main'},
        component: () => import('../views/Buildings')
      },
      {
        path: 'editor',
        component: () => import('../views/Editor/Editor.vue')
      }
    ]
  },
  // {
  //   path: '/arweb/',
  //   name: 'ARWeb',
  //   component: ARWeb,
  //   children: [
  //     {
  //       path: '',
  //       name: 'MainARWeb',
  //       component: MainARWeb
  //     },
  //     {
  //       path: 'main',
  //       name: 'MainARWeb',
  //       component: MainARWeb
  //     },
  //   ]
  // },



]

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
