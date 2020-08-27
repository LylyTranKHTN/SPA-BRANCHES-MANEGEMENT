import axios from 'axios'
import router from '@/router'
export const HTTP_API = axios.create({
// baseURL điền cái localhost:port
  baseURL: 'http://localhost:5828/',
  headers: {
  'Access-Control-Allow-Origin': "*",
}
})

const state = {
    secretkey:'Abc12345',
    errtxt:'',
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
    phone_isexist:null,
    email_isexist:null,
    nric_isexist:null,
    register: false
}

// getters
const getters = {
}

// Viết các hàm gọi API ở đây
// Xem ví dụ bên thư mục customer
const actions = {
    SendSMS({commit}, payload){
        HTTP_API({
            method: 'get',
            url:`service/verycode/${payload.phone}/${payload.message}`

        })
        .then((result) => {
            var error=result.data.result.data
            console.log(error)
            commit('seterror',error)
            return result.data.result.data
        }).catch((err) => {
            // console.log(err)
            commit('seterror',error)
            return "server error"
        })
    },
    logIn({commit}, payload) {
        console.log('thực hiện login')
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
            role: 'therapist'
          }
        })
      // Kết quả sau khi gọi API
        .then((result) => {
          console.log('action login')
          console.log(result)
          if (result.data.hasOwnProperty('error')) {
            alert(result.data.error)
          }
          commit('setToken', token)
        }).catch((err) => {
          console.log(err)
        });
    },
    register({commit}, payload) {
        console.log('thực hiện resister :' , payload)
      if (this.passWord!=this.confirmPass)
          console.log('wfewefwefwefwe ')
        HTTP_API({
          method: 'post',
          url: 'therapist',
          data:{
            DoB: payload.DoB,
            Email:payload.email,
            Gender: payload.Gender,
            Name: payload.Name,
            passWord: payload.PassWord,
            Possition:3,
            Phone: payload.Phone,
            userName:payload.email,
            //ConfirmPassWord: payload.ConfirmPassWord
          }
        })
        .then((result) => {
          console.log('register actions 1234');
          console.log(payload);
          if (result.data.hasOwnProperty('error')){
            alert(result.data.error.errorMessage);
            alert(result.data.error.validation.errorMessage)
          }
          else {

            var usernamePass = {
            user : payload.Name,
            pass : payload.PassWord,
          };

          commit('setUsernamePassWord', usernamePass);
          router.push('/')
          }

        })
        .catch((err) => {
          console.log('catch');
          console.log(err)
        });
      },
    getTherapistInfoByUserNamePassword({commit}, payload){
      console.log('thực hiện login 2');
        HTTP_API({
          method: 'get',
          url: `therapist/profile/${payload.user}/${payload.pass}`,
          headers: {
    
          }
        }).then((result) => {
          console.log('getTherapistInfoByUserNamePassword');
          console.log(result);
          if (result.data.hasOwnProperty('error')){
            alert(result.data.error.validation.errorMessage)
          }
          else {

            var userinfo = result.data.result.data;
            commit('setUserInfo', userinfo);
            router.push('/');
          }

          
        }).catch((err) => {
          console.log(err)
        });
      },
    checkExistAccount({commit}, payload){
        console.log('thực hiện check exist')
        HTTP_API({
          method: 'get',
          url: `therapist/isexist`,
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
      }

    
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
      state.userInfo.id = payload.id,
      state.userInfo.phone = payload.phone,
      state.userInfo.blackList = payload.blackList,
      state.userInfo.dob = payload.doB,
      state.userInfo.gender = payload.gender,
      state.userInfo.nric = payload.nric,
      state.userInfo.avatar = payload.avatar
    },
    setEmailIsexist(state,payload){
      state.email_isexist=payload
    },
    setPhoneIsexist(state,payload){
      state.phone_isexist=payload
    },
    setNRICIsexist(state,payload) {
      state.nric_isexist = payload
    }
  }
  export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
  }
