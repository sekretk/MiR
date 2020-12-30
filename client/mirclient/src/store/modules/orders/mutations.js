import Vue from 'vue'
import { ORDERS_ERROR, ORDERS_REQUEST, ORDERS_REQUEST_MORE, ORDERS_SUCCESS } from './consts'

export default {
    [ORDERS_REQUEST]: (state) => {
        Vue.set(state, 'orders', [])
        state.totalOrders = 0
        state.status = 'loading'        
    },
    [ORDERS_REQUEST_MORE]: (state) => {
        state.status = 'loading'
    },
    [ORDERS_SUCCESS]: (state, resp) => {
        state.status = 'success'
        resp.items.forEach(i => state.orders.push(i))
        state.totalOrders = resp.totalAmount
    },
    [ORDERS_ERROR]: (state) => {
        state.status = 'error'
        Vue.set(state, 'orders', [])
        state.totalOrders = 0
    },
}