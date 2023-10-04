
import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify'
import { createPinia, PiniaVuePlugin } from 'pinia'
import router from './router'
import './axios'


const pinia = createPinia()

Vue.use(PiniaVuePlugin)
Vue.config.productionTip = false

new Vue({
  vuetify,
  pinia,
  router,
  render: h => h(App)
}).$mount('#app')



