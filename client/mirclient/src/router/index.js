import Vue from 'vue'
import Router from 'vue-router'

import paths from './paths'

import store from '@/store/'

function requireAuth (to, from, next) {
  if (!store.getters['auth/isAuthenticated']) {
    next('/login')
  } else {
    next()
  }
}

function route (path) {
  return {
    name: path.name || path.view,
    path: path.path,
    component: (resolve) => import(
      `@/views/${path.view}.vue`
    ).then(resolve),
    beforeEnter: requireAuth
  }
}

Vue.use(Router)

const router = new Router({
  mode: 'history',
  routes: paths
        .map(path => route(path))
        .concat([
            { path: '*', redirect: '/' },
            { path: '/login', name: 'login' },
            ]),
  scrollBehavior (to, from, savedPosition) {
    if (savedPosition) {
      return savedPosition
    }
    if (to.hash) {
      return { selector: to.hash }
    }
    return { x: 0, y: 0 }
  }
})

export default router
