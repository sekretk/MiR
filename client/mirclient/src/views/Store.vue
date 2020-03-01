<template>
  <div>
    <h2>Склад</h2>
    <v-progress-linear
      :active="loading"
      :indeterminate="loading"
      absolute
      bottom
      color="deep-purple accent-4"
    ></v-progress-linear>
    <v-list three-line>
      <template v-for="(good, index) in goods">
        <v-list-item :key="index" v-if="good.isGroup" class="group" @click="gotoGroup(good)">
          <v-list-item-content>
            <v-list-item-title v-html="good.id"></v-list-item-title>
            <v-list-item-subtitle v-html="good.name"></v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
         <v-list-item :key="index" v-else >
          <v-list-item-content>
            <v-list-item-title v-html="good.id"></v-list-item-title>
            <v-list-item-subtitle v-html="good.name"></v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>

        <v-divider :key="index+'_'" inset></v-divider>
      </template>
    </v-list>
    <v-row>
      <v-spacer />
      <v-btn dark center :v-if="haveMore" @click="getMore">Ещё</v-btn>
      <v-spacer />
    </v-row>
  </div>
</template>

<script>
import {
  GOODS_REQUEST,
  GOODS_REQUEST_MORE
} from "@/store/modules/goods/consts";

import { mapActions, mapState, mapGetters } from "vuex";

export default {
  metaInfo: {
    title: "Склад"
  },
  methods: {
    ...mapActions("goods", {
      getGoods: GOODS_REQUEST,
      getMore: GOODS_REQUEST_MORE
    }),
    gotoGroup(good){
      this.$router.push({ name: 'store', params: { groupId: good.id } })
       this.getGoods(good.id);
    }
  },
  mounted() {
    this.getGoods(this.$route.params ? this.$route.params.groupId : null);
  },
     computed: {
    ...mapState('goods', ['goods']),
    ...mapGetters('goods', {haveMore: "haveMoreGoods", loading: 'loading' }),
  },
};
</script>

<style scoped>
.group{
  background-color: lightblue;
}
</style>