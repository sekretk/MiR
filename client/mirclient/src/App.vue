<template>
  <v-app>
    <template v-if="isAuth">
      <core-filter />

      <core-toolbar />

      <core-drawer />

      <core-view />
    </template>
    <core-login v-else />      
  </v-app>
</template>

<script>
export default {
  data() {
    return {
      isAuth: false,
    }
  }
}
</script>

<style lang="scss">
@import "@/styles/index.scss";

/* Remove in 1.2 */
.v-datatable thead th.column.sortable i {
  vertical-align: unset;
}
</style>
