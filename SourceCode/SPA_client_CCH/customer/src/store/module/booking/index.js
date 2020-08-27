import axios from 'axios'

export const HTTP_API = axios.create({
// baseURL điền cái localhost:port
baseURL: 'http://localhost:5828/',
// headers: {"Access-Control-Allow-Origin": "*"}
})

// Lưu dữ liệu sau khi gọi API ở đây
// Xem ví dụ bên thư mục customer
const state = {
  measurementbycustomer: [],
  bookingHistory: [],
  booking: [],
  bookingAvailables: [],
  therapists:[],
  timeslots:[],
}

// getters
const getters = {
}

// Viết các hàm gọi API ở đây
// Xem ví dụ bên thư mục customer
const actions = {
  getMeasurementByCustomer({commit}, payload){
    HTTP_API({
      method: 'get',
      url:`booking/getmeasurement/customer/${payload.cusID}/service/${payload.serID}`,
      header:{
        
      }
    }).then((result) => {
      console.log('get measurement history customer')
      var measurementbycustomer = result.data.result.data
      commit('setMeasurementByCustomer', measurementbycustomer)
    }).catch((err) => {
      console.log(err)
    })
  },

  getBookingHistoryByCustomer({commit}, payload){
    HTTP_API({
      method: 'get',
      url: `booking/history/${payload.cusID}/${payload.days}`,
      header:{
        //
      }
    }).then((result) => {
      console.log('get booking history by customer')
      console.log(result)
      var bookingHistory = result.data.result.data
      commit('setBookingHistoryByCustomerLoader', bookingHistory)
    }).catch((err) => {
      console.log(err)
    })
  },

  getBookingDetail({commit}, payload){
    HTTP_API({
      method: 'get',
      url: `booking/${payload}`,
      header: {
        // 
      }
    }).then((result) => {
      console.log('get booking detail')
      console.log(result)
      var booking = result.data.result.data
      commit('setBookingDetailLoader', booking)
    }).catch((err) => {
      console.log(err)
    })
  },
  getBookingAvailable({commit}, payload){
    commit('share/setLoading', {payload: true} , { root: true })
    HTTP_API({
      method: 'get',
      url: `booking/booking-available/${payload.outletId}/${payload.date}/${payload.pagesize}/${payload.pageNumber}`,
      header: {
        // 
      }
    }).then((result) => {
      console.log('get booking available')
      console.log(result)
      if (result.data.hasOwnProperty('error')){
        var mes = 'Error: ' + result.data.error.validation.errorMessage
        var noti = {
          mes: mes,
          type: 'error'
        }
        commit('share/setNoti', {payload: noti} , { root: true })
        commit('share/setLoading', {payload: false} , { root: true })
      }
      else{
        var booking = result.data.result.data
        commit('share/setLoading', {payload: false} , { root: true })
        commit('setBookingAvailable', booking)
      }
    }).catch((err) => {
      console.log(err)
    })
  },
  getTherapistBookingAvailable({commit},payload){
    HTTP_API({
      method: 'get',
      url: `booking/booking-available/therapist/${payload.outletId}/${payload.serviceId}/${payload.timeslot}/${payload.date}`,
      header: {
        // 
      }
    }).then((result) => {
      console.log('get therapist of booking available')
      console.log(result)
      if (result.data.hasOwnProperty('error')){
        alert(result.data.error.errorMessage)
        alert(result.data.error.validation.errorMessage)
      }
      var therapists = result.data.result.data.result
      commit('setTherapistBookingAvailable', therapists)
    }).catch((err) => {
      console.log(err)
    })
  },
  getTimeslotBookingAvailable({commit},payload){
    HTTP_API({
      method: 'get',
      url: `booking/booking-available/timeslot/${payload.outletId}/${payload.serviceId}/${payload.therapistId}/${payload.date}`,
      header: {
        // 
      }
    }).then((result) => {
      console.log('get timeslots of booking available')
      console.log(result)
      if (result.data.hasOwnProperty('error')){
        alert(result.data.error.errorMessage)
        alert(result.data.error.validation.errorMessage)
      }
      var timeslots = result.data.result.data
      commit('setTimeslotBookingAvailable', timeslots)
    }).catch((err) => {
      console.log(err)
    })
  },
  createBooking({commit},payload){
    commit('share/setLoading', {payload: true} , { root: true })
    HTTP_API({
      method: 'post',
      url: `booking/${payload.customerID}/${payload.outletID}`,
      header: {
        // 
      },
      data:{
        note: payload.note,
        createBookingDtos: payload.createBookingDtos
      }
    }).then((result) => {
      console.log('create booking')
      console.log(result)
      if (result.data.hasOwnProperty('error')){
        commit('share/setLoading', {payload: false} , { root: true })
        // alert(result.data.error.errorMessage)
        // alert(result.data.error.validation.errorMessage)
        var mes = 'Error: ' + result.data.error.validation.errorMessage
        var noti = {
          mes: mes,
          type: 'error'
        }
        commit('share/setNoti', {payload: noti} , { root: true })
      }
      var booking = result.data.result.data
      commit('setBookingHistoryByCustomerLoader', booking)
      commit('share/setLoading', {payload: false} , { root: true })
      commit('service/removeAllServiceChoose', {payload: null},{root: true})
    }).catch((err) => {
      console.log(err)
    })
  },
  editBooking({commit},payload){
    HTTP_API({
      method: 'put',
      url: `booking/${payload.customerID}/${payload.outletID}`,
      header: {
        // 
      },
      data:{
        note: payload.note,
        editBookingDtos: payload.editBookingDtos
      }
    }).then((result) => {
      console.log('edit booking')
      console.log(result)
      if (result.data.hasOwnProperty('error')){
        alert(result.data.error.errorMessage)
        alert(result.data.error.validation.errorMessage)
      }
      var booking = result.data.result.data
      commit('setBookingHistoryByCustomerLoader', booking)
    }).catch((err) => {
      console.log(err)
    })
  },
  cancelBooking({commit},payload){
    HTTP_API({
      method: 'delete',
      url: `booking/${payload}`,
      header: {
        // 
      }
    }).then((result) => {
      console.log('cancel booking')
      console.log(result)
      if (result.data.hasOwnProperty('error')){
        alert(result.data.error.errorMessage)
        alert(result.data.error.validation.errorMessage)
      }
    }).catch((err) => {
      console.log(err)
    })
  },
  resetAllBookings({commit}){
    commit('setBookingAvailable', [])
    console.log('action reset all booking')
  }
}

// Để truyền dữ liệu sau khi gọi API ở action vào phần State, phải gọi các hàm ở phần Mutations
const mutations = {
  setMeasurementByCustomer(state, payload){
    state.measurementbycustomer = payload
  },
  setBookingHistoryByCustomerLoader(state, payload){
    console.log('mutation booking history')
    state.bookingHistory = payload
  },
  setBookingDetailLoader(state, payload){
    state.booking = payload
  },
  setBookingAvailable(state,payload){
    state.bookingAvailables = payload
  },
  setTherapistBookingAvailable(state, payload){
    state.therapists = payload
  },
  setTimeslotBookingAvailable(state,payload){
    state.timeslots = payload
  }
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}
