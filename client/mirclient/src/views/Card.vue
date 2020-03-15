<template>
  <div>
    <h2>Заказ</h2>
    <v-list three-line>
      <template v-for="(good, index) in goods">
        <v-list-item :key="index">
          <v-list-item-content>
            <v-list-item-title v-html="good.good.id"></v-list-item-title>
            <v-list-item-subtitle v-html="good.good.name"></v-list-item-subtitle>
          </v-list-item-content>

          <v-spacer></v-spacer>

          <v-list-item-action>
            <v-flex>
              <v-btn
                text
                icon
                color="red darken-1"
                @click="changeCount({good, delta: -1})"
              >
                <v-icon>mdi-minus-box</v-icon>
              </v-btn>
              {{good.count}}
              <v-btn
                text
                icon
                color="teal darken-1"
                @click="changeCount({good, delta: 1})"
              >
                <v-icon>mdi-plus-box</v-icon>
              </v-btn>
            </v-flex>
          </v-list-item-action>
        </v-list-item>

        <v-divider :key="index+'_'" inset></v-divider>
      </template>
    </v-list>
  </div>
</template>

<script>
import { CHANGE_ORDER } from "@/store/modules/app/consts";

import { mapState, mapMutations } from "vuex";

export default {
  metaInfo: {
    title: "Заказ"
  },
  methods: {
    ...mapMutations("app", {
      changeCount: CHANGE_ORDER
    }),
    gotoGroup(good) {
      if (this.$route.params && good.id != this.$route.params.groupId)
        this.$router.push({ name: "store", params: { groupId: good.id } });
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