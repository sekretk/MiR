import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify';
import axios from 'axios'
import moment from 'moment'

import './components'

import './plugins'

//import { sync } from 'vuex-router-sync'

import router from '@/router'
import store from '@/store'
import './registerServiceWorker'

Vue.filter('formatDate', function(value) {
  if (value) {
    return moment(String(value)).format('DD.MM.YYYY hh:mm')
  }
})

Vue.filter('shortDate', function(value) {
  if (value) {
    return moment(String(value)).format('DD.MM.YYYY')
  }
})

Vue.filter('onlyTime', function(value) {
  if (value) {
    return moment(String(value)).format('hh.mm')
  }
})

Vue.filter('numFormat', function (value) {
  var formatter = new Intl.NumberFormat('ru-RU', {
    style: 'currency',
    currency: 'RUB',
  });
  
  return formatter.format(value);
  })

Date.prototype.addDays = function(days) {
  var date = new Date(this.valueOf());
  date.setDate(date.getDate() + days);
  return date;
}

Vue.config.productionTip = false

if (store.state.token)
  axios.defaults.headers.common['Authorization'] = store.state.token

new Vue({
  store,
  router,  
  vuetify,  
  render: h => h(App)
}).$mount('#app')
