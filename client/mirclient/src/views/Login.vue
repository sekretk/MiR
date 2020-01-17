<template>
  <v-container fluid fill-height>
    <v-layout align-center justify-center>
      <v-flex xs12 sm8 md4>
        <v-card class="elevation-12">
          <div class="layout column align-center">
            <img src="/static/logo.png" alt="MI Отчётность" width="120" height="120" />
            <h1 class="flex my-4 primary--text">MI отчёт</h1>
          </div>
          <v-card-text>
            <v-form>
              <v-text-field
                prepend-icon="mdi-account"
                name="login"
                label="Логин"
                type="text"
                required
                v-model="user"
                color="secondary"
              ></v-text-field>
              <v-text-field
                prepend-icon="mdi-lock"
                name="password"
                label="Пароль"
                id="password"
                type="password"
                required
                v-model="password"
                color="secondary"
                @keyup.enter.native="proceedLogin"
              ></v-text-field>
            </v-form>
          </v-card-text>
          <v-alert :value="hasError" type="error">{{error}}</v-alert>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn text class="black--text font-weight-black" @click="proceedLogin">Вход</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>

import { AUTH_REQUEST } from "@/store/modules/auth/consts";

import {
  mapActions,
} from 'vuex'


export default {
 data() {
    return {
      user: localStorage.getItem("lastuser")
        ? localStorage.getItem("lastuser")
        : "",
      password: "",
      error: null,
      from: null,
      isAuth: false,
    };
  },
  methods: {
       ...mapActions('auth', { authRequest: AUTH_REQUEST}),
    proceedLogin() {
      this.error = null;

      const { user, password } = this;

      this.authRequest({ user, password })
        .then(() => {
          localStorage.setItem("lastuser", user);
          this.$router.push(this.$route.params.apiAuth ? this.from : '/');
        })
        .catch(err => {
          this.error = err;
        });
    }
  },
  computed: {
    // a computed getter
    hasError: function() {
      // `this` points to the vm instance
      return this.error != null;
    }
  },
  beforeRouteEnter(to, from, next) {
    next(vm => {
      vm.from = from;
    });
  }
}
</script>

<style>

</style>