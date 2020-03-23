<template>
  <div>
    <v-row align="center" class="mx-auto">
      <h2 class="ml-5">Операции</h2>
      <v-spacer />
      <v-btn text icon color="red darken-1" @click="shiftDate(-1)">
        <v-icon>mdi-arrow-left-box</v-icon>
      </v-btn>
      <v-card class="mx-auto">{{selectedDate | shortDate}}</v-card>
      <v-btn text icon color="green darken-1" @click="shiftDate(1)" :disabled="!canGoFurther">
        <v-icon>mdi-arrow-right-box</v-icon>
      </v-btn>
    </v-row>
    <v-row>
      <span  class="ml-5">Всего операций: {{totalOperations}}</span>| 
      <span>Нал: {{cash}}</span>|
      <span>Безнал: {{card}}</span>|
      <span>Средний чек: {{average}}</span>
    </v-row>
    <v-progress-linear
      :active="loading"
      :indeterminate="loading"
      absolute
      bottom
      color="deep-purple accent-4"
    ></v-progress-linear>
    <v-list three-line>
      <template v-for="(operation, index) in operations">
        <v-list-item :key="index" @click="openDetails(operation.acct)">
          <span>{{operation.acct}}</span>          
          <span>в {{operation.date | onlyTime}}</span>
          <span>позиц. {{operation.positionsCount}}</span>
          <span>кол-во. {{operation.goodsAmount}}</span>
          <span>Нал. {{operation.cash}}</span>
          <span>Безнал. {{operation.card}}</span>
        </v-list-item>

        <v-divider :key="index+'_'" inset></v-divider>
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
    ...mapState("operations", ["operations", "selectedDate", "totalOperations", "cash", "card", "average"]),
    ...mapGetters("operations", {
      haveMore: "haveMoreOperations",
      loading: "loading"
    }),
    ...mapState("app", ["currentObject"]),
    canGoFurther: function() {
      return this.selectedDate < (new Date()).addDays(-1);
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
</style>