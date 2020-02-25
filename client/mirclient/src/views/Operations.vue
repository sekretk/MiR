<template>
<div>
  <h2>Operations</h2>
   <v-list three-line>
      <template v-for="(operation, index) in operations">       

        <v-list-item
          :key="index"
        >

          <v-list-item-content>
            <v-list-item-title v-html="operation.userRealTime"></v-list-item-title>
            <v-list-item-subtitle v-html="operation.operType"></v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>

        <v-divider
          :key="index+'_'"
          inset
        ></v-divider>
      </template>
    </v-list>
    </div>
</template>

<script>
import { OPERATIONS_REQUEST } from "@/store/modules/operations/consts";

import { mapActions, mapState } from "vuex";

export default {
  metaInfo: {
    title: "Операции"
  },
   methods: {    
  ...mapActions("operations", { getOperations: OPERATIONS_REQUEST }),
   },
    mounted() {
    this.getOperations();
  },
   computed: {
    ...mapState('operations', ['operations']),
  },
};
</script>

<style>
</style>