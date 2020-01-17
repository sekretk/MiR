<template>
  <v-app>
    <template v-if="isAuthenticated">
      <core-filter />

      <core-toolbar />

      <core-drawer />

      <core-view />
    </template>
    <template v-else>
      <router-view></router-view>
    </template>
  </v-app>
</template>

<script>
import { mapMutations, mapState } from "vuex";

export default {
  computed: {
    ...mapState("auth", ["isAuthenticated"])
  },
  methods: {
    ...mapMutations("auth", ["toggleAuth"])
  }
};
</script>

<style lang="scss">
@import "@/styles/index.scss";

/* Remove in 1.2 */
.v-datatable thead th.column.sortable i {
  vertical-align: unset;
}
</style>
