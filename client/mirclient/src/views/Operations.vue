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
    <v-list class="ml-2">
      <v-row no-gutters>
        <v-col cols="2">Время</v-col>
        <v-col cols="2">Позиц</v-col>
        <v-col cols="2">Кол-во</v-col>
        <v-col cols="2">Нал</v-col>
        <v-col cols="2">Безнал</v-col>
        <v-col cols="2"></v-col>
      </v-row>
      <template v-for="(operation, index) in operations">
        <v-row :key="index">
          <p class="operation-id">{{operation.acct}}</p>
          <v-col cols="2">{{operation.date | onlyTime}}</v-col>
          <v-col cols="2">{{operation.positionsCount}}</v-col>
          <v-col cols="2">{{operation.goodsAmount}}</v-col>
          <v-col cols="2">{{operation.cash | numFormat}}</v-col>
          <v-col cols="2">{{operation.card | numFormat}}</v-col>
          <v-col cols="2">
            <v-btn icon @click="openDetails(operation.acct)">
              <v-icon large>mdi-arrow-right</v-icon>
            </v-btn>
          </v-col>
          <v-divider :key="index+'_'" inset></v-divider>
        </v-row>
      </template>
    </v-list>
    <v-row>
      <v-spacer />
      <v-btn dark center v-if="haveMore" @click="getMore">Ещё</v-btn>
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
  left: 15px;
  top: 3px;
  font-size: 8px;
  font-weight: 600;
}
</style>