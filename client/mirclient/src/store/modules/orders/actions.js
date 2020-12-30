import apiCall from '@/utils/api'
import { ORDERS_ERROR, ORDERS_REQUEST, ORDERS_REQUEST_MORE, ORDERS_SUCCESS } from './consts'

export default {
    [ORDERS_REQUEST]: ({ commit }) => {
        commit(ORDERS_REQUEST)
        apiCall({ url: 'order/list', data: {skip: 0, size: 10}  })
          .then(resp => {
            commit(ORDERS_SUCCESS, resp)
          })
          .catch(() => {
            commit(ORDERS_ERROR)
          })
      },
      [ORDERS_REQUEST_MORE]: ({ getters, commit }) => {
        commit(ORDERS_REQUEST_MORE)
        apiCall({ url: 'order/list', data: {skip: getters.amount, size: 10}  })
          .then(resp => {
            commit(ORDERS_SUCCESS, resp)
          })
          .catch(() => {
            commit(ORDERS_ERROR)
          })
      },
  }