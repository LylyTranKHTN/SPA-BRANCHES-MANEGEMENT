import axios from 'axios'

export const HTTP_API = axios.create({
// baseURL điền cái localhost:port
  baseURL: 'http://localhost:5828/',
  headers: {"Access-Control-Allow-Origin": "*"}
})

const state = {
    customer:{},
    userInfo: {
      id: 0,
      blackList: 0,
      dob: null,
      gender:0,
      nric:0,
      phone:null,
      avatar: null,
      token: null,
      userName: null,
      passWord: null,
      expires_in: null
    },

    bodymeasurement:{},
    customers:[],
    register: false
}

// getters
const getters = {
}

// Viết các hàm gọi API ở đây
// Xem ví dụ bên thư mục customer
const actions = {

  // function update password for customer
  updatePassword({commit},payload)
  {
    HTTP_API({
      method: 'put',
      url: 'customer',
      // params là các tham số để gọi API
      data: payload,
      headers: {}
    }).then((result)=>{
      commit('setCustomerload',payload)
      console.log('update successfully')
      alert('update successfully')

  }).catch(err=>{
    console.log('catch')
    console.log(err)
    })
  },
// hàm login

  logIn({commit}, payload) {
  HTTP_API({
    method: 'post',
    url: 'api/token',
    // params là các tham số để gọi API
    data:{
      grant_type: 'password',
      username: payload.user,
      password: payload.pass
    },
    headers:{
      role: 'customer'
    }
  }).then((result) => {
    console.log(result)
    if (result.data.hasOwnProperty('error')){
      alert(result.data.error)
    }
    var userInfo = {
      username: payload.user,
      password: payload.pass,
      token: result.data.access_token,
      expires_in: result.data.expires_in
    } 
    commit('setToken', token)
    //router.push('/home')
  }).catch((err) => {
    console.log(err)
  });
},
  register({commit}, payload) {
  HTTP_API({
    method: 'post',
    url: 'customer',
    data:{
      Name: payload.Name,
      DoB: payload.DoB,
      Gender: payload.Gender,
      PassWord: payload.PassWord,
      NRIC: payload.NRIC,
      Phone: payload.Phone,
      ConfirmPassWord: payload.ConfirmPassWord
    }
  })
  .then((result) => {
    console.log('register actions')
    if (result.data.hasOwnProperty('error')){
      alert(result.data.error.errorMessage)
      alert(result.data.error.validation.errorMessage)
    }
    var usernamePass = {
      user : payload.Name,
      pass : payload.PassWord,
    } 
    commit('setUsernamePassWord', usernamePass)
    router.push('/home')
  })
  .catch((err) => {
    console.log('catch')
    console.log(err)
  });
},
  // hàm get customer by id
  get_info_customer_by_id({commit}, payload){
    HTTP_API({
      method: 'get',
      url: `customer/profile/${payload}`,
      headers:{
        // Authorization:
      }
    })
    .then((result) => {
      console.log('get  detail customer')
      console.log(result)
      var customerbyid = result.data.result.data
      commit('setCustomerload', customerbyid)
    }).catch((err) => {
      console.log(err)
    });
  },
  get_bodymeasurement_customer_by_id({commit}, payload){
    HTTP_API({
      method: 'get',
      url: `customer/measurement/${payload}`,
    }).then((result) => {
        var Bodymeasurement = result.data.result.data
        commit('setMeasurement', Bodymeasurement)
      }).catch((err) => {
        console.log(err)
      });
    },
    
  GetAllCustomerByOutlet({commit}, payload){
    HTTP_API({
      method: 'get',
      url: `customer/outlet/${payload.idOutlet}`,
      headers:{
        // Authorization:
      }
    })
    .then((result) => {
      console.log('get all customer by outlet')
      console.log(result)
      var customersbyoutlet = result.data.result.data
      commit('setCustomerByOutletLoaded', customersbyoutlet)
    }).catch((err) => {
      console.log(err)
    });
  },
  GetCustomerByOutletAndText({commit}, payload){
    HTTP_API({
      method: 'get',
      url: `customer/outlet/${payload.outletID}/txt/${payload.txt}`,
      header: {

      }
    }).then((result) => {
      console.log('get customer by outlet and text')
      console.log(result)
      var customerbyoutletandtext = result.data.result.data
      commit('setCustomerByOutletAndText', customerbyoutletandtext )
    }).catch((err) => {
      console.log(err)
    });
  },
};
// Để truyền dữ liệu sau khi gọi API ở action vào phần State, phải gọi các hàm ở phần Mutations
const mutations = {
  setCustomerload(state, payload){
    state.customer = payload
  },
  setTokenLoaded(state, payload) {
    state.userInfo.passWord = payload.passWord,
    state.userInfo.token = payload.token,
    state.userInfo.userName = payload.userName,
    state.userInfo.expires_in = payload.expires_in,
    console.log(state.token)
  },
  setUsernamePassWord(state, payload){
    state.userInfo.userName = payload.user,
    state.userInfo.passWord = payload.pass
  },
  setUserInfo(state, payload){
    state.userInfo.id = payload.id,
    state.userInfo.phone = payload.phone,
    state.userInfo.blackList = payload.blackList,
    state.userInfo.dob = payload.doB,
    state.userInfo.gender = payload.gender,
    state.userInfo.nric = payload.nric,
    state.userInfo.avatar = payload.avatar
  },
  setMeasurement(state,payload){
    state.bodymeasurement=payload
  },

  setCustomerByOutletLoaded(state, payload){
    state.customers = payload
  },
  setCustomerByOutletAndText(state, payload){
    state.customers = payload
  }

}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}
