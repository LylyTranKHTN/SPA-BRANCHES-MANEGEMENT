import axios from 'axios'

export const HTTP_API = axios.create({
// baseURL điền cái localhost:port
baseURL: 'http://localhost:8000/',
headers: {"Access-Control-Allow-Origin": "*"}
})

// Lưu dữ liệu sau khi gọi API ở đây
// Xem ví dụ bên thư mục customer
const state = {
    rooms: []
}

// getters
const getters = {
}

// Viết các hàm gọi API ở đây
// Xem ví dụ bên thư mục customer
const actions = {
    loadRoomByOutletID({commit}, payload){
        commit('share/setLoading', {payload: true} , { root: true })
        HTTP_API({
            method: 'get',
            url: `room/${payload.outletID}`,
            headers:{
              // Authorization:
            },
        })
        .then((result) => {
            console.log('action get room by outlet id')
            console.log(result)
            var rooms = result.data
            if(rooms.length == 0){
                var noti = {
                    type: 'warning',
                    mes: 'Warning: This outlet has not rooms! Please add rooms!'
                }
                commit('share/setNoti', {payload: noti}, { root: true })
            }
            commit('setRoomList', rooms)
            commit('share/setLoading', {payload: false} , { root: true })
          }).catch((err) => {
            console.log(err)
        });
    },
    addNewRoom({commit}, payload){
        commit('share/setLoading', {payload: true} , { root: true })
        HTTP_API({
            method: 'post',
            url: `room`,
            headers:{
              // Authorization:
            },
            data: {
                name: payload.name,
                numofbed: payload.numofbed,
                outlet: payload.outlet,
                deleted: payload.deleted,
                createdate: new Date().toISOString(),
                recordversion: payload.recordversion
            }
        })
        .then((result) => {
            console.log('add new room')
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
    editRoom({commit}, payload){
        commit('share/setLoading', {payload: true} , { root: true })
        HTTP_API({
            method: 'put',
            url: `room`,
            headers:{
              // Authorization:
            },
            data: {
                id: payload.id,
                name: payload.name,
                outlet: payload.outlet,
                deleted: payload.deleted,
                updatedate: new Date().toISOString(),
                recordversion: payload.recordversion
            }
        })
        .then((result) => {
            console.log('add new room')
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
    loadRooms({commit}, payload){
        commit('share/setLoading', {payload: true} , { root: true })
        HTTP_API({
            method: 'get',
            url: `room`,
            headers:{
              // Authorization:
            },
        })
        .then((result) => {
            console.log('action get all rooms')
            console.log(result)
            var services = result.data
            commit('setRoomList', services)
            commit('share/setLoading', {payload: false} , { root: true })
          }).catch((err) => {
            console.log(err)
        });
    },
}

// Để truyền dữ liệu sau khi gọi API ở action vào phần State, phải gọi các hàm ở phần Mutations
const mutations = {
    setRoomList(state, payload){
        console.log('mutation get room')
        state.rooms = payload
    }  
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}
