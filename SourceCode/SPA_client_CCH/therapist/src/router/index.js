import Vue from 'vue'
import Router from 'vue-router'
import DashBoard from '@/components/containers/DashBoard'
import BookingList from '@/components/booking/BookingList'
import CustomerProfile from '@/components/customer/CustomerProfile'
import LogIn from '@/components/containers/LogIn'
import VeriCode from '@/components/containers/Vericode'
import AddNewNote from '@/components/customer/AddNote'
import CustomerList from '@/components/customer/CustomerList'
import AddNewBooking from '@/components/booking/AddNewBooking'
import AddNewBookingForCustomer from '@/components/booking/AddNewBookingForCustomer'
import AddNewCustomer from '@/components/customer/AddNewCustomer'
import ServiceCompo from '@/components/service/ServiceCompo'

//import AddNewBooking from '@/components/booking/AddNewBooking'
Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
        name: 'home',
      component: DashBoard
    },
    {
      path: '/verycode',
      name: 'Veri code',
      component: VeriCode
    },
    {
      path: '/customerprofile',
      name: 'Customer Profile',
      component: CustomerProfile
    },
    {
      path: '/BookingList',
      name: 'Booking list',
      component: BookingList
    },
    {
      path: '/login',
      name: 'Log In',
      component: LogIn
    },
    {
      path: '/addnewnote',
      name: 'Add new note',
      component: AddNewNote
    },

    // Huy
    {
      path: '/customerlist',
      name: 'CustomerList',
      component: CustomerList
    },
    {
      path:'/customer/new',
      name: 'AddNewCustomer',
      component: AddNewCustomer
    },
    {
      path:'/booking',
      name:'AddNewBooking',
      component: AddNewBooking
    },
    {
      path:'/booking/new',
      name:'AddNewBookingForCustomer',
      component: AddNewBookingForCustomer
    },
    {
      path:'test/serviceCompo',
      name:'testServiceCompo',
      component:ServiceCompo
    },
    {
      path:'/bookinglist',
      name:'BookingList',
      component: BookingList
    }
    
  ]
})
