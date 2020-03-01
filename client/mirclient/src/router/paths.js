export default [
  {
    path: '',
    // Relative to /src/views
    view: 'Dashboard'
  },
  {
    path: '/operations',
    // name: 'Продажи',
    view: 'Operations'
  },
  {
    path: '/store/:groupId?',
    name: 'store',
    view: 'Store'
  },
  {
    path: '/card',
    // name: 'Заказ',
    view: 'Card'
  },
]
