import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

//import actions from './actions'
//import getters from './getters'
import modules from './modules'
//import mutations from './mutations'
//import state from './state'

Vue.use(Vuex)

const store = new Vuex.Store({
  //actions,
  //getters,
  modules,
  //mutations,
  //state
})

if (store.state.auth.token)
  axios.defaults.headers.common['Authorization'] = store.state.auth.token

export default store
