import axios from 'axios'
import router from '@/router'

export const HTTP_API = axios.create({
// baseURL điền cái localhost:port
  baseURL: 'http://localhost:5828/',
  //headers: {"Access-Control-Allow-Origin": "*"}
})

// Lưu dữ liệu sau khi gọi API ở đây
// Xem ví dụ bên thư mục customer
const state = {
  outlet: {
    id: 1
  },
  outlets: [],
  outletReview: [],
  addReview: false,
}

// getters
const getters = {
  ReviewsInOutletDetail: (state) => {
    return state.outletReview.slice(0, 5)
  }
}

// Viết các hàm gọi API ở đây
// Xem ví dụ bên thư mục customer
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
    commit('share/setLoading', {payload: true} , { root: true })
    HTTP_API({
      method: 'get',
      url: `outlet/name/${payload}`,
      headers:{

      }
    }).then((result) =>{
      console.log('get outlet by name')
      var outletByName = result.data.result.data
      console.log(outletByName)
      commit('share/setLoading', {payload: false} , { root: true })
      commit('setOutletByNameLoader', outletByName)
    }).catch((err) =>{
      console.log(err)
    });
  },

  getAllOutlet({commit}){
    commit('share/setLoading', {payload: true} , { root: true })

    HTTP_API({
      method: 'get',
      url: `outlet/all`,
      headers: {
        Authorization: 'Bearer vBL0R-tZ-qilAXqieu4TzC86P2Y9TBBj7Qn17Pe7MzH5OIDfyyGdMpuiy0iUWB4DhOOuQr5g7XV9QuE5qDECBwMM6IeTYNnU5WMxHydi906L5GfacGgdGBbJGifZyXhwxLADT-K6BbCWZFaRuLPrjCI_xtDisMP46qMqO0vFfIEV_HAx4oJiXk7CnHFJwVb4fclYYEfjQu_Ch7IM56HxR8yWA3Rd2dAdFmVGcJIgP0wpcLSL7iueKRkCx1wtjtqxcxrwzQC3O_I4W4afyayfGudK4pXOT9iUuAnmyOHOX9svwPrUeUMwrU9Im2yvV1fv'
      }
    }).then((result) => {
      console.log('get all outlet')
      var alloutlet = result.data.result.data
      console.log(alloutlet)
      commit('share/setLoading', {payload: false} , { root: true })
      commit('setAllOutlet', alloutlet)
    }).catch((err) => {
      console.log(err)
    });
  },
  addReview({commit}, payload){
    HTTP_API({
      method: 'post',
      url: `outlet/review`,
      headers: {
        //
      },
      data:{
        outletId: payload.outletId,
	      customerid: payload.customerid,
	      star: payload.star,
	      content: payload.content
      }
    }).then((result) => {
      console.log('add review outlet')
      console.log(result)
      router.push('/outlet/reviews')
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
  setOutletReviewLoaded(state, payload){
    state.outletReview = payload
  },
  setOutletByNameLoader(state, payload){
    state.outlets = payload
  },
  setAllOutlet(state, payload){
    state.outlets = payload
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
