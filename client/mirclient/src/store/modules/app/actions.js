import { OBJECTS_REQUEST, PING } from './consts'
import apiCall from '@/utils/api'

export default {
    [OBJECTS_REQUEST]: ({ commit }) => {
        apiCall({ url: 'system/objects' })
          .then(resp => {
            commit(OBJECTS_REQUEST, resp)
          })
      },
      [PING]: () => {
        apiCall({ url: 'system/ping' }) .then(() => {
          
        })
      }
  }