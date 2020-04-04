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

.goods-container .group {
  background-color: lightblue;
}
.goods-container .store-id {
  position: absolute;
  left: 3px;
  top: 3px;
  font-size: 8px;
  font-weight: 600;
}

.goods-container .store-quantity{
  position: absolute;
  right: 3px;
  color: red;
  top: -3px;
  font-size: 8px;
  font-weight: 600;
}

.goods-container .v-list-item {
  height: 60px;
}

.goods-container .v-list-item__content {
  height: 60px;
}

.goods-container p.center {
  height: 60px;
  line-height: 60px;
}

.goods-container .v-list-item__action {
  height: 60px;
}
</style>
