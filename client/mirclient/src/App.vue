<template>
  <v-app>
    <core-login v-if="isLogin" />
    <template v-else>
      <core-filter />

      <core-toolbar />

      <core-drawer />

      <core-view />
    </template>
  </v-app>
</template>

<script>
export default {
  metaInfo: {
    title: "Главная",
    titleTemplate: "MI отчёт - %s",
    htmlAttrs: {
      lang: "ru",
      amp: true
    }
  },
  computed: {
    isLogin: function() {
      return this.$route.name == "login";
    }
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
