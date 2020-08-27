<template>
 <div id="app">
<h1>login fb</h1>
<facebook-login class="button"
      appId="1878947162211144"
      @login="onLogin"
      @logout="onLogout"
      @sdk-loaded="sdkLoaded">
    </facebook-login>
 </div>
 
</template>


<script>
import Vue from 'vue'
import facebookLogin  from 'facebook-login-vuejs';
export default {
  name:"app",
components :{
  facebookLogin
},
methods: {
    getUserData() {
      this.FB.api('/me', 'GET', { fields: 'id,name,email' },
        userInformation => {
          console.warn("get data from facebook", userInformation)
          this.personalID = userInformation.id;
          this.email = userInformation.email;
          this.name = userInformation.name;
        }
      )
    },
    sdkLoaded(payload) {
      this.isConnected = payload.isConnected
      this.FB = payload.FB
      if (this.isConnected) this.getUserData()
    },
    onLogin() {
      this.isConnected = true
      this.getUserData()
    },
    onLogout() {
      this.isConnected = false;
    }
  }
}
</script>


