<template>
  <div class="orders-container">
    <v-row align="center">
      <h2 class="ml-2">Заказы</h2>
    </v-row>
    <div class="ml-2 mr-2" v-if="orders.length == 0">
      <p>Нет заказов</p>
      <v-row align="center" justify="center">
        <v-btn class="red--text" color="tean darken-1" @click="gotoStore"
          >Перейти к складу</v-btn
        >
      </v-row>
    </div>
    <v-list v-else>
      <template v-for="(order, index) in orders">
        <v-list-item :key="index">
          <p class="store-id">{{ order.id }}</p>
          <v-list-item-content class="ml-4 pa-0">
            <p>{{ order.created | formatDate }}</p>
          </v-list-item-content>
        </v-list-item>
        <v-divider :key="index + '_'"></v-divider>
      </template>
    </v-list>
    <v-progress-linear
      :active="loading"
      :indeterminate="loading"
      absolute
      bottom
      color="deep-purple accent-4"
    ></v-progress-linear>
    <v-row>
      <v-spacer />
      <v-btn dark center v-if="haveMore" @click="getMore"
        >Ещё ({{ totalOrders - orders.length }})</v-btn
      >
      <v-spacer />
    </v-row>
  </div>
</template>

<script>
import { mapState, mapActions, mapGetters } from "vuex";
import {
  ORDERS_REQUEST,
  ORDERS_REQUEST_MORE,
} from "@/store/modules/orders/consts";

export default {
  metaInfo: {
    title: "Заказы",
  },
  computed: {
    ...mapState("orders", ["orders"]),
    ...mapGetters("orders", {
      haveMore: "haveMoreOrders",
      loading: "loading",
      totalOrders: "amount",
    }),
  },
  mounted() {
    this.getOrders();
  },
  methods: {
    gotoStore() {
      this.$router.push({ name: "store" });
    },
    ...mapActions("orders", {
      getOrders: ORDERS_REQUEST,
      getMore: ORDERS_REQUEST_MORE,
    }),
  },
  watch: {
    $route() {
      this.getOrders();
    },
  },
};
</script>