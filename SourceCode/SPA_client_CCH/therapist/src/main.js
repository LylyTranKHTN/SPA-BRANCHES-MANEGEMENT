// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import Vuetify from 'vuetify'
import { store } from './store'

import AddNewCustomer from './components/customer/AddNewCustomer';
import BookingList from './components/booking/BookingList';
import AddNote from './components/customer/AddNote';
import CustomerProfile from './components/customer//CustomerProfile';
import UpComingCustomer from './components/customer/UpComingCustomer';
import CustomerList from './components/customer/CustomerList';
import Service from './components/service/ServiceCompo'
import TermConditions from './components/containers/TermConditions'
import ReviewBookingBeforeSubmit from './components/booking/ReviewBookingBeforeSubmit'
import AddNewBooking from './components/booking/AddNewBooking'
import SwitchCustomer from './components/booking/SwitchCustomer'
import Alert from '@/components/share/Alert'
//
Vue.component('add-new-customer', AddNewCustomer);
Vue.component('add-note', AddNote);
Vue.component('customer-profile', CustomerProfile);
Vue.component('up-coming-customer', UpComingCustomer);
Vue.component('booking-list', BookingList)
//
Vue.component('review-booking', ReviewBookingBeforeSubmit);
Vue.component('service', Service)
Vue.component('term-conditions', TermConditions)
Vue.component('add-new-booking',AddNewBooking)
Vue.component('customer-list',CustomerList)
Vue.component('switch-customer',SwitchCustomer)


Vue.component('app-alert', Alert)

Vue.use(Vuetify)
Vue.config.productionTip = false



/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,
  components: { App },
  template: '<App/>'
})
