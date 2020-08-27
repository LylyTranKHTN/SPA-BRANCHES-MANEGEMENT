import axios from 'axios'
import router from '@/router'

export const HTTP_API = axios.create({
// baseURL điền cái localhost:port
  baseURL: 'http://localhost:5828/',
  //headers: {"Access-Control-Allow-Origin": "*"}
})

// data store
const state = {
  outlet: {},
  outlets: [],
  outletReview: [],
  addReview: false,
}

// data fillter
const getters = {
  ReviewsInOutletDetail: (state) => {
    return state.outletReview.slice(0, 5)
  }
}

// api calling
const actions = {
  getOutletByID({commit}, payload){
    HTTP_API({
      method: 'get',
      url: `outlet/${payload}`,
      headers:{
        // Authorization:
      },
    })
    .then((result) => {
      console.log('action getoutlet')
      console.log(result)
      var outlet = result.data.result.data
      commit('setOutletLoaded', outlet)
    }).catch((err) => {
      console.log(err)
    });
  },
  getOutletReview({commit}, payload){
    HTTP_API({
      method: 'get',
      url: `outlet/review/${payload}`,
      headers:{

      }
    })
    .then((result) => {
      console.log('get outlet review')
      console.log(result)
      var outletReview = result.data.result.data
      commit('setOutletReviewLoaded', outletReview)
    })
    .catch((err) => {
      console.log(err)
    });
  },

  getOutletByName({commit}, payload){
    HTTP_API({
      method: 'get',
      url: `outlet/name/${payload}`,
      headers:{

      }
    }).then((result) =>{
      console.log('get outlet by name')
      var outletByName = result.data.result.data
      commit('setOutletByNameLoader', outletByName)
    }).catch((err) =>{
      console.log(err)
    });
  },

  getAllOutlet({commit}){
    HTTP_API({
      method: 'get',
      url: `outlet/all`,
      header: {
        //
      }
    }).then((result) => {
      var outletList = result.data.result.data
      console.log('getAllOutlet (store module)')
      commit('setOutletsByGetAll', outletList)
      console.log('getAllOutlet (store module) success')
    }).catch((err) => {
      console.log(err)
    });
  },

  changeIDCurrentOutLet({commit}, payload){
    console.log('changeIDCurrentOutlet = ' + payload )
    commit('setIDCurrentOutLet', payload)
  }
}

// Để truyền dữ liệu sau khi gọi API ở action vào phần State, phải gọi các hàm ở phần Mutations
const mutations = {
  setOutletLoaded(state, payload){
    state.outlet=payload
  },
  setOutletByNameLoader(state, payload){
    state.outlets = payload
  },
  setOutletsByGetAll(state, outletList){
    state.outlets = outletList
  },
  setIDCurrentOutLet(state, payload){
    console.log('commit ' + payload)
    state.outlet.id = payload
  }
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}
