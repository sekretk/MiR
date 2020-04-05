import { GOODS_REQUEST, GOODS_REQUEST_MORE, GOODS_SUCCESS, GOODS_ERROR } from './consts'
import Vue from 'vue'

export default {
    [GOODS_REQUEST]: (state) => {
        Vue.set(state, 'goods', [])
        state.totalGoods = 0
        state.status = 'loading'        
    },
    [GOODS_REQUEST_MORE]: (state) => {
        state.status = 'loading'
    },
    [GOODS_SUCCESS]: (state, resp) => {
        state.status = 'success'
        resp.items.forEach(i => state.goods.push(i))
        state.totalGoods = resp.totalAmount
        state.groupId = resp.groupId
        state.parentGroupId = resp.parentGroupId
    },
    [GOODS_ERROR]: (state) => {
        state.status = 'error'
        Vue.set(state, 'goods', [])
        state.totalGoods = 0
        state.groupId = -1
        state.parentGroupId = null
    },
}