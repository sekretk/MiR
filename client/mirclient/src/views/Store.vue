<template>
  <div class="goods-container">
    <h2>Склад</h2>
    <v-progress-linear
      :active="loading"
      :indeterminate="loading"
      absolute
      bottom
      color="deep-purple accent-4"
    ></v-progress-linear>
    <v-list>
      <template v-for="(good, index) in goods">
        <v-list-item :key="index" v-if="good.isGroup" class="group" @click="gotoGroup(good)">
          <p class="store-id">{{good.id}}</p>
          <v-list-item-content class="ml-4 pa-0">
            <p>{{good.name}}</p>
          </v-list-item-content>
          <v-list-item-action>
            <v-icon large color="green darken-3">mdi-folder</v-icon>
          </v-list-item-action>
        </v-list-item>
        <v-list-item :key="index" v-else>
          <p class="store-id">{{good.id}}</p>
          <v-list-item-content class="ml-4 pa-0">
            <p>{{good.name}}</p>
          </v-list-item-content>
          <v-list-item-action>
            <v-row class="pt-3">
              <div v-if="goodsOrderCount(good) == 0">
                <v-tooltip bottom>
                  <template v-slot:activator="{ on }">
                    <v-btn icon color="teal darken-1" v-on="on" @click="addOrder(good)">
                      <v-icon large>mdi-folder-plus</v-icon>
                    </v-btn>
                  </template>
                  <span class="white--text">Заказать</span>
                </v-tooltip>
              </div>
              <div v-else>
                <v-tooltip bottom>
                  <template v-slot:activator="{ on }">
                    <v-btn v-on="on" icon color="red darken-1" @click="changeCount({good, delta: -1})">
                      <v-icon large>mdi-minus-box</v-icon>
                    </v-btn>
                  </template>
                  <span class="white--text">Уменьшить</span>
                </v-tooltip>
                {{goodsOrderCount(good)}}
                <v-tooltip bottom>
                  <template v-slot:activator="{ on }">
                    <v-btn v-on="on" icon color="teal darken-1" @click="changeCount({good, delta: 1})">
                      <v-icon large>mdi-plus-box</v-icon>
                    </v-btn>
                  </template>
                  <span class="white--text">Добавить</span>
                </v-tooltip>
              </div>
            </v-row>
          </v-list-item-action>
          <p class="store-quantity">Кол-во: {{good.quantity}}</p>
        </v-list-item>

        <v-divider :key="index+'_'"></v-divider>
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
  GOODS_REQUEST,
  GOODS_REQUEST_MORE
} from "@/store/modules/goods/consts";

import { ADD_ORDER, CHANGE_ORDER } from "@/store/modules/app/consts";

import { mapActions, mapState, mapGetters, mapMutations } from "vuex";

export default {
  metaInfo: {
    title: "Склад"
  },
  methods: {
    ...mapActions("goods", {
      getGoods: GOODS_REQUEST,
      getMore: GOODS_REQUEST_MORE
    }),
    ...mapMutations("app", {
      addOrder: ADD_ORDER,
      changeCount: CHANGE_ORDER
    }),
    gotoGroup(good) {
      if (this.$route.params && good.id != this.$route.params.groupId)
        this.$router.push({ name: "store", params: { groupId: good.id } });
    },
  },
  mounted() {
    this.getGoods(this.$route.params ? this.$route.params.groupId : null);
  },
  watch: {
    $route(to) {
      this.getGoods(to.params ? to.params.groupId : null);
    },
    currentObject: function() {
      this.getOperations();
    }
  },
  computed: {
    ...mapState("goods", ["goods"]),
    ...mapGetters("goods", { haveMore: "haveMoreGoods", loading: "loading" }),
    ...mapGetters("app", { goodsOrderCount: "goodInOrder" }),
    ...mapState("app", ["currentObject"])
  }
};
</script>