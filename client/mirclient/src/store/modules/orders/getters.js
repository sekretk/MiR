export default {
    loading: state => state.status === "loading",
    haveMoreOrders: state => state.orders.length < state.totalOrders,
    amount: state => state.orders.length
  }