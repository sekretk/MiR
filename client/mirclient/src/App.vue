<template>
  <v-app>
    <core-login v-if="isLogin" />
    <template v-else>
      <core-filter />

      <core-toolbar />

      <core-drawer />

      <core-view />
    </template>
    <span class="appversion">Версия: {{Version}}, Дата: {{VersionDate}}</span>
  </v-app>
</template>

<script>
import { mapActions } from "vuex";
import { PING } from "@/store/modules/app/consts";
import { BUILD_VERSION, BUILD_DATE } from "@/version";

export default {
  data() {
    return {
      Version: BUILD_VERSION,
      VersionDate: BUILD_DATE
    };
  },
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
  },
  mounted() {
    this.ping();
  },
  methods: {
    ...mapActions("app", { ping: PING })
  }
};
</script>

<style lang="scss">
@import "@/styles/index.scss";

/* Remove in 1.2 */
.v-datatable thead th.column.sortable i {
  vertical-align: unset;
}

span.appversion {
  margin-bottom: 0px;
  position: fixed;
  bottom: 3px;
  right: 10px;
  font-size: 9px;
  color: brown;
}
</style>
