import { OPERATIONS_REQUEST, OPERATIONS_SUCCESS, OPERATIONS_ERROR } from './consts'
import Vue from 'vue'

export default {
    [OPERATIONS_REQUEST]: (state) => {
        state.status = 'loading'
      },
      [OPERATIONS_SUCCESS]: (state, resp) => {
        state.status = 'success'
        Vue.set(state, 'operations', resp.items)
        state.totalOperations = resp.totalAmount
      },
      [OPERATIONS_ERROR]: (state) => {
        state.status = 'error'
        Vue.set(state, 'operations', [])
        state.totalOperations = 0
      },
}