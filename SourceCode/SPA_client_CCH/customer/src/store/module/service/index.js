import axios from 'axios'
import router from '@/router'

export const HTTP_API = axios.create({
// baseURL điền cái localhost:port
  baseURL: 'http://localhost:5828/',
  headers: {"Access-Control-Allow-Origin": "*"}
})

// Lưu dữ liệu sau khi gọi API ở đây
// Xem ví dụ bên thư mục customer


const state = {
  numBedTherapist:[],
  services: [],
  serviceTypes: [],
  service: [],
  servicesChoose: [],
  serviceReviews:[],
}

// getters
const getters = {
  ServicesInOutletDetail: (state) => {
    return state.services.slice(0, 4)
  }
  // FilterService: (state) => (payload) => {
  //   return state.services.filter(payload => payload.)
    
  // }
}

// Viết các hàm gọi API ở đây
// Xem ví dụ bên thư mục customer
const actions = {
  getServicesByOutlet({commit}, payload){
    commit('share/setLoading', {payload: true} , { root: true })

    HTTP_API({
      method: 'get',
      url: `service/outlet/${payload.idOutlet}/${payload.pageSize}/${payload.pageNumber}`,
      headers:{
        // Authorization:
      }
    })
    .then((result) => {
      console.log('get service by outlet')
      console.log(result)
      var servicesbyoutlet = result.data.result.data
      commit('share/setLoading', {payload: false} , { root: true })
      commit('setServiceByOutletLoaded', servicesbyoutlet)
    }).catch((err) => {
      console.log(err)
    });
  },

  getServiceByID({commit},payload){
    commit('share/setLoading', {payload: true} , { root: true })
    HTTP_API({
      method: 'get',
      url: `service/id/${payload}`,
      header:{
        // Authoiation
      }
    }).then((result) => {
      console.log('get service by id')
      console.log(result)
      var servicebyid = result.data.result.data
      commit('share/setLoading', {payload: false} , { root: true })
      commit('setServiceByIDLoader', servicebyid)
    }).catch((err) => {
      console.log(err)
    });
  },

  GetNumOfBedAndTherapistByOutlet({commit},outletID){
    HTTP_API({
      method: 'get',
      url: `outlet/BedAndTherapistNum/${outletID}`,
      header: {

      }
    }).then((result)=>{
      console.log('get num of bed and therapist by outletID')
      console.log(result)
      var _numBedTherapist=result.data.result.data
      commit('setBedAndTherapistNumLoader',_numBedTherapist)
    }).catch((err)=>{
      console.log(err)
    })
  },
  getServiceByName({commit}, payload){
    commit('share/setLoading', {payload: true} , { root: true })
    HTTP_API({
      method: 'get',
      url: `service/name/${payload}`,
      header: {

      }
    }).then((result) => {
      console.log('get service by name')
      console.log(result)
      var servicebyname = result.data.result.data
      commit('share/setLoading', {payload: false} , { root: true })
      commit('setServiceByName', servicebyname )
    }).catch((err) => {
      console.log(err)
    });
  },

  getServiceType({commit}){
    HTTP_API({
      method: 'get',
      url: `service/alltype`,
      header: {

      }
    }).then((result) => {
      console.log('get service type')
      console.log(result)
      var serviceTypes = result.data.result.data
      commit('setServiceTypes', serviceTypes)
    }).catch((err) => {
      console.log(err)
    });
  },

  chooseService({commit}, payload){
    commit('addServiceChoose', payload)
  },
  removeService({commit}, payload){
    commit('removeServiceChoose', payload)
  },
  getServiceFilter({commit},payload){
    commit('share/setLoading', {payload: true} , { root: true })
    HTTP_API({
      method: 'post',
      url: `service/GetServiceBy5Filter`,
      data:{
        outletID: payload.outletID,
        serivceName: payload.serviceName,
        serviceType: payload.serviceType,
        servicePrice: payload.servicePrice,
        orderBy: payload.orderBy
      },
      header: {}
    }).then((result) => {
      if (result.data.hasOwnProperty('error')){
        var mes = 'Error: ' + result.data.error.validation.errorMessage
        var noti = {
          mes: mes,
          type: 'error'
        }
        
        commit('share/setNoti', {payload: noti} , { root: true })
        commit('share/setLoading', {payload: false} , { root: true })

      }
      else {
      var services = result.data.result.data
      commit('share/setLoading', {payload: false} , { root: true })
      commit('setServiceFilter', services)
      }
    }).catch((err) => {
      console.log(err)
    });
  },
  getReviewsServiceByID({commit}, payload){
    HTTP_API({
      method: 'get',
      url: `service/review/${payload}`,
      header: {}
    })
    .then((result) => {
      console.log('get reviews service')
      console.log(result)
      var reviewsService = result.data.result.data
      commit('setReviewService', reviewsService)
    }).catch((err) => {
      console.log(err)
    });
  },

  addReview({commit}, payload){
    HTTP_API({
      method: 'post',
      url: `service/review`,
      data:{
        content : payload.content,
        star: payload.star,
        serviceId: payload.serviceId,
        customerid: payload.customerid
      },
      header: {}
    })
    .then((result) => {
      console.log('add review service')
      console.log(result)
      router.push('/service')
    })
    .catch((err) => {
      console.log('catch')
      console.log(err)
    }); 
  },
  removeAllServiceChoose({commit}){
    commit('removeAllServiceChoose')
  },
}

// Để truyền dữ liệu sau khi gọi API ở action vào phần State, phải gọi các hàm ở phần Mutations
const mutations = {
  setServiceByOutletLoaded(state, payload){
    state.services = payload
  },
  setServiceByIDLoader(state, payload){
    state.service = payload
  },
  setServiceByName(state, payload){
    state.services = payload
  },

  setBedAndTherapistNumLoader(state,_numBedTherapist){
    state.numBedTherapist=_numBedTherapist
  },
  setServiceTypes(state, payload){
    state.serviceTypes = payload
  },

  addServiceChoose(state, payload){
    console.log('mutations add')
    state.servicesChoose.push(payload)
  },
  setServiceFilter(state,payload){
    state.services = payload
  },
  removeServiceChoose(state, payload){
    console.log('mutations remove')
    state.servicesChoose.splice(payload.index, 1)
  },

  removeAllServiceChoose(state){
    state.servicesChoose=[]
  },

  setReviewService(state, payload){
    state.serviceReviews = payload
  }
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}
