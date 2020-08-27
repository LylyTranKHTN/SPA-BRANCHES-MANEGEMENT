<template>
  <v-app id="inspire">
    <v-navigation-drawer
      v-model="drawer"
      fixed
      app
    >
      <v-list>
        <v-list-tile
          v-for="item in menuItems"
          :key="item.title"
          :to="item.link">
          <v-list-tile-action>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>{{ item.title }}</v-list-tile-content>
        </v-list-tile>
      </v-list>
    </v-navigation-drawer>
     <v-toolbar
        color="white" light fixed app>
        <v-layout row align-center>
          <v-flex xs1>
            <v-toolbar-side-icon @click.stop="drawer = !drawer"
              v-if="$route.meta.draw"
            ></v-toolbar-side-icon>
            <v-btn
              icon
              v-else
              :to="$route.meta.to"
            >
              <v-icon>arrow_back</v-icon>
            </v-btn>
          </v-flex>
          <v-flex xs10 text-xs-center>
            <v-toolbar-title>DAY MORE</v-toolbar-title>
          </v-flex>
          <v-flex xs1>
                  <v-btn
                  icon
                >
                  <v-icon>account_circle</v-icon>
                </v-btn>
          </v-flex>
        </v-layout>
      </v-toolbar>
     <!-- Hiển thị thông báo -->
     <!-- <v-layout row wrap v-if="noti != null">
        <app-alert
            :noti="noti"
            @dismissed="onDismissed"
            >
        </app-alert>
      </v-layout> -->
    <v-content>
      <router-view></router-view>
    </v-content>
    <v-footer color="primary" app>
      <span class="white--text">&copy; 2017</span>
    </v-footer>
  </v-app>
</template>

<script>
import { mapState, mapActions } from 'vuex'
  export default {
    data: () => ({
      drawer: null,
      menuItems: [
        {icon: 'home', title: "Home", link: '/'},
        {icon: 'people', title: "Customer List", link: '/customerlist'},
        {icon: 'list', title: "Booking List", link: '/bookinglist'},
        {icon: 'settings', title: "Settings", link: '/settings'}
      ]
    }),
    props: {
      source: String
    },
    computed: {
        ...mapState({
            noti: state => state.share.noti,
        }),
    },
    methods:{
      onDismissed () {
          this.$store.dispatch('share/clearNoti')
      }

    }
  }
</script>
<style lang="stylus">
  @import './stylus/main'
</style>
