export default {
    namespaced: true,
    state: {
      loading: false,
      noti: null
    },
    mutations: {
      setLoading (state, {payload}) {
        state.loading = payload
      },
      setNoti (state, {payload}) {
        state.noti = payload
      },
      clearNoti (state) {
        state.noti = null
      }
    },
    actions: {
      clearNoti ({commit}) {
        console.log('action clear noti')
        commit('clearNoti')
      }
    },
    getters: {
      loading (state) {
        return state.loading
      },
      error (state) {
        return state.error
      }
    }
  }
