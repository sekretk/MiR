export default {
    orderAmount: state => state.order.length,
    goodInOrder(state) {
        return good => {
            const foundgood = state.order.find(oi => oi.good.id == good.id)

            const idx = state.order.indexOf(foundgood)
        
            if (idx >= 0) {
              return state.order[idx].count
            }
            else {
              return 0
            }
    }
    }
}