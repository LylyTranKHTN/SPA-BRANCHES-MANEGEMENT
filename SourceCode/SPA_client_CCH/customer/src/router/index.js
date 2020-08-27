import Vue from 'vue'
import Router from 'vue-router'

import DashBoard from '@/components/containers/DashBoard'
import LogIn from '@/components/containers/LogIn'
import NotificationList from '@/components/containers/NotificationList'
import ReferralCode from '@/components/containers/ReferralCode'

import BookingList from '@/components/booking/BookingList/BookingList'
import AddNewBooking from '@/components/booking/AddNewBooking'

import OutletDetail from '@/components/outlet/OutletDetails'
import OutletList from '@/components/outlet/OutletList/OutletList'
import ViewReview from '@/components/outlet/ViewReview'

import AddNewReview from '@/components/outlet/AddNewReview'
import DetailOfStepInBooking from '@/components/customer/DetailOfStepInBooking'
import TermConditions from '@/components/containers/TermConditions'
import BookingDetail from '@/components/booking/BookingDetail'
import Problem from '@/components/customer/Measurement/Problem'
import BookingHistory from '@/components/booking/BookingHistory'

import ServiceList from '@/components/service/ServiceList'
import ServiceDetails from '@/components/service/ServiceDetails'
import ViewReviewService from '@/components/service/ViewReviewService'
import AddReviewService from '@/components/service/AddReviewService'


import TherapistList from '@/components/therapist/TherapistList'
import TherapistProfile from '@/components/therapist/TherapistProfile'

import  CustomerProfile from '@/components/customer/CustomerProfile'

import TestAPI from '@/components/TestAPI'
import GoogleMap from '@/components/containers/Map';


Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/map',
      name: 'GoogleMap',
      component: GoogleMap,
      meta: {
        public: true,
      },
    },
    {
      path: '/testapi',
      name: 'TestAPI',
      component: TestAPI,
      meta: {
        public: true,
      },
    },
    {
      path: '/',
      name: 'referral code',
      component: ReferralCode,
      meta: {
        public: true,
      },
    },
    {
      path: '/login',
      name: 'LogIn',
      component: LogIn,
      meta: {
        public: true,
      },
    },
    {
      path: '/booking_list',
      name: 'BookingList',
      component: BookingList
    },
    {
      path: '/home',
      name: 'DashBoard',
      component: DashBoard,
      meta: {
        draw: true,
      },
    },
    {
      path: '/add_new_booking',
      name: 'AddNewBooking',
      component: AddNewBooking,
      meta: {
        draw: true,
      },
    },
    {
      path: '/notification_list',
      name: 'NotificationList',
      component: NotificationList
    },
    

    {
      path: '/booking_history',
      name: 'BookingHistory',
      component: BookingHistory,
      meta: {
        draw: true,
      },
    },
   
    {
      path: '/DetailOfStepInBooking',
      name: 'Detail Of Step In Booking',
      component: DetailOfStepInBooking
    },

    {
      path: '/outlets',
      name: 'OutletList',
      component: OutletList,
      meta: {
        draw: true,
      },
    },

    {
      path: '/outlet',
      name: 'OutletDetail',
      component: OutletDetail,
      meta: {
        draw: false,
        to: '/outlets'
      },
    },
    {
      path: '/outlet/reviews',
      name: 'ViewReview',
      component: ViewReview,
      meta: {
        draw: false,
        to: '/outlet'
      },
    },

    {
      path: '/outlet/addnewreview',
      name: 'Add new review',
      component: AddNewReview,
      meta: {
        draw: false,
        to: '/outlet'
      },
    },

    {
      path: '/customer/measurement/problem',
      name: 'Problem',
      component: Problem
    },

    {
      path: '/booking_detail',
      name: 'BookingDetail',
      component: BookingDetail,
      meta: {
        draw: false,
        to: '/booking_history'
      },
    },
    {
      path: '/services',
      name: 'ServiceList',
      component: ServiceList,
      meta: {
        draw: false,
        to: '/outlet'
      },
    },
    {
      path: '/service',
      name: 'ServiceDetails',
      component: ServiceDetails,
      meta: {
        draw: false,
        to: '/services'
      },
    },
    {
      path: '/service/reviews',
      name: 'ViewReviewService',
      component: ViewReviewService,
      meta: {
        draw: false,
        to: '/service'
      },
    },
    {
      path: '/service/addreview',
      name: 'AddReviewService',
      component: AddReviewService,
      meta: {
        draw: false,
        to: '/service'
      },
    },
    {
      path: '/therapists',
      name: 'TherapistList',
      component: TherapistList,
      meta: {
        draw: false,
        to: '/outlet'
      },
    },
    {
      path: '/therapist',
      name: 'TherapistProfile',
      component: TherapistProfile,
      meta: {
        draw: false,
        to: '/therapists'
      },
    },
    {
      path: '/customer',
      name: 'CustomerProfile',
      component: CustomerProfile,
      meta: {
        draw: true,
      },
    },
  ]
})
