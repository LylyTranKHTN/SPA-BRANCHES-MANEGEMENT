import axios from 'axios'

export const HTTP_API = axios.create({
// baseURL điền cái localhost:port
baseURL: 'http://localhost:5828/',
headers: {"Access-Control-Allow-Origin": "*"}
})

// Lưu dữ liệu sau khi gọi API ở đây
// Xem ví dụ bên thư mục customer
const state = {

   
}

// getters
const getters = {
}

// Viết các hàm gọi API ở đây
// Xem ví dụ bên thư mục customer
const actions = {
  
}

// Để truyền dữ liệu sau khi gọi API ở action vào phần State, phải gọi các hàm ở phần Mutations
const mutations = {
    
    
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}
