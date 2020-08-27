import Vue from 'vue'
import Vuex from 'vuex'

import bed from './module/bed'
import outlet from './module/outlet'
import room from './module/room'
import service from './module/service'
import share from './module/share'
import user from './module/user'


Vue.use(Vuex)

export const store = new Vuex.Store({
  modules: {
    bed,
    outlet,
    room,
    service,
    share,
    user
  }
})
