import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify';

import './components'

//import { sync } from 'vuex-router-sync'

//import router from '@/router'
//import store from '@/store'

Vue.config.productionTip = false

new Vue({
  vuetify,
  render: h => h(App)
}).$mount('#app')
