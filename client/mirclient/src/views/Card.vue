<template>
  <div class="goods-container">
    <v-row align="center">
      <h2 class="ml-2">Заказ</h2>
      <v-spacer></v-spacer>
      <v-btn center color="indigo" class="mr-5" @click="createOrder">Создать заказ</v-btn>
      <v-btn center color="purple" class="mr-5" @click="gotoOrders">Перейти к заказам</v-btn>
    </v-row>
    <div class="ml-2 mr-2" v-if="goods.length == 0">
      <p>Ещё не выбраны позиции</p>
      <v-row align="center" justify="center">
        <v-btn class="red--text" color="tean darken-1" @click="gotoStore">Перейти к складу</v-btn>
      </v-row>
    </div>
    <v-list v-else>
      <template v-for="(good, index) in goods">
        <v-list-item :key="index">
          <p class="store-id">{{good.good.id}}</p>
          <v-list-item-content class="ml-4 pa-0">
            <p>{{good.good.name}}</p>
          </v-list-item-content>
          <v-list-item-action>
            <v-row align="center" justify="center">
              <v-tooltip bottom>
                <template v-slot:activator="{ on }">
                  <v-btn
                    v-on="on"
                    icon
                    color="red darken-1"
                    @click="changeCount({good: good.good, delta: -1})"
                  >
                    <v-icon large>mdi-minus-box</v-icon>
                  </v-btn>
                </template>
                <span class="white--text">Уменьшить</span>
              </v-tooltip>
              {{good.count}}
              <v-tooltip bottom>
                <template v-slot:activator="{ on }">
                  <v-btn
                    v-on="on"
                    icon
                    color="teal darken-1"
                    @click="changeCount({good: good.good, delta: 1})"
                  >
                    <v-icon large>mdi-plus-box</v-icon>
                  </v-btn>
                </template>
                <span class="white--text">Добавить</span>
              </v-tooltip>
            </v-row>
          </v-list-item-action>
        </v-list-item>
        <v-divider :key="index+'_'"></v-divider>
      </template>
    </v-list>
  </div>
</template>

<script>
import { CHANGE_ORDER, CREATE_ORDER } from "@/store/modules/app/consts";

import { mapState, mapMutations, mapActions } from "vuex";

export default {
  metaInfo: {
    title: "Заказ"
  },
  methods: {
    ...mapMutations("app", {
      changeCount: CHANGE_ORDER
    }),
    ...mapActions("app", { createOrder: CREATE_ORDER }),
    gotoStore() {
      this.$router.push({ name: "store" });
    },
    gotoOrders() {
       this.$router.push({ name: "orders" });
    }
  },
  computed: {
    ...mapState("app", { goods: "order" })
  }
};
</script>

<style scoped>
.group {
  background-color: lightblue;
}
</style>