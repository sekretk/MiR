import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'
import modules from './modules'

Vue.use(Vuex)

const store = new Vuex.Store({
  modules,
})

if (store.state.auth.token)
  axios.defaults.headers.common['Authorization'] = store.state.auth.token


export default store
