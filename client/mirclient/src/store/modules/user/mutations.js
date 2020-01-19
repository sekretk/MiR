import { USER_REQUEST, USER_ERROR, USER_SUCCESS } from './consts'
import Vue from 'vue'

export default {
    [USER_REQUEST]: (state) => {
        state.status = 'loading'
      },
      [USER_SUCCESS]: (state, resp) => {
        state.status = 'success'
        Vue.set(state, 'profile', resp)
        //state.profile=resp
      },
      [USER_ERROR]: (state) => {
        state.status = 'error'
      },
}