<template>
  <div>
    <h2 class="ml-2">О приложении</h2>
    <v-progress-linear
      :active="changesLoading"
      :indeterminate="changesLoading"
      absolute
      top
      color="deep-purple accent-4"
    ></v-progress-linear>
    <div class="ml-2 mr-2">
      <p>Версия сервера: {{serverVersion}}</p>
      <p>Версия клиента: {{clientVersion}}</p>
    </div>
    <v-list>
      <template v-for="(change, index) in changes">
        <v-list-item :key="index">
          <v-list-item-content class="ml-4 pa-0">
            <p>{{change}}</p>
          </v-list-item-content>
        </v-list-item>
        <v-divider :key="index+'_'"></v-divider>
      </template>
    </v-list>
  </div>
</template>

<script>
import { BUILD_VERSION, BUILD_DATE } from "@/version";

import { mapState } from "vuex";

export default {
  data() {
    return {
      Version: BUILD_VERSION,
      VersionDate: BUILD_DATE
    };
  },
  computed: {
    ...mapState("app", ["changes", "serverVersion", "changesLoading"]),
    clientVersion() {
      return `Версия: ${this.Version}, Дата: ${this.VersionDate}`;
    }
  }
};
</script>

<style>
</style>