import {
  OPERATIONS_REQUEST,
  GET_OPERATIONS_4_DATE,
  OPERATIONS_REQUEST_MORE, 
  OPERATIONS_SUCCESS, 
  OPERATIONS_ERROR
} from './consts'
import apiCall from '@/utils/api'

export default {
  [OPERATIONS_REQUEST]: ({ commit, state, rootState  }) => {
    commit(OPERATIONS_REQUEST)
    apiCall({ url: 'operations/list', data: { skip: 0, size: 10, objectId: rootState.app.currentObject, date: state.selectedDate  } })
      .then(resp => {
        commit(OPERATIONS_SUCCESS, resp)
      })
      .catch(() => {
        commit(OPERATIONS_ERROR)
      })
  },
  [GET_OPERATIONS_4_DATE]: ({ rootState }, date) => 
    apiCall({ url: 'operations/list', data: { skip: 0, size: 10, objectId: rootState.app.currentObject, date  } })
  ,
  [OPERATIONS_REQUEST_MORE]: ({ getters, commit, state, rootState }) => {
    commit(OPERATIONS_REQUEST_MORE)
    apiCall({ url: 'operations/list', data: { skip: getters.amount, size: 10, objectId: rootState.app.currentObject, date: state.selectedDate } })
      .then(resp => {
        commit(OPERATIONS_SUCCESS, resp)
      })
      .catch(() => {
        commit(OPERATIONS_ERROR)
      })
  },
}