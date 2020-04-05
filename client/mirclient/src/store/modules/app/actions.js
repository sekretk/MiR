import {
  OBJECTS_REQUEST,
  PING,
  REQUEST_CHANGES,
  CHANGES_RESPONSE
} from './consts'
import apiCall from '@/utils/api'

export default {
  [OBJECTS_REQUEST]: ({
    commit
  }) => {
    apiCall({
        url: 'system/objects'
      })
      .then(resp => {
        commit(OBJECTS_REQUEST, resp)
      })
  },
  [PING]: () => {
    apiCall({
      url: 'system/ping'
    }).then(() => {

    })
  },
  [REQUEST_CHANGES]: ({
    commit
  }) => {
    commit(REQUEST_CHANGES)
    apiCall({
      url: 'system/version'
    }).then((resp) => {
      commit(CHANGES_RESPONSE, resp)
    }).catch(() =>
      commit(CHANGES_RESPONSE, {
        version: "na",
        date: "na",
        changes: []
      })
    )
  }
}