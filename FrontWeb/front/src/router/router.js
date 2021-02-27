import Vue from 'vue'
import Router from 'vue-router'
import Editor from "../views/Editor/Editor";
import LandingPage from "../views/LandingPage/LandingPage";
import SignInAdminForm from "../views/SignAdmin/SignInAdminForm";
import SignUpAdminForm from "../views/SignAdmin/SignUpAdminForm";
import AdminPage from "../views/AdminPage";
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
    path: '/adminpage',
    name: 'adminpage',
    meta: {layout: 'main'},
    component: AdminPage,
    children : [
      {
        path: '/buildings',
        name: 'buildings',
        meta: {layout: 'main'},
        component: Buildings,
      },
      {
        path: '/editor/:id',
        component: Editor,
        props: true
      }
    ]
  },


]
const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})



export default router
