<template>
   <v-container>
      <v-layout column align-center >
          <span class="pb-3" > Verify Code</span>
          <v-text-field
              single-line
              v-model="code_input"
              outline
            >
            </v-text-field>
            <span  class="red--text">{{errtext}}</span>
            <span  class="red--text">{{errsubmit}}</span>
            <v-flex xs3 justify-center row>
                <v-btn  color="cyan" v-on:click="submitcode()">
                    Submit
                </v-btn>
                <v-btn  color="cyan" v-on:click="verycodeact()" >
                    ReSend
                </v-btn>
            </v-flex>
           
       
      </v-layout>
   </v-container>
</template>

<script>
import secretkey from '../auth/constant'

import { mapState, mapActions } from 'vuex'
var encryptor = require('simple-encryptor')('test1234567890234567890wertyuifghjdkjhgkjhergej');    
export default {

    data () {
      return {
        code_input:null,
        username: "abc",
        code:null,
        sendfrom:'+16623378292',
        tophone:'0708508857',
        errsubmit:null,
        errsendcode:'',
    }
    },
    computed :{
      ...mapState({
            // customer: state => state.customer.customerabc,
            seretkey:state => state.user.seretkey,
            errtext:state=>state.user.errtxt,
            
        })
    },
    props:
    {
        
    },
    methods:{
      verycodeact(){
        console.log('bd chay ham verycode')
        var encrypted = encryptor.encrypt(this.username);
        this.code=(encrypted).slice(0,6);
        this.errsubmit=null;
        // console.log((encrypted).slice(0,6))
        var msgtwilio= {
            message:(encrypted).slice(0,6),
            sendfrom:this.sendfrom,
            phone:(this.tophone).substring(1,11),
        }
        // console.log(msgtwilio)
        this.$store.dispatch('user/SendSMS', msgtwilio)
        console.log("kết thúc")
        },
        submitcode(){
            console.log(this.code_input)
            console.log(this.code)
        if (this.code_input!=this.code)
            this.errsubmit= 'mã chưa chính xác'
            
        },
    },

    };
    
</script>



