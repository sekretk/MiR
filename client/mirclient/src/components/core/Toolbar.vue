<template>
  <v-app-bar flat app id="core-toolbar" clipped-left style="background: #eee;">
    <v-toolbar-title class="tertiary--text font-weight-light">
      <v-btn v-if="responsive" class="default v-btn--simple" dark icon @click.stop="toggleDrawer">
        <v-icon>mdi-view-list</v-icon>
      </v-btn>
    </v-toolbar-title>
    <v-select class="ml-3 mr-3" style="width: 80px;"
      :items="objects"
      item-text="name"
      item-value="id"
      :value="currentObject"
      @change="changeObject"
    ></v-select>
    <router-link v-ripple class="toolbar-items" to="/">
      <v-icon color="tertiary">mdi-view-dashboard</v-icon>
    </router-link>
    <router-link v-ripple class="toolbar-items" to="/card">
      <v-badge color="red" overlap>
        <span slot="badge">{{orderAmount}}</span>
        <v-icon color="rgba(0, 0, 0, 0.54)" medium>mdi-bell</v-icon>
      </v-badge>
    </router-link>
    <v-menu offset-y origin="center center" :nudge-bottom="10" transition="scale-transition">
      <template v-slot:activator="{ on }">
        <v-btn icon large text slot="activator" v-on="on">
          <v-avatar size="30px">
            <v-icon color="rgba(0, 0, 0, 0.54)">mdi-account</v-icon>
          </v-avatar>
        </v-btn>
      </template>

      <v-list class="pa-0">
        <v-list-item ripple="ripple" rel="noopener">
          <v-list-item-content>
            <v-list-item-title class="text-uppercase">{{ profile.role }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <v-list-item ripple="ripple" rel="noopener">
          <v-list-item-content>
            <v-list-item-title>{{ profile.name }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <v-list-item
          v-for="(item, index) in profileItems"
          :to="!item.href ? { name: item.name } : null"
          :href="item.href"
          @click="item.click"
          ripple="ripple"
          :disabled="item.disabled"
          :target="item.target"
          rel="noopener"
          :key="index"
        >
          <v-list-item-action v-if="item.icon">
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title>{{ item.title }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-menu>
  </v-app-bar>
</template>

<script>
import { mapMutations, mapGetters, mapActions, mapState } from "vuex";
import vieUtils from "@/utils/viewUtils";
import { AUTH_LOGOUT } from "@/store/modules/auth/consts";
import { SET_OBJECT, OBJECTS_REQUEST } from "@/store/modules/app/consts";

export default {
  methods: {
    ...mapMutations("app", {
      setDrawer: "setDrawer",
      toggleDrawer: "toggleDrawer",
      changeObject: SET_OBJECT
    }),
    ...mapActions("auth", { logout: AUTH_LOGOUT }),
    ...mapActions("app", { objectRequest: OBJECTS_REQUEST }),
    handleFullScreen() {
      vieUtils.toggleFullScreen();
    },
    onClick() {
      //
    },
    onResponsiveInverted() {
      if (window.innerWidth < 991) {
        this.responsive = true;
      } else {
        this.responsive = false;
      }
    },
    handleLogout() {
      this.logout();
      this.$router.push({ name: "/login", params: { apiAuth: false } });
    },
    handleSetting() {},
    handleProfile() {}
  },
  data() {
    return {
      responsive: false,
      profileItems: [
        {
          icon: "mdi-account-circle",
          href: "#",
          title: "Профиль",
          click: this.handleProfile
        },
        {
          icon: "mdi-settings",
          href: "#",
          title: "Настройки",
          click: this.handleSetting
        },
        {
          icon: "mdi-fullscreen-exit",
          href: "#",
          title: "Выход",
          click: this.handleLogout
        }
      ]
    };
  },
  computed: {
    ...mapGetters("user", ["profile"]),
    ...mapGetters("app", ["orderAmount"]),
    ...mapState("app", ["objects", "currentObject"])
  },
  mounted() {
    this.onResponsiveInverted();
    window.addEventListener("resize", this.onResponsiveInverted);
    this.objectRequest();
  },
  beforeDestroy() {
    window.removeEventListener("resize", this.onResponsiveInverted);
  }
};
</script>

<style>
#core-toolbar a {
  text-decoration: none;
}
.v-toolbar__content {
  flex-direction: row;
}
</style>
