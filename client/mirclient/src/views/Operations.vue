<template>
  <div>
    <v-progress-linear
      :active="loading"
      :indeterminate="loading"
      absolute
      top
      color="deep-purple accent-4"
    ></v-progress-linear>
    <v-row align="center" class="mx-auto">
      <p class="header ma-0 ml-3">Операции</p>
      <v-spacer />
      <v-btn icon color="red darken-1" @click="shiftDate(-1)">
        <v-icon large>mdi-arrow-left-box</v-icon>
      </v-btn>
      <v-card class="ml-2 mr-2 pa-1">{{selectedDate | shortDate}}</v-card>
      <v-btn icon color="green darken-1" @click="shiftDate(1)" :disabled="!canGoFurther">
        <v-icon large>mdi-arrow-right-box</v-icon>
      </v-btn>
    </v-row>
    <v-flex class="ml-2">
      <v-chip light class="ma-1">Всего: {{totalOperations}}</v-chip>
      <v-chip light class="ma-1">Выручка: {{cash+card | numFormat}}</v-chip>
      <v-chip light class="ma-1">Нал: {{cash | numFormat}}</v-chip>
      <v-chip light class="ma-1">Безнал: {{card | numFormat}}</v-chip>
      <v-chip light class="ma-1">Средний чек: {{average.toFixed(2)}}</v-chip>
    </v-flex>
    <v-divider></v-divider>
    <v-list class="ml-2 pa-0">
      <template v-for="(operation, index) in operations">
        <v-list-item class="pa-0" :key="index">
          <v-list-item-content>
            <v-row align="center" no-gutters class="ml-2">
              <v-col>
                <v-row no-gutters>
                  <p class="operation-id">{{operation.acct}}</p>
                </v-row>
                <v-row no-gutters>{{operation.date | onlyTime}}</v-row>
              </v-col>
              <v-spacer></v-spacer>
              <span class="ma-1">Кол-во</span>
              <v-col class="ml-2 mr-2">
                <v-row class="ma-1" no-gutters>{{operation.positionsCount}}</v-row>
                <v-divider></v-divider>
                <v-row class="ma-1" no-gutters>{{operation.goodsAmount}}</v-row>
              </v-col>
              <v-spacer></v-spacer>
              <span class="ma-1">Оплата</span>
              <v-col>
                <v-row class="ma-1" no-gutters>{{operation.cash | numFormat}}</v-row>
                <v-divider></v-divider>
                <v-row class="ma-1" no-gutters>{{operation.card | numFormat}}</v-row>
              </v-col>
            </v-row>
          </v-list-item-content>
          <v-list-item-action>
            <v-btn class="mr-2" icon color="black" @click="openDetails(operation.acct)">
              <v-icon large>mdi-dots-horizontal</v-icon>
            </v-btn>
          </v-list-item-action>
        </v-list-item>
        <v-divider :key="index+'_'"></v-divider>
      </template>
    </v-list>
    <v-row>
      <v-spacer />
      <v-btn dark center :disabled="loading" v-if="haveMore" @click="getMore">
        <v-progress-circular indeterminate v-if="loading" color="primary"></v-progress-circular>
        <span v-else>Ещё ({{totalOperations - operations.length}})</span>
      </v-btn>
      <v-spacer />
    </v-row>
  </div>
</template>

<script>
import {
  OPERATIONS_REQUEST,
  OPERATIONS_REQUEST_MORE,
  MOVE_SELECTED_DATE
} from "@/store/modules/operations/consts";

import { mapActions, mapState, mapGetters, mapMutations } from "vuex";

export default {
  metaInfo: {
    title: "Операции"
  },
  data: () => ({}),
  methods: {
    ...mapActions("operations", {
      getOperations: OPERATIONS_REQUEST,
      getMore: OPERATIONS_REQUEST_MORE
    }),
    ...mapMutations("operations", {
      shiftDate: MOVE_SELECTED_DATE
    }),
    openDetails(operationId) {
      this.$router.push({
        name: "operationdetails",
        params: { id: operationId }
      });
    }
  },
  mounted() {
    this.getOperations();
  },
  computed: {
    ...mapState("operations", [
      "operations",
      "selectedDate",
      "totalOperations",
      "cash",
      "card",
      "average"
    ]),
    ...mapGetters("operations", {
      haveMore: "haveMoreOperations",
      loading: "loading"
    }),
    ...mapState("app", ["currentObject"]),
    canGoFurther: function() {
      return this.selectedDate < new Date().addDays(-1);
    }
  },
  watch: {
    currentObject: function() {
      this.getOperations();
    },
    selectedDate: function() {
      this.getOperations();
    }
  }
};
</script>

<style>
.operation-id {
  color: red;
  font-size: 8px;
  font-weight: 600;
}
</style>