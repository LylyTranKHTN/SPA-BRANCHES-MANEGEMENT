// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import Vuetify from 'vuetify'
import { store } from './store'
import * as VueGoogleMaps from "vue2-google-maps";

import CustomerProfile from './components/customer//CustomerProfile';
import BookingList from './components/booking/BookingList/BookingList';
import Service from './components/service/ServiceList/Service'
import ServiceReview from './components/service/ServiceList/ServiceReview'
import TermConditions from './components/containers/TermConditions'
import ReviewBookingBeforeSubmit from '@/components/booking/ReviewBookingBeforeSubmit'
import Alert from '@/components/share/Alert'

Vue.component('review-booking', ReviewBookingBeforeSubmit);
Vue.component('customer-profile', CustomerProfile);
Vue.component('booking-list', BookingList)

Vue.component('service', Service)
Vue.component('service-review', ServiceReview)

Vue.component('app-alert', Alert)
Vue.component('term-conditions', TermConditions)

Vue.use(Vuetify)
Vue.use(VueGoogleMaps, {
  load: {
    key: "AIzaSyBVec11szjuxod9rdgN-zTEoRpuOcH7hTE",
    libraries: "places" // necessary for places input
  }
});
Vue.config.productionTip = false


/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,
  components: { App },
  template: '<App/>'
})
