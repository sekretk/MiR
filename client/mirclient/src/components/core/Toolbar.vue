<template>
  <v-app-bar id="core-toolbar" flat prominent style="background: #eee;">
    <div class="v-toolbar-title">
      <v-toolbar-title class="tertiary--text font-weight-light">
        <v-btn v-if="responsive" class="default v-btn--simple" dark icon @click.stop="onClickBtn">
          <v-icon>mdi-view-list</v-icon>
        </v-btn>
        {{ title }}
      </v-toolbar-title>
    </div>

    <v-spacer></v-spacer>
    <v-toolbar-items>
      <v-flex align-center layout py-2>
        <v-text-field class="mr-4 purple-input" label="Поиск..." hide-details color="purple"></v-text-field>
        <router-link v-ripple class="toolbar-items" to="/">
          <v-icon color="tertiary">mdi-view-dashboard</v-icon>
        </router-link>
        <v-btn v-ripple light icon @click="handleFullScreen()">
          <v-icon color="rgba(0, 0, 0, 0.54)">mdi-fullscreen</v-icon>
        </v-btn>
        <v-menu bottom left content-class="dropdown-menu" offset-y transition="slide-y-transition">
          <template v-slot:activator="{ on }">
            <v-btn icon v-on="on">
              <router-link v-ripple icon slot="activator" class="toolbar-items" to="/notifications">
                <v-badge color="error" overlap>
                  <template slot="badge">{{ notifications.length }}</template>
                  <v-icon color="tertiary">mdi-bell</v-icon>
                </v-badge>
              </router-link>
            </v-btn>
          </template>

          <v-card>
            <v-list dense>
              <v-list-item
                v-for="notification in notifications"
                :key="notification"
                @click="onClick"
              >
                <v-list-item-title v-text="notification"></v-list-item-title>
              </v-list-item>
            </v-list>
          </v-card>
        </v-menu>
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
      </v-flex>
    </v-toolbar-items>
  </v-app-bar>
</template>

<script>
import { mapMutations, mapGetters, mapActions } from "vuex";
import vieUtils from "@/utils/viewUtils";
import { AUTH_LOGOUT } from "@/store/modules/auth/consts";

export default {
  methods: {
    ...mapMutations("app", ["setDrawer", "toggleDrawer"]),
    ...mapActions("auth", { logout: AUTH_LOGOUT }),
    handleFullScreen() {
      vieUtils.toggleFullScreen();
    },
    onClickBtn() {
      this.setDrawer(!this.$store.state.app.drawer);
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
      this.logout()
      this.$router.push({ name: "/login", params: { apiAuth: false } });
    },
    handleSetting() {},
    handleProfile() {}
  },
  data() {
    return {
      notifications: [
        "Mike, John responded to your email",
        "You have 5 new tasks",
        "You're now a friend with Andrew",
        "Another Notification",
        "Another One"
      ],
      title: null,
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
    ...mapGetters("user", ["profile"])
  },
  watch: {
    $route(val) {
      this.title = val.name;
    }
  },

  mounted() {
    this.onResponsiveInverted();
    window.addEventListener("resize", this.onResponsiveInverted);
  },
  beforeDestroy() {
    window.removeEventListener("resize", this.onResponsiveInverted);
  }
};
</script>

<style scoped>
#core-toolbar a {
  text-decoration: none;
}

.v-toolbar__content {
  height: 60px;
}

header {
  height: 60px;
}
</style>
