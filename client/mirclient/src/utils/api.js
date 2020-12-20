import axios from 'axios'
import { LOG_MESSAGE } from '@/utils/events';
import EventBus from '@/plugins/eventbus.js';
import store from '@/store'
import Router from '@/router'; 
import { AUTH_LOGOUT } from '@/store/modules/auth/consts'

axios.defaults.baseURL = `http://${process.env.VUE_APP_MIR_API_URL}`

axios.interceptors.response.use(response => {
  return response;
}, error => {
  if (error.config && error.config.url)
    EventBus.$emit(LOG_MESSAGE, `Ошибка при выполнение запроса к API ${error.config.url}.` + error);
  else
    EventBus.$emit(LOG_MESSAGE, `Ошибка при выполнение запроса AXIOS ${error}.` + error);

  if (error && error.response && error.response.status == 401) {
    
    //todo: save current URL       
    store.commit(`auth/${AUTH_LOGOUT}`)
    Router.push({name: "login", params: { apiAuth: true }}); //push to login
  }
  else //on other
    throw error
  return error;
});

const mockTimeout = 1000;

const mocks = [
  //{ url: 'auth/login', method: 'POST', result: { token: 'This-is-a-mocked-token' } },
  //{ url: 'auth/signout', method: 'DELETE', result: {} },
  //{ url: 'users/me', method: 'GET', result: { name: 'Администратор', role: 'admin', avatar: 'andy-warhol-icon' } },
  {
    url: 'notifications/top', method: 'GET', result: [
      {
        title: "Создан новый пользователь",
        color: "light-green",
        icon: "account_circle",
        timeLabel: "Только что"
      },
      {
        title: "Отредактирована анкета",
        color: "light-green",
        icon: "payment",
        timeLabel: "25 мин назад"
      },
    ]
  },
  {
    url: 'notifications/all', method: 'GET', result: [
      {
        title: "Создан новый пользователь",
        color: "light-green",
        icon: "account_circle",
        timeLabel: "Только что"
      },
      {
        title: "Отредактирована анкета",
        color: "light-green",
        icon: "payment",
        timeLabel: "25 мин назад"
      },
      {
        title: "Получено новое уведомление",
        color: "red",
        icon: "email",
        timeLabel: "1 час назад"
      },
    ]
  },
  //{ url: 'glossary', method: 'GET', result: [{ weight: 3, explanation: "", word: "Lorem" }, { weight: 3, explanation: "", word: "Ipsum" }, { weight: 3, explanation: "", word: "is" }, { weight: 3, explanation: "", word: "simply" }, { weight: 3, explanation: "", word: "dummy" }, { weight: 3, explanation: "", word: "text" }, { weight: 3, explanation: "", word: "of" }, { weight: 3, explanation: "", word: "the" }, { weight: 3, explanation: "", word: "printing" }, { weight: 3, explanation: "", word: "and" }, { weight: 3, explanation: "", word: "typesetting" }, { weight: 3, explanation: "", word: "industry." }, { weight: 3, explanation: "", word: "Lorem" }, { weight: 3, explanation: "", word: "Ipsum" }, { weight: 3, explanation: "", word: "has" }, { weight: 3, explanation: "", word: "been" }, { weight: 3, explanation: "", word: "the" }, { weight: 3, explanation: "", word: "industry's" }, { weight: 3, explanation: "", word: "standard" }, { weight: 3, explanation: "", word: "dummy" }, { weight: 7, explanation: "", word: "text" }, { weight: 3, explanation: "", word: "ever" }, { weight: 3, explanation: "", word: "since" }, { weight: 3, explanation: "", word: "the" }, { weight: 3, explanation: "", word: "1500s," }, { weight: 3, explanation: "", word: "when" }, { weight: 3, explanation: "", word: "an" }, { weight: 9, explanation: "", word: "unknown" }, { weight: 3, explanation: "", word: "printer" }, { weight: 3, explanation: "", word: "took" }, { weight: 3, explanation: "", word: "a" }, { weight: 3, explanation: "", word: "galley" }, { weight: 3, explanation: "", word: "of" }, { weight: 3, explanation: "", word: "type" }, { weight: 3, explanation: "", word: "and" }, { weight: 3, explanation: "", word: "scrambled" }, { weight: 3, explanation: "", word: "it" }, { weight: 2, explanation: "", word: "to" }, { weight: 3, explanation: "", word: "make" }, { weight: 3, explanation: "", word: "a" }, { weight: 3, explanation: "", word: "type" }, { weight: 3, explanation: "", word: "specimen" }, { weight: 3, explanation: "", word: "book." }, { weight: 7, explanation: "", word: "It" }, { weight: 3, explanation: "", word: "has" }, { weight: 3, explanation: "", word: "survived" }, { weight: 3, explanation: "", word: "not" }, { weight: 3, explanation: "", word: "only" }, { weight: 3, explanation: "", word: "five" }, { weight: 3, explanation: "", word: "centuries," }, { weight: 3, explanation: "", word: "but" }, { weight: 6, explanation: "", word: "also" }, { weight: 3, explanation: "", word: "the" }, { weight: 3, explanation: "", word: "leap" }, { weight: 3, explanation: "", word: "into" }, { weight: 3, explanation: "", word: "electronic" }, { weight: 2, explanation: "", word: "typesetting," }, { weight: 3, explanation: "", word: "remaining" }, { weight: 3, explanation: "", word: "essentially" }, { weight: 3, explanation: "", word: "unchanged." }, { weight: 3, explanation: "", word: "It" }, { weight: 3, explanation: "", word: "was" }, { weight: 3, explanation: "", word: "popularised" }, { weight: 3, explanation: "", word: "in" }, { weight: 3, explanation: "", word: "the" }, { weight: 3, explanation: "", word: "1960s" }, { weight: 3, explanation: "", word: "with" }, { weight: 3, explanation: "", word: "the" }, { weight: 11, explanation: "", word: "release" }, { weight: 3, explanation: "", word: "of" }, { weight: 1, explanation: "", word: "Letraset" }, { weight: 3, explanation: "", word: "sheets" }, { weight: 3, explanation: "", word: "containing" }, { weight: 3, explanation: "", word: "Lorem" }, { weight: 3, explanation: "", word: "Ipsum" }, { weight: 3, explanation: "", word: "passages," }, { weight: 3, explanation: "", word: "and" }, { weight: 3, explanation: "", word: "more" }, { weight: 3, explanation: "", word: "recently" }, { weight: 3, explanation: "", word: "with" }, { weight: 3, explanation: "", word: "desktop" }, { weight: 3, explanation: "", word: "publishing" }, { weight: 3, explanation: "", word: "software" }, { weight: 3, explanation: "", word: "like" }, { weight: 3, explanation: "", word: "Aldus" }, { weight: 3, explanation: "", word: "PageMaker" }, { weight: 3, explanation: "", word: "including" }, { weight: 3, explanation: "", word: "versions" }, { weight: 3, explanation: "", word: "of" }, { weight: 3, explanation: "", word: "Lorem" }, { weight: 3, explanation: "", word: "Ipsum" }] },
  //{ url: 'glossary/add', method: 'POST', result: { weight: 3, word: "newadded", explanation: "" } },
  //{ url: 'glossary/remove', method: 'DELETE', result: {} },
  { url: 'logs/send', method: 'POST', result: {} },
  { url: 'users/avatars', method: 'GET', result: ['andy-warhol-icon', 'barack-obama-icon', 'batman-icon', 'cristiano-ronaldo-icon', 'indian-woman-icon', 'joseph-stalin-icon', 'mahatma-gandhi-icon'] },
  // {
  //   url: 'users/list', method: 'GET', result: [
  //     { Name: 'liza', Fullname: 'liza simpsons', Description: 'lovely girl', Avatar: 'batman-icon' },
  //     { Name: 'home', Fullname: 'home simpsons', Description: 'lazy asshole', Avatar: 'indian-woman-icon' },
  //     { Name: 'marge', Fullname: 'marge simpsons', Description: 'house wife', Avatar: 'barack-obama-icon' }]
  // },
  //{ url: 'users/add', method: 'POST', result: { name: 'liza', fullname: 'liza simpsons', description: 'lovely girl', avatar: 'batman-icon' } },
  //{ url: 'users/remove', method: 'DELETE', result: {} },
  //{ url: 'users/changePassword', method: 'POST', result: {} },
]

const mockApiCall = (mockData) =>
  new Promise((resolve, reject) => {
    setTimeout(() => {
      try {
        resolve(mockData)
        //console.log(`Mocked '${url}' - '${method}'}`)
        //console.log('response: ', mockData)
      } catch (err) {
        reject(new Error(err))
      }
    }, mockTimeout)
  })

const apiCall = ({ url, method, data }) => {
  const mockItem = mocks.find(m => m.url == url && (m.method == method ? method : 'GET'))

  const mockData = mockItem ? mockItem.result : null

  if (mockData)
    return mockApiCall(mockData, url, method)

  let axiosCall = null

  switch (method) {
    case 'POST':
      axiosCall = axios({
        url, method: 'POST', data
      })

      break
    default: //GET+DELETE
      axiosCall = axios({
        url, method, params: data
      })
  }

  return axiosCall.then(resp => 
    {
        return resp.data;
    })
    .catch(function (error) {
      EventBus.$emit(LOG_MESSAGE, `Ошибка при выполнение запроса к API ${url}.` + error);
      throw error
    })
}

export default apiCall
