import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify';
import axios from 'axios'

import './components'

import './plugins'

//import { sync } from 'vuex-router-sync'

import router from '@/router'
import store from '@/store'

Vue.config.productionTip = false

if (store.state.token)
  axios.defaults.headers.common['Authorization'] = store.state.token

new Vue({
  store,
  router,  
  vuetify,  
  render: h => h(App)
}).$mount('#app')
