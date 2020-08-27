import axios from 'axios'
import { mapState } from 'vuex'

export const HTTP_API = axios.create({
  baseURL: 'http://localhost:5828',
//   headers: {'X-Requested-With': 'XMLHttpRequest'},
})

// Danh sách API
export default {
    // login
    logIn (){
        return  HTTP_API({
            method: 'get',
            url: '',
            params:{

            }
        })
    },

    // register
    register (user) {
        return HTTP_API({
            method: 'post',
            url: '/Customer',
            params: {
                Name: user.name,
                DoB: user.dob,
                Gender: user.gender,
                PassWord: user.pass,
                NRIC: user.nric,
                phone: user.phone,
                ConfirmPassWord: user.confimrPass
            }
        })
    },

    // get outlet list
    getOutletList() {
        return  HTTP_API({
            method: 'get',
            url: '',
            params:{

            }
        })
    }
}
