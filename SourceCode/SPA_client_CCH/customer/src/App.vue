<template>
  <div>
    <template v-if="!$route.meta.public">
      <v-app id="inspire">
        <v-navigation-drawer
          v-model="drawer"
          fixed
          app
          width="200"
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
                    @click="viewCustomerProfile()"
                  >
                    <v-icon>account_circle</v-icon>
                  </v-btn>
            </v-flex>
          </v-layout>
        </v-toolbar>
        <v-content>
          <router-view></router-view>
        </v-content>
        <!--  -->
        <v-card
        >
          <v-layout
            justify-center
            row
            wrap>
              <v-bottom-nav
              app
              :value="true"
              fixed
              >
                <v-btn v-for="item in menuItems" :key="item.link"
                  color="primary"
                  flat
                  round
                  :to="item.link">
                  <span>{{item.title}}</span>
                  <v-icon> {{item.icon}} </v-icon>
                </v-btn>
              </v-bottom-nav>
          </v-layout>
        </v-card>
      </v-app>
      </template>
    <template v-else>
      <v-app id="inspire">
          <router-view></router-view>
      </v-app>
    </template>
    <v-layout row justify-center>
    <v-dialog v-model="error" persistent max-width="290">
      <v-card>
        <v-card-title class="headline">Error</v-card-title>
        <v-card-text>Let Google help apps determine location. This means sending anonymous location data to Google, even when no apps are running.</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" flat @click="clearError()">Disagree</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-layout>
  </div>
</template>


<script>
import { mapState, mapActions } from 'vuex'
export default {
  data: () => ({
    bottomNav: null,
    drawer: null,
    menuItems: [
      {icon: 'home', title: "Home", link: '/home'},
      {icon: 'list', title: "Booking List", link: '/booking_history'},
      {icon: 'location_on', title: "Nearby", link: '/map'},
      {icon: 'settings', title: "Settings", link: '/settings'},
      {icon: 'exit_to_app', title: "Logout", link: '/login'}
    ]
  }),
  props: {
    source: String
  },
  computed: {
    ...mapState({
        error: getters => getters.share.error
    })
  },
  methods:{
    clearError(){
      this.$store.dispatch('share/clearError')
    },
    viewCustomerProfile()
    {
      this.$router.push('/customer')
    }
  }
}
</script>
<style lang="stylus">
  @import './stylus/main'
</style>
