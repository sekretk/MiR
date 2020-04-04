export default {
    loading: state => state.status == 'loading',
    haveMoreOperations: state => state.operations.length < state.totalOperations,
    amount: state => state.operations.length,    
  }
  