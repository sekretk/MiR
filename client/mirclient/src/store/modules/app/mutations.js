import {
  set,
  toggle
} from '@/utils/vuex'
import {
  ADD_ORDER,
  CHANGE_ORDER,
  OBJECTS_REQUEST,
  SET_OBJECT,
  REQUEST_CHANGES,
  CHANGES_RESPONSE,
  CLEAR_ORDER
} from './consts'
import Vue from 'vue'

export default {
  setDrawer: set('drawer'),
  setColor: set('color'),
  toggleDrawer: toggle('drawer'),
  [ADD_ORDER]: (state, good) => {

    const foundgood = state.order.find(oi => oi.good.id == good.id)

    const idx = state.order.indexOf(foundgood)

    if (idx >= 0) {
      state.order[idx].count += 1
    } else {
      state.order.push({
        good,
        count: 1
      })
    }
  },
  [CHANGE_ORDER]: (state, {
    good,
    delta
  }) => {
    const foundgood = state.order.find(oi => oi.good == good)

    const idx = state.order.indexOf(foundgood)

    if (idx < 0)
      return;

    if ((state.order[idx].count + delta) <= 0) {
      Vue.delete(state.order, idx);
    } else {
      state.order[idx].count += delta
    }
  },

  [OBJECTS_REQUEST]: (state, objects) => {
    Vue.set(state, 'objects', objects)

    if (!objects)
      return

    const objectId = localStorage.getItem("objectId")

    const foundObject = objects.find(o => o.id == objectId)

    const idx = objects.indexOf(foundObject)

    if (idx < 0 && objects && objects.length > 0) {
      state.currentObject = objects[0].id
    } else {
      state.currentObject = objects[idx].id
    }

    localStorage.setItem("objectId", state.currentObject.id)
  },

  [SET_OBJECT]: (state, object) => {
    localStorage.setItem("objectId", object)
    state.currentObject = object
  },
  [REQUEST_CHANGES]: (state) => {
    state.changesLoading = true
  },
  [CHANGES_RESPONSE]: (state, response) => {
    state.changesLoading = false
    state.changes = response.changes
    state.serverVersion = `Версия: ${response.version}, Дата: ${response.date}`
  },
  [CLEAR_ORDER]: (state) => {
    Vue.set(state, 'order', [])
  }

}