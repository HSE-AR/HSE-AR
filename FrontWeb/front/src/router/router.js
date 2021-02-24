import Vue from 'vue'
import Router from 'vue-router'
import Editor from "../views/Editor/Editor";
import LandingPage from "../views/LandingPage/LandingPage";
import SignInAdminForm from "../views/SignAdmin/SignInAdminForm";
import SignUpAdminForm from "../views/SignAdmin/SignUpAdminForm";
import Adminka from "../views/Adminka";
import Buildings from "../views/Buildings";



Vue.use(Router)

const routes = [
  {
    path: '/',
    name: 'Landing',
    meta: {layout: 'landing'},
    component: LandingPage
  },
  {
    path: '/landing',
    name: 'Landing',
    meta: {layout: 'landing'},
    component: LandingPage
  },
  {
    path: '/signin/admin',
    name: 'SignInAdmin',
    meta: {layout: 'sign'},
    component: SignInAdminForm
  },
  {
    path: '/signup/admin',
    name: 'SignUpAdmin',
    meta: {layout: 'sign'},
    component: SignUpAdminForm
  },
  {
    path: '/adminka',
    name: 'adminka',
    meta: {layout: 'main'},
    component: Adminka,
    children : [
      {
        path: '/buildings',
        name: 'buildings-list',
        meta: {layout: 'main'},
        component: Buildings,
      },
      {
        path: '/editor',
        component: Editor
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
