export default {
    loading: state => state.status === "loading",
    haveMoreGoods: state => state.goods.length < state.totalGoods,
    amount: state => state.goods.length,
    haveParent: state => !!state.parentGroupId
  }