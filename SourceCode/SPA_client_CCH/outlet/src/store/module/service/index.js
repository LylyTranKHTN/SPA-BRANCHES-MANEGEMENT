import axios from 'axios'

export const HTTP_API = axios.create({
// baseURL điền cái localhost:port
baseURL: 'http://localhost:5828/',
headers: {"Access-Control-Allow-Origin": "*"}
})

// Lưu dữ liệu sau khi gọi API ở đây
// Xem ví dụ bên thư mục customer
const state = {
  services : []
   
}

// getters
const getters = {
}

// Viết các hàm gọi API ở đây
// Xem ví dụ bên thư mục customer
const actions = {
  getServicesByOutlet({commit}, payload){
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
      commit('setServiceByOutletLoaded', servicesbyoutlet)
    }).catch((err) => {
      console.log(err)
    });
  },

}

// Để truyền dữ liệu sau khi gọi API ở action vào phần State, phải gọi các hàm ở phần Mutations
const mutations = {
  setServiceByOutletLoaded(state, payload){
    state.services = payload
  },
    
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}
