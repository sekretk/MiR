import { GOODS_REQUEST, GOODS_REQUEST_MORE, GOODS_SUCCESS, GOODS_ERROR } from './consts'
import apiCall from '@/utils/api'

export default {
    [GOODS_REQUEST]: ({ commit }, groupId) => {
        commit(GOODS_REQUEST)
        apiCall({ url: 'goods/list', data: {skip: 0, size: 10, groupId}  })
          .then(resp => {
            commit(GOODS_SUCCESS, resp)
          })
          .catch(() => {
            commit(GOODS_ERROR)
          })
      },
      [GOODS_REQUEST_MORE]: ({ getters, state, commit }) => {
        commit(GOODS_REQUEST_MORE)
        apiCall({ url: 'goods/list', data: {skip: getters.amount, size: 10, groupId: state.groupId}  })
          .then(resp => {
            commit(GOODS_SUCCESS, resp)
          })
          .catch(() => {
            commit(GOODS_ERROR)
          })
      },
  }