import Vue from 'vue'
import Vuex from 'vuex'

import booking from './module/booking'
import customer from './module/customer'
import user from './module/user'
import outlet from './module/outlet'
import service from './module/service'
import therapist from './module/therapist'
import share from './module/share'

Vue.use(Vuex)
export const store = new Vuex.Store({
  modules: {
    customer,
    user,
    outlet,
    booking,
    service,
    therapist,
    share
  }
})
