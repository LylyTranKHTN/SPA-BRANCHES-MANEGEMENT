import axios from 'axios'


export const HTTP_API = axios.create({
// baseURL điền cái localhost:port
baseURL: 'http://localhost:5828/',
headers: {"Access-Control-Allow-Origin": "*"}
})

// Lưu dữ liệu sau khi gọi API ở đây
// Xem ví dụ bên thư mục customer
const state = {
  therapists: [],
  therapist: {},
  therapistsSearch: [],
  timeSlots:[]
}

// getters
const getters = {
  TherapistsInOutletDetail: (state) => {
    return state.therapists.slice(0, 3)
  }
}

// Viết các hàm gọi API ở đây
// Xem ví dụ bên thư mục customer
const actions = {
  getTherapistbyServiceOutlet({commit}, payload){
    HTTP_API({
      method: 'get',
      url: `therapist/service/${payload.idService}/outlet/${payload.idOutlet}`,
      headers:{
        // Authorization:
      }
    })
    .then((result) => {
      console.log(result)
      console.log('action getTherapistbyServiceOutlet')
      var therapists = result.data.result.data
      commit('setTherapistbyServiceOutlet', therapists)
    }).catch((err) => {
      console.log(err)
    });
  },
  getTherapistsByOutlet({commit}, payload){
    HTTP_API({
      method: 'get',
      url: `therapist/outlet/${payload.outletID}/${payload.pageSize}/${payload.pageNumber}`,
      headers:{
        // Authorization:
      }
    })
    .then((result) => {
      console.log('action get TherapistByOutlet')
      console.log(result)
      var therapists = result.data.result.data
      commit('setTherapistByOutlet', therapists)
    })
    .catch((err) => {
      console.log(err)
    });
  },

  getTherapistsByName({commit}, payload){
    HTTP_API({
      method: 'get',
      url: `therapist/name/${payload}`,
      header: {
        //
      }
    }).then((result) => {
      console.log('get therapist by name')
      console.log(result)
      var therapist = result.data.result.data
      commit('setTherapistByName', therapist)
    }).catch((err) => {
      console.log(err)
    });
  },

  getTherapistsByID({commit}, payload){
    HTTP_API({
      method: 'get',
      url: `therapist/${payload}`,
      header: {
        //
      }
    }).then((result) => {
      console.log('get therapist by id')
      console.log(result)
      var therapist = result.data.result.data
      commit('setTherapistByID', therapist)
    }).catch((err) => {
      console.log(err)
    });
  },

    getTherapistsByNameandOutlet({commit}, payload){
      HTTP_API({
        method: 'get',
        url: `therapist/outlet/name/${payload.outletID}/${payload.therapistName}`,
        header: {
          //
        }
      }).then((result) => {
        console.log('get therapist by name and outlet')
        console.log(result)
        var therapist = result.data.result.data
        commit('setTherapistByNameandOutlet', therapist)
      }).catch((err) => {
        console.log(err)
      });
    }
}

// Để truyền dữ liệu sau khi gọi API ở action vào phần State, phải gọi các hàm ở phần Mutations
const mutations = {
  setTherapistbyServiceOutlet(state, payload){
    state.therapists = payload
  },
  setTherapistByOutlet(state, payload){
    state.therapists = payload
  },
  setTherapistByName(state, payload){
    state.therapists = payload
  },
  setTherapistByID(state, payload){
    state.therapist = payload
  },
  setTherapistByNameandOutlet(state, payload){
    state.therapistsSearch = payload
  }
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}
