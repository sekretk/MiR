<template>
  <div>
    <h2>Содержимое продажи: {{$route.params.operationId}}</h2>
    <v-btn class="black--text">
      <v-icon>mdi-arrow-left-bold</v-icon>Обратно
    </v-btn>
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
import apiCall from '@/utils/api'

export default {
  metaInfo: {
    title: "Содержимое операции"
  },
  data: () => ({
    positions: []
  }),
  methods: {   
      getOperationDetails: (operationId) => apiCall({ url: 'operations/positions', data: { operationAcct: operationId } })      
  },
  mounted() {
    this.getOperationDetails(this.$route.params.id).then(resp => (this.positions = resp));
  }
};
</script>

<style>
</style>