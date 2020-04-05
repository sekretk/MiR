<template>
  <v-container>
    <v-row dense>
      <p class="ml-3 header">Главная</p>
    </v-row>
    <v-row no-gutters>
      <v-col sm="12" md="6">
        <v-card dark class="ma-1 amber darken-4" :loading="operationsLoading">
          <v-card-title class="headline">Продажи</v-card-title>

          <v-card-subtitle>Продажи за сегодня</v-card-subtitle>

          <v-card-text>
            <v-flex>
              <v-chip light class="ma-1">Всего: {{operationsResponse.totalAmount}}</v-chip>
              <v-chip
                light
                class="ma-1"
              >Выручка: {{operationsResponse.cash+operationsResponse.card | numFormat}}</v-chip>
              <v-chip light class="ma-1">Нал: {{operationsResponse.cash | numFormat}}</v-chip>
              <v-chip light class="ma-1">Безнал: {{operationsResponse.card | numFormat}}</v-chip>
              <v-chip light class="ma-1">Средний чек: {{operationsResponse.average.toFixed(2)}}</v-chip>
            </v-flex>
            <v-divider></v-divider>
            <v-list>
              <template v-for="(operation, index) in operationsResponse.items.slice(0,5)">
                <v-list-item :key="index">
                  <span>{{operation.acct}}</span>
                  <span class="ml-1">{{operation.date | onlyTime}}</span>
                  <span class="ml-1">позиц. {{operation.positionsCount}}</span>
                  <span class="ml-1">кол-во. {{operation.goodsAmount}}</span>
                  <span class="ml-1">Нал. {{operation.cash | numFormat}}</span>
                  <span class="ml-1">Безнал. {{operation.card | numFormat}}</span>
                </v-list-item>
                <v-divider :key="index+'_'"></v-divider>
              </template>
            </v-list>
          </v-card-text>

          <v-card-actions>
            <v-btn @click="goToOperations">К продажам</v-btn>
          </v-card-actions>
        </v-card>
      </v-col>

      <v-col sm="12" md="6">
        <v-card class="ma-1 green d8rken-4 black--text">
          <v-card-title class="headline">Заказ</v-card-title>

          <v-card-subtitle class="black--text">Позиции в заказе</v-card-subtitle>

          <v-card-text class="pa-0">
            <v-list class="ma-0 pa-0">
              <template v-for="(good, index) in topOrder">
                <v-list-item :key="index">{{good.good.name}} - {{good.count}}</v-list-item>
                <v-divider class="ma-0" :key="index+'_'"></v-divider>
              </template>
            </v-list>
          </v-card-text>

          <v-card-actions>
            <v-btn class="black--text" @click="goToCard">К заказу</v-btn>
          </v-card-actions>
        </v-card>
        <v-card class="ma-1" color="#385F73" dark :loading="changesLoading">
          <v-card-title class="headline">О приложении</v-card-title>

          <v-card-subtitle>Версия: {{Version}}, Дата: {{VersionDate}}</v-card-subtitle>

          <v-card-text>
            <v-list>
              <template v-for="(change, index) in topChanges">
                <v-list-item :key="index">{{change}}</v-list-item>
                <v-divider :key="index+'_'"></v-divider>
              </template>
            </v-list>
          </v-card-text>

          <v-card-actions>
            <v-btn @click="goToChanges">К изменениям</v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { BUILD_VERSION, BUILD_DATE } from "@/version";
import { mapGetters, mapState, mapActions } from "vuex";
import { GET_OPERATIONS_4_DATE } from "@/store/modules/operations/consts";

const emptyOperationsResponse = {
  totalAmount: 0,
  cash: 0,
  card: 0,
  average: 0,
  items: []
};

export default {
  data() {
    return {
      Version: BUILD_VERSION,
      VersionDate: BUILD_DATE,
      operationsResponse: emptyOperationsResponse,
      operationsLoading: false
    };
  },
  metaInfo: {
    title: "Главная"
  },
  methods: {
    ...mapActions("operations", {
      getOperations: GET_OPERATIONS_4_DATE
    }),
    goToChanges() {
      this.$router.push("/about");
    },
    goToCard() {
      this.$router.push("/card");
    },
    goToOperations() {
      this.$router.push("/operations");
    },
    getOperations4Today() {
      this.operationsLoading = true;
      this.getOperations(new Date())
        .then(resp => {
          this.operationsResponse = resp;
        })
        .finally(() => {
          this.operationsLoading = false;
          this.operationsResponse = emptyOperationsResponse;
        });
    }
  },
  computed: {
    ...mapGetters("app", ["topChanges", "topOrder"]),
    ...mapState("app", ["changesLoading", "currentObject"])
  },
  mounted() {
    this.getOperations4Today();
  },
  watch: {
    currentObject: function() {
      this.getOperations4Today();
    }
  }
};
</script>

<style scoped>
.v-list {
  background-color: rgba(0, 0, 0, 0.1) !important;
}
</style>