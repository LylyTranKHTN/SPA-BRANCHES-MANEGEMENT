import Vue from 'vue'
import Vuex from 'vuex'

import booking from './module/booking'
import customer from './module/customer'
import outlet from './module/outlet'
import service from './module/service'
import therapist from './module/therapist'
import share from './module/share'

Vue.use(Vuex)

export const store = new Vuex.Store({
  modules: {
    booking,
    customer,
    outlet,
    service,
    therapist,
    share
  }
})
