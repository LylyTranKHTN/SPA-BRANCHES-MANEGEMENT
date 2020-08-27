import axios from 'axios'

export const HTTP_API = axios.create({
// baseURL điền cái localhost:port
baseURL: 'http://localhost:8000/',
headers: {"Access-Control-Allow-Origin": "*"}
})

// Lưu dữ liệu sau khi gọi API ở đây
// Xem ví dụ bên thư mục customer
const state = {
    beds: [],
    services:[],    
    bedServices:[]

}

// getters
const getters = {
}

// Viết các hàm gọi API ở đây
// Xem ví dụ bên thư mục customer
const actions = {
    loadbedByOutletID({commit}, payload){
        commit('share/setLoading', {payload: true} , { root: true })
        HTTP_API({
            method: 'get',
            url: `bed/room/${payload.outletID}/10/0`,
            headers:{
              // Authorization:
            },
        })
        .then((result) => {
            console.log('action get bed by outlet id')
            console.log(result)
            var beds = result.data
            commit('setbedList', beds)
            commit('share/setLoading', {payload: false} , { root: true })
          }).catch((err) => {
            console.log(err)
        });
    },
    loadServices({commit}, payload){
        commit('share/setLoading', {payload: true} , { root: true })
        HTTP_API({
            method: 'get',
            url: `service`,
            headers:{
              // Authorization:
            },
        })
        .then((result) => {
            console.log('action get bed by outlet id')
            console.log(result)
            var services = result.data
            commit('setServiceList', services)
            commit('share/setLoading', {payload: false} , { root: true })
          }).catch((err) => {
            console.log(err)
        });
    },
   
    loadbedBedService({commit}, payload){
        commit('share/setLoading', {payload: true} , { root: true })
        HTTP_API({
            method: 'get',
            url: `bed/service/${payload}`,
            headers:{
              // Authorization:
            },
        })
        .then((result) => {
            console.log('action get bed by outlet id')
            console.log(result)
            var bedServices = result.data
            commit('setbedServiceList', bedServices)
            commit('share/setLoading', {payload: false} , { root: true })
          }).catch((err) => {
            console.log(err)
        });
    },
    addNewbed({commit}, payload){
        commit('share/setLoading', {payload: true} , { root: true })
        HTTP_API({
            method: 'post',
            url: `bed`,
            headers:{
              // Authorization:
            },
            data: {
                roomID: payload.roomID,
                isEnable: payload.isEnable,
                serviceID: payload.serviceID
            }
        })
        .then((result) => {
            console.log('add new bed')
            console.log(result)
            var noti = {
                type: null,
                mes: result.data.msg
            }
            if (result.data.result == true)
                noti.type = 'success'
            else
                noti.type = 'error'
            commit('share/setLoading', {payload: false} , { root: true })
            commit('share/setNoti', {payload: noti}, { root: true })
        }).catch((err) => {
            console.log(err)
        });
    },
    editbed({commit}, payload){
        commit('share/setLoading', {payload: true} , { root: true })
        HTTP_API({
            method: 'put',
            url: `bed`,
            headers:{
              // Authorization:
            },
            data: {
                bedID: payload.bedID,
                roomID:  payload.roomID,
                isEnable: payload.isEnable,
                serviceID: payload.serviceID
            }
        })
        .then((result) => {
            console.log('add new bed')
            console.log(result)
            var noti = {
                type: null,
                mes: result.data.msg
            }
            if (result.data.result == true)
                noti.type = 'success'
            else
                noti.type = 'error'
            commit('share/setLoading', {payload: false} , { root: true })
            commit('share/setNoti', {payload: noti}, { root: true })
        }).catch((err) => {
            console.log(err)
        });
    },
    setBedServices({commit}, payload){
        console.log('setBedServices = ' + payload )
        commit('setBedServices', payload)
    }
}

// Để truyền dữ liệu sau khi gọi API ở action vào phần State, phải gọi các hàm ở phần Mutations
const mutations = {
    setbedList(state, payload){
        console.log('mutation get bed')
        state.beds = payload
    } ,

    setServiceList(state, payload){
        console.log('mutation get service')
        state.services = payload
    } ,
   
    setbedServiceList(state, payload){
        console.log('mutation get bedService')
        state.bedServices = payload
    },
    setBedServices(state, payload){
        state.bedServices.push(payload.pop())
    }
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}
