import axios from 'axios'
import router from '@/router'

export const HTTP_API = axios.create({
  baseURL: 'http://localhost:5828/',
  //headers: {"Access-Control-Allow-Origin": "*"}
})

// data store
const state = {
  therapists:[],
  timeOfSlot:[],
}

// data fillter
const getters = {
  
}

// api calling
const actions = {
    getTherapistsByServiceOutlet({commit},serviceOutletID){
        // request
        HTTP_API({
            method:'get',
            url: `therapist/service/${serviceOutletID.serviceID}/outlet/${serviceOutletID.outletID}`,
            headers:{}
        // get result
        }).then((result)=>{
            console.log('getTherapistsByServiceOutlet (store module)')
            console.log(serviceOutletID.serviceID)
            console.log(serviceOutletID.outletID)
            var therapistList=result.data.result.data
            commit('setTherapistsByServiceOutlet',therapistList)
            console.log(therapistList)
            console.log('getTherapistsByServiceOutlet (store module) success')
        // handing err
        }).catch((err)=>{
            console.log(err)
        })
    }
}
  

// map data to store
const mutations = {
  setTherapistsByServiceOutlet(state,therapistList){
      state.therapists=therapistList
  }
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}
