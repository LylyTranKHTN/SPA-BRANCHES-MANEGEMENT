import axios from 'axios'

export const HTTP_API = axios.create({
// baseURL điền cái localhost:port
  baseURL: 'http://localhost:5828/',
  headers: {"Access-Control-Allow-Origin": "*"}
})

const state = {
  booking: {
    id: 1
  },
    bookings:[],
}

// getters
const getters = {
}

// Viết các hàm gọi API ở đây
// Xem ví dụ bên thư mục customer
const actions = {
  getBookingByOutletandCustomername({commit}, payload){
   
  },
};
// Để truyền dữ liệu sau khi gọi API ở action vào phần State, phải gọi các hàm ở phần Mutations
const mutations = {
  setBookingload(state, payload){
    state.bookinglist = payload
  },
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}
