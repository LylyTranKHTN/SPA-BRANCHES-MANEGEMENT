import axios from 'axios'
import router from '@/router'

export const HTTP_API = axios.create({
// baseURL điền cái localhost:port
  baseURL: 'http://localhost:8000/',
  //headers: {"Access-Control-Allow-Origin": "*"}
})

// Lưu dữ liệu sau khi gọi API ở đây
// Xem ví dụ bên thư mục customer
const state = {
  outlet: {
    id: 1
  },
  outlets: [],
}

// getters
const getters = {

}

// Viết các hàm gọi API ở đây
// Xem ví dụ bên thư mục customer
const actions = {
  getAllOutlet({commit}){
    commit('share/setLoading', {payload: true} , { root: true })
    HTTP_API({
      method: 'get',
      url: `outlet/all`,
      headers:{
        // Authorization:
      },
    })
    .then((result) => {
      console.log('action getoutlet')
      console.log(result)
      var outlets = result.data
      commit('setOutletLoaded', outlets)
      commit('share/setLoading', {payload: false} , { root: true })
    }).catch((err) => {
      console.log(err)
    });
  },

  changeIDCurrentOutLet({commit}, payload){
    console.log('changeIDCurrentOutlet = ' + payload )
    commit('setCurrentOutLet', payload)
  }
}

// Để truyền dữ liệu sau khi gọi API ở action vào phần State, phải gọi các hàm ở phần Mutations
const mutations = {
  setOutletLoaded(state, payload){
    state.outlets=payload
  },
  setOutletReviewLoaded(state, payload){
    state.outletReview = payload
  },
  setOutletByNameLoader(state, payload){
    state.outlets = payload
  },
  setAllOutlet(state, payload){
    state.outlets = payload
  },
  setCurrentOutLet(state, payload){
    console.log('commit ' + payload)
    state.outlet = payload
  }
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}
