<template>
  <div>
    <v-row class="ml-3" align="center">
      <v-btn center icon color="indigo" @click="goBack">
        <v-icon large>mdi-arrow-left-bold</v-icon>
      </v-btn>
      <p class="header ma-2">Продажа: {{$route.params.operationId}}</p>
    </v-row>
     <v-divider></v-divider>
    <v-list>
      <template v-for="(position, index) in positions">
        <v-list-item :key="index">
          <v-list-item-content>
            <v-list-item-title>{{position.id}}</v-list-item-title>
            <v-list-item-subtitle>{{position.name}}</v-list-item-subtitle>
          </v-list-item-content>

          <v-spacer></v-spacer>

          <v-list-item-action>{{position.qtty}}</v-list-item-action>
        </v-list-item>

        <v-divider :key="index+'_'"></v-divider>
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
      getOperationDetails: (operationId) => apiCall({ url: 'operations/positions', data: { operationAcct: operationId } }),
      goBack(){
        this.$router.push("/operations")
      }
  },
  mounted() {
    this.getOperationDetails(this.$route.params.id).then(resp => (this.positions = resp));
  }
};
</script>

<style>
</style>