import Vue from 'vue'
import Vuetify from 'vuetify/lib'
import theme from './theme'

Vue.use(Vuetify)

const opts = {iconfont: 'mdi',
theme}

export default new Vuetify(opts)