import { OPERATIONS_REQUEST, OPERATIONS_REQUEST_MORE, OPERATIONS_SUCCESS, OPERATIONS_ERROR, MOVE_SELECTED_DATE } from './consts'
import Vue from 'vue'

export default {
    [OPERATIONS_REQUEST]: (state) => {
      Vue.set(state, 'operations', [])
      state.totalOperations = 0
      state.cash = 0
      state.card = 0
      state.average = 0
        state.status = 'loading'
      },
      [OPERATIONS_REQUEST_MORE]: (state) => { 
        state.status = 'loading'
      },
      [OPERATIONS_SUCCESS]: (state, resp) => {
        state.status = 'success'
        resp.items.forEach(i => state.operations.push(i))        
        state.totalOperations = resp.totalAmount
        state.cash = resp.cash
        state.card = resp.card
        state.average = resp.average
      },
      [OPERATIONS_ERROR]: (state) => {
        state.status = 'error'
        Vue.set(state, 'operations', [])
        state.totalOperations = 0
        state.cash = 0
        state.card = 0
        state.average = 0
      },
      [MOVE_SELECTED_DATE]: (state, shift) => {
        state.selectedDate = state.selectedDate.addDays(shift)
      },
}