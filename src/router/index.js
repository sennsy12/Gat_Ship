import Vue from 'vue'
import VueRouter from 'vue-router'
import AppDashboard from '../views/AppDashboard.vue'
import AppCalendar from '../views/AppCalendar.vue'
import AppProfile from '../views/AppProfile.vue'
import AppLogin from '../components/AppLogin.vue'
import ViewScheduleComp from '../components/ViewScheduleComp.vue'


Vue.use(VueRouter)
export default new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,

  routes: [
  {
    path: '/dashboard',
    name: 'dashboard',
    component: AppDashboard
  },
  {
    path: '/calendar',
    name: 'calendar',
    component: AppCalendar
  },
  {
    path: '/ViewSchedule',
    name: 'ViewSchedule',
    component: ViewScheduleComp
  },
  {
    path: '/profileView',
    name: 'profileView',
    component: AppProfile
  },
{
  path: '/',
  name: 'login',
  component: AppLogin,
},

  
]

})


