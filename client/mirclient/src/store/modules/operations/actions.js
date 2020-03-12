import {
  OPERATIONS_REQUEST,
  OPERATIONS_REQUEST_MORE, 
  OPERATIONS_SUCCESS, 
  OPERATIONS_ERROR
} from './consts'
import apiCall from '@/utils/api'

export default {
  [OPERATIONS_REQUEST]: ({ commit }) => {
    commit(OPERATIONS_REQUEST)
    apiCall({ url: 'operations/list', data: { skip: 0, size: 10 } })
      .then(resp => {
        commit(OPERATIONS_SUCCESS, resp)
      })
      .catch(() => {
        commit(OPERATIONS_ERROR)
      })
  },
  [OPERATIONS_REQUEST_MORE]: ({ getters, commit }) => {
    commit(OPERATIONS_REQUEST_MORE)
    apiCall({ url: 'operations/list', data: { skip: getters.amount, size: 10 } })
      .then(resp => {
        commit(OPERATIONS_SUCCESS, resp)
      })
      .catch(() => {
        commit(OPERATIONS_ERROR)
      })
  },
}