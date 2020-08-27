import axios from 'axios'
import router from '@/router'
export const HTTP_API = axios.create({
// baseURL điền cái localhost:port
  baseURL: 'http://localhost:5828/',
  headers: {
  'Access-Control-Allow-Origin': "*",
}
})

// Lưu dữ liệu sau khi gọi API ở đây
// Ví dụ customer cần phải lưu token và userName để sử dụng cho các API sau.
const state = {
  userInfo: {
    id: 0,
    name:null,
    blackList: 0,
    dob: null,
    age: 0,
    gender:0,
    nric:0,
    phone:null,
    avatar: null,
    token: null,
    userName: null,
    passWord: null,
    expires_in: null,
    profession: null,
    email:null,
  },
  measurement:{},
  register: false,
  phone_isexist:null,
  email_isexist:null,
  nric_isexist:null,
}

// getters
const getters = {
}

// Viết các hàm gọi API ở đây
// Ví dụ; hàm logIn được viết như dưới.
const actions = {
  // hàm login -> gọi API login
  logIn({commit}, payload) {
    commit('share/setLoading', {payload: true} , { root: true })

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
        role: 'customer',
        "Access-Control-Allow-Origin": "*"
      }

    })
  // Kết quả sau khi gọi API
    .then((result) => {
      console.log(result)
      if (result.data.hasOwnProperty('error')){
        var mes = 'Error: ' + result.data.error.validation.errorMessage
        var noti = {
          mes: mes,
          type: 'error'
        }
        commit('share/setNoti', {payload: noti} , { root: true })
        commit('share/setLoading', {payload: false} , { root: true })

      }
      else {
        var userInfo = {
          username: payload.user,
          password: payload.pass,
          token: result.data.access_token,
          expires_in: result.data.expires_in
        } 
        commit('share/setLoading', {payload: false} , { root: true })
        commit('setToken', token)
        //router.push('/home')  
      }
          }).catch((err) => {
      console.log(err)
    });
  },
  register({commit}, payload) {
    commit('share/setLoading', {payload: true} , { root: true })
    HTTP_API({
      method: 'post',
      url: 'customer',
      data:{
        Name: payload.Name,
        DoB: payload.DoB,
        Gender: payload.Gender,
        PassWord: payload.PassWord,
        NRIC: payload.NRIC,
        Email:payload.Email,
        Phone: payload.Phone,
        ConfirmPassWord: payload.ConfirmPassWord
      }
    })
    .then((result) => {
      console.log('register actions')
      if (result.data.hasOwnProperty('error')){
        var mes = 'Error: ' + result.data.error.validation.errorMessage
        var noti = {
          mes: mes,
          type: 'error'
        }
        commit('share/setNoti', {payload: noti} , { root: true })
        commit('share/setLoading', {payload: false} , { root: true })
      }
      else {
        var usernamePass = {
        user : payload.Name,
        pass : payload.PassWord,
        } 
        commit('share/setLoading', {payload: false} , { root: true })
        commit('setUsernamePassWord', usernamePass)
        router.push('/home')
      }

    })
    .catch((err) => {
      console.log('catch')
      console.log(err)
    });
  },
  editCustomer({commit}, payload){
    commit('share/setLoading', {payload: true} , { root: true })
    HTTP_API({
      method: 'put',
      url: 'customer',
      data:{
        ID: payload.Id,
        Name: payload.Name,
        DoB: payload.DoB,
        Gender: payload.Gender,
        PassWord: payload.PassWord,
        NRIC: payload.NRIC,
        Phone: payload.Phone,
        Email: payload.Email
      }
    })
    .then((result) => {
      console.log('edit customer profile')
      if (result.data.hasOwnProperty('error')){
        var mes = 'Error: ' + result.data.error.validation.errorMessage
        var noti = {
          mes: mes,
          type: 'error'
        }
        commit('share/setNoti', {payload: noti} , { root: true })
        commit('share/setLoading', {payload: false} , { root: true })
      }
    })
    .catch((err) => {
      console.log('catch')
      console.log(err)
    });
  },
  getCustomerInfoByUserNamePassword({commit}, payload){
    commit('share/setLoading', {payload: true} , { root: true })

    HTTP_API({
      method: 'post',
      url: `customer/profile`,
      data:{
        username: payload.user,
        password: payload.pass
      },
      headers: { }
    }).then((result) => {
      if (result.data.hasOwnProperty('error')){
        var mes = 'Error: ' + result.data.error.validation.errorMessage
        var noti = {
          mes: mes,
          type: 'error'
        }
        commit('share/setNoti', {payload: noti} , { root: true })
        commit('share/setLoading', {payload: false} , { root: true })
      }
      else {
        var userinfo = result.data.result.data
        commit('share/setLoading', {payload: false} , { root: true })
        commit('setUserInfo', userinfo)
        router.push('/home')
      }

      
    }).catch((err) => {
      console.log(err)
    });
  },
  getCustomerMeasurement({commit}, payload){
    commit('share/setLoading', {payload: true} , { root: true })
    HTTP_API({
      method: 'get',
      url: `customer/measurement/${payload}`,
      headers: { }
    }).then((result) => {
      if (result.data.hasOwnProperty('error')){
        var mes = 'Error: ' + result.data.error.validation.errorMessage
        var noti = {
          mes: mes,
          type: 'error'
        }
        commit('share/setNoti', {payload: noti} , { root: true })
        commit('share/setLoading', {payload: false} , { root: true })

      }
      else {
      var userinfo = result.data.result.data
      commit('share/setLoading', {payload: false} , { root: true })
      commit('setmeasurement', userinfo)
    }
    }).catch((err) => {
      console.log(err)
    });
  },
  checkExistAccount({commit}, payload){
        console.log('thực hiện check exist')
        HTTP_API({
          method: 'get',
          url: `customer/isexist`,
          params:{
            info:payload.info,
          },
          headers: {
          }
        }).then((result) => {
          var kq =result.data.result.data
          if(payload.type=='email')
          {
            commit('setEmailIsexist',kq)
            console.log('kq SAU KHI CHECK email',state.email_isexist)
          }

          else if(payload.type=='nric')
          {
            commit('setNRICIsexist',kq)
            console.log('kq SAU KHI CHECK NRIC',state.nric_isexist)
          }
          else {
            commit('setPhoneIsexist',kq)
            console.log('kq SAU KHI CHECK Phone',state.phone_isexist)
          }

        }).catch((err) => {
          console.log('lỗi  với pr ', payload)
          console.log(err)
        });
      },
  SendSMS({commit}, payload){
    commit('share/setLoading', {payload: true} , { root: true })
        HTTP_API({
            method: 'get',
            url:`service/verycode/${payload.phone}/${payload.message}`
        })
        .then((result) => {
            var error=result.data.result.data
            console.log(error)
            commit('share/setLoading', {payload: false} , { root: true })
            commit('seterror',error)
            return result.data.result.data
        }).catch((err) => {
            // console.log(err)
            commit('seterror',error)
            return "server error"
        })
    },
}

// Để truyền dữ liệu sau khi gọi API ở action vào phần State, phải gọi các hàm ở phần Mutations
const mutations = {
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
    state.userInfo.name = payload.name,
    state.userInfo.id = payload.id,
    state.userInfo.phone = payload.phone,
    state.userInfo.blackList = payload.blackList,
    state.userInfo.dob = payload.doB,
    state.userInfo.gender = payload.gender,
    state.userInfo.nric = payload.nric,
    state.userInfo.avatar = payload.avatar,
    state.userInfo.profession = payload.profession,
    state.userInfo.email = payload.email
  },
  setmeasurement(state, payload){
    state.measurement = payload
  },
  setEmailIsexist(state,payload){
      state.email_isexist=payload
    },
  setPhoneIsexist(state,payload){
    state.phone_isexist=payload
  },
  setNRICIsexist(state,payload) {
    state.nric_isexist = payload
  },
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}
