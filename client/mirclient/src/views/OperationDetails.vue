<template>
  <div>
    <h2>Содержимое продажи: {{$route.params.operationId}}</h2>
    <v-list three-line>
      <template v-for="(position, index) in positions">
        <v-list-item :key="index">
          <v-list-item-content>
            <v-list-item-title>{{position.id}}</v-list-item-title>
            <v-list-item-subtitle>{{position.name}}</v-list-item-subtitle>
          </v-list-item-content>

          <v-spacer></v-spacer>

          <v-list-item-action>{{position.count}}</v-list-item-action>
        </v-list-item>

        <v-divider :key="index+'_'" inset></v-divider>
      </template>
    </v-list>
  </div>
</template>

<script>
import { OPERATION_DETAILS_REQUEST } from "@/store/modules/operations/consts"

import { mapActions } from "vuex";

export default {
  metaInfo: {
    title: "Содержимое операции"
  },
  methods: {
    ...mapActions("operations", {
      getOperationDetails: OPERATION_DETAILS_REQUEST,
    })
  },
    mounted() {
    this.getOperationDetails();
  },
};
</script>

<style>
</style>