import { OPERATIONS_REQUEST, OPERATIONS_SUCCESS, OPERATIONS_ERROR } from './consts'
import apiCall from '@/utils/api'

export default {
    [OPERATIONS_REQUEST]: ({ getters, commit }) => {
        commit(OPERATIONS_REQUEST)
        apiCall({ url: 'operations/list', data: {skip: getters.amount, size: 10}  })
          .then(resp => {
            commit(OPERATIONS_SUCCESS, resp)
          })
          .catch(() => {
            commit(OPERATIONS_ERROR)
          })
      },
  }