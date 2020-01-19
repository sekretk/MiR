import { USER_REQUEST, USER_ERROR, USER_SUCCESS } from './consts'
import apiCall from '@/utils/api'

export default {
    [USER_REQUEST]: ({ commit }) => {
        commit(USER_REQUEST)
        apiCall({ url: 'users/me' })
          .then(resp => {
            commit(USER_SUCCESS, resp)
          })
          .catch(() => {
            commit(USER_ERROR)
          })
      },
  }