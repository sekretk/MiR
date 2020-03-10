export default [
  {
    path: '',
    // Relative to /src/views
    view: 'Dashboard'
  },
  {
    path: '/operations',
    name: 'operations',
    view: 'Operations'
  },
  {
    path: '/operationdetails/:id',
    name: 'operationdetails',
    view: 'OperationDetails'
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
