import axios from 'axios'

export const HTTP_API = axios.create({
// baseURL điền cái localhost:port
  baseURL: 'http://localhost:5828/',
  headers: {"Access-Control-Allow-Origin": "*"}
})

// Data Store
const state = {
  services:[]
}

// Getter (State Filter)
const getters = {
}

// API calling
const actions = {
  getServicesByOutletID({commit},outletID){
      HTTP_API({
          method:'get',
          url: `service/outlet/${outletID}`,
          headers:{
              // Authorization
          }
      }).then((result)=>{
          var servicesList=result.data.result.data
          console.log('getServicesByOutletID (store module)')
          console.log(outletID)
          console.log(servicesList)
          commit('setServicesByOutletID',servicesList)
          console.log('getServicesByOutletID (store module) success')
      }).catch((err)=>{
          console.log(err)
      })
  }
};

// Set State Value
const mutations = {
  setServicesByOutletID(state,servicesList){
      state.services=servicesList
  }
}

// Set part of this file
export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}
