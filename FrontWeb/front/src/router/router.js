import Vue from 'vue'
import Router from 'vue-router'
import Editor from "../views/Editor/Editor";
import LandingPage from "../views/LandingPage/LandingPage";
import SignInAdminForm from "../views/SignAdmin/SignInAdminForm";
import SignUpAdminForm from "../views/SignAdmin/SignUpAdminForm";
import AdminPage from "../views/AdminPage/AdminPage";
import Buildings from "../views/Buildings/Buildings";
import About from "../views/About";
import Help from "../views/Help";
import BuildingInfo from "../views/Buildings/BuildingInfo";


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
    path: '/admin/profile',
    name: 'adminpage',
    meta: {layout: 'main'},
    component: AdminPage,
  },
  {
    path: '/admin/maps',
    name: 'buildings',
    meta: {layout: 'main'},
    component: Buildings,
  },
  {
    path: '/admin/mapsinfo',
    name: 'buildingInfo',
    meta: {layout: 'main'},
    component: BuildingInfo,
    props: true
  },
  {
    path: '/admin/editor/:id',
    component: Editor,
    props: true
  },
  {
    path: '/admin/help',
    name: 'help',
    meta: {layout: 'main'},
    component: Help,
  },
  {
    path: '/admin/about',
    name: 'about',
    meta: {layout: 'main'},
    component: About,
  },





]
const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})



export default router
