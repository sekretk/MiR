import { AUTH_REQUEST, AUTH_ERROR, AUTH_SUCCESS, AUTH_LOGOUT } from './consts'
import { LOG_MESSAGE } from '@/utils/events';
import { USER_REQUEST } from '@/store/modules/user/consts'
import axios from 'axios'
import apiCall from '@/utils/api'
import EventBus from '@/plugins/eventbus.js';

export default {
  [AUTH_REQUEST]: ({ commit, dispatch }, user) => {
    return new Promise((resolve, reject) => {
      commit(AUTH_REQUEST)

      if (user.user == "error") {
        reject("неверный логин или пароль")
        return
      }

      apiCall({ url: 'auth/login', data: user, method: 'POST' })
        .then(resp => {
          localStorage.setItem('user-token', resp.token)
          axios.defaults.headers.common['Authorization'] = resp.token
          commit(AUTH_SUCCESS, resp)
          dispatch(`user/${USER_REQUEST}`, null, { root: true })
          resolve(resp)
        })
        .catch(err => {
          commit(AUTH_ERROR, err)
          localStorage.removeItem('user-token')

          if (err && err.response && err.response.status == 400)
            reject("Неверный логин или пароль")
          else
            reject("Неудачная попытка регистрации")
        })
    })
  },
  [AUTH_LOGOUT]: ({ commit }) => {
    return new Promise((resolve) => {
      localStorage.removeItem('user-token')
      commit(AUTH_LOGOUT)
      apiCall({ url: 'auth/signout', method: 'DELETE' })
        .then(() => {
          EventBus.$emit(LOG_MESSAGE, `Сессия с токеном ${localStorage.getItem('user-token')} успешно завершена`);
        })
        .catch(() => {
          EventBus.$emit(LOG_MESSAGE, `Сессия с токеном ${localStorage.getItem('user-token')} ошибочно завершена`);
        })
        .finally(() => {
          resolve()
        })
    })
  }
}