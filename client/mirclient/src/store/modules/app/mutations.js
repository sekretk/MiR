import { set, toggle } from '@/utils/vuex'
import { ADD_ORDER } from './consts'

export default {
  setDrawer: set('drawer'),
  setColor: set('color'),
  toggleDrawer: toggle('drawer'),
  [ADD_ORDER]: (state, good) => {
    state.order.push(good)
  },
}
