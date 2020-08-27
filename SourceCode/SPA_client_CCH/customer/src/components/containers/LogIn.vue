<template>
    <v-container grid-list-md fluid>
       <!-- cover -->
       <v-layout row wrap px-1>
           <v-img
           height="200px"
            src="http://www.shangri-la.com/uploadedImages/Shangri-la_Hotels/Manila,_Makati_Shangri-La/health-leisure/spa/720x283-spa.jpg?width=720&height=283&mode=crop&quality=80">
           </v-img>
       </v-layout>

        <!-- login + register -->
            <v-tabs
                centered
                color="primary"
                dark
                icons-and-text
                grow
                slider-color="yellow"
            >
                <!-- tab login  -->
                <v-tab href="#tab-1">
                    Login
                    <v-icon>account_box</v-icon>
                </v-tab>
                <v-tab-item
                    value ="tab-1"
                >
                    <v-form>
                        <v-layout row wrap justify-space-around pt-4>
                            <!-- phone -->
                            <v-flex xs10 sm8 md8>
                                 <v-text-field
                                    v-model="phone"
                                    label="Phone Number"
                                    required
                                    outline
                                    hide-details
                                ></v-text-field>
                            </v-flex>
                            <!-- pass -->
                            <v-flex xs10 sm8 md8>
                                 <v-text-field
                                    v-model="pass"
                                    label="Password"
                                    :append-icon="show_pass ? 'visibility_off' : 'visibility'"
                                    @keyup.enter="login()"
                                    :type="show_pass ? 'text' : 'password'"
                                    required
                                    outline
                                    @click:append="show_pass = !show_pass"
                                ></v-text-field>
                            </v-flex>

                            

                            <!-- login-->

                             <v-flex xs10 sm5 md5>
                                 <v-btn
                                    block color="primary"
                                    @click="login()">
                                    Login
                                 </v-btn>
                            </v-flex>

                            </v-layout>
                             <v-layout
                            v-if="loading">
                            <v-progress-linear
                                :indeterminate="loading"
                                height="5"
                            >              
                            </v-progress-linear>
                            
                        </v-layout>

                        <!-- Shownoti -->
                        <v-layout row wrap v-if="noti != null">
                                    <app-alert
                                        :noti="noti"
                                        @dismissed="onDismissed"
                                        >
                                    </app-alert>
                                </v-layout>


                    </v-form>
                </v-tab-item>

                <!-- register -->
                <v-tab href="#tab-2">
                    Register
                <v-icon>person_add</v-icon>
                </v-tab>

                <v-tab-item
                    value="tab-2"
                >
                    <v-form>
                        <v-layout row wrap justify-space-around pt-4>
                             <!-- fullname -->
                            <v-flex xs12>
                                <v-text-field
                                    label="Full name"
                                    outline
                                    v-model="name"
                                    :rules="[rules.required]"

                                ></v-text-field>
                            </v-flex>
                            <!-- gender -->
                            <v-flex xs6>
                                <v-select
                                    v-model="gender"
                                    :items="genders"
                                    label="Gender"
                                    outline
                                ></v-select>
                            </v-flex>
                            <v-flex xs6>
                                 <v-dialog
                                    ref="dialog"
                                    v-model="dialog_dob"
                                    :return-value.sync="dob"
                                    persistent
                                    lazy
                                    full-width
                                    width="290px"
                                >
                                    <v-text-field
                                    slot="activator"
                                    v-model="dob"
                                    label="Date of birth"
                                    readonly
                                    outline
                                    ></v-text-field>
                                    <v-date-picker v-model="dob" scrollable>
                                    <v-spacer></v-spacer>
                                    <v-btn flat color="primary" @click="dialog_dob = false">Cancel</v-btn>
                                    <v-btn flat color="primary" @click="$refs.dialog.save(dob)">OK</v-btn>
                                    </v-date-picker>
                                </v-dialog>
                            </v-flex>
                            <!-- phone -->
                            <v-flex xs12>
                                <v-text-field
                                    v-model="phone"
                                    label="Phone number"
                                    outline
                                    @change="checkPhoneExist()"
                                    :rules="[rules.required,rules.phoneLen,rules.mustNum]"
                                    :error-messages="this.isPhoneExist==1?'phone is exist':''"
                                ></v-text-field>
                            </v-flex>
                            <!-- nric -->
                            <v-flex xs12>
                                <v-text-field
                                    v-model="nric"
                                    label="NRIC"
                                    outline
                                    @change="checkNricExist()"
                                    :rules="[rules.required,rules.mustNum]"
                                    :error-messages="this.isNricExist==1?'nric is exist':''"
                                ></v-text-field>
                            </v-flex>
                            <!-- pass -->
                            <v-flex xs12>
                                <v-text-field
                                    v-model="pass"
                                    label="Password" type="password"
                                    outline
                                    :rules="[rules.required,rules.min]"
                                ></v-text-field>
                            </v-flex>
                            <!-- confirm pass -->
                            <v-flex xs12>
                                <v-text-field
                                    v-model="confirmPass" type="password"
                                    label="Confirm Password"
                                    outline
                                    :rules="[rules.passMatch]"
                                ></v-text-field>
                            </v-flex>
                            <!-- term & conditions -->
                            <v-flex xs12>
                                <v-checkbox v-model="checkbox">
                                    <div slot="label">
                                        <term-conditions ></term-conditions>
                                    </div>
                                </v-checkbox>
                            </v-flex>
                            <!-- register -->
                            <v-flex xs6 sm6 md3>
                                <v-btn   color="primary" block @click="register()"
                                :disabled="!allow_register"
                                >
                                    Register
                                </v-btn>
                            </v-flex>
                        </v-layout>

                           <!-- Loading -->
                        <v-layout
                            v-if="loading">
                            <v-progress-linear
                                :indeterminate="loading"
                                height="5"
                            >              
                            </v-progress-linear>
                            
                        </v-layout>

                        <!-- Shownoti -->
                        <v-layout row wrap v-if="noti != null">
                                    <app-alert
                                        :noti="noti"
                                        @dismissed="onDismissed"
                                        >
                                    </app-alert>
                                </v-layout>


                    </v-form>
                </v-tab-item>
            </v-tabs>
           
        <v-spacer></v-spacer>
        <v-layout class="justify-center xs8" >
          <!--face book-->
          <v-flex xs10 sm5 md5>
            <facebook-login class="button"
              appId="1878947162211144"
              @sdk-loaded="sdkLoaded"
              @login="onLogin"
              @logout="onLogout"
            >
            </facebook-login>
          </v-flex>
          <!--end facebook-->
        </v-layout>
    </v-container>
</template>
<script>
  import facebookLogin from 'facebook-login-vuejs'
  import { mapState, mapActions } from 'vuex'
export default {
    components: {
      facebookLogin
    },
    data () {
        return{
            name: '',
            dob: new Date().toISOString().substr(0, 10),
            genders: ['Male', 'Female'],
            gender: 'null',
            pass: '',
            nric: '',
            phone: '',
            confirmPass: '',
            dialog_dob: false,
            show_pass: false,
            rules: {
                required: value => !!value || 'Required.',
                phoneLen: v => v.length == 10 || 'Phone input is not true',
                mustNum: v => !isNaN(v) || 'Type values only number',
                min: v => v.length >= 8 || 'Min 8 characters',
                passMatch: value => value == this.pass || 'The password does not match',
            },
            checkbox: false,
            allow_register:true,
        }
    },
    computed: {
        ...mapState({
            loading: state => state.share.loading,
            noti: state => state.share.noti,

            //outlet: state => state.customer.outlet,
            
        }),
      isNricExist() {
        return this.$store.state.customer.nric_isexist
      },
      isEmailExist() {
        return this.$store.state.customer.email_isexist
      },
      isPhoneExist() {
        return this.$store.state.customer.phone_isexist
      },
    },
    methods: {
        register (){
            console.log('register methods')
            if (this.gender == 'Male'){
                var userInfo = {
                Name : this.name,
                DoB : this.dob,
                Gender: 1,
                PassWord: this.pass,
                NRIC: this.nric,
                Phone: this.phone,
                ConfirmPassWord: this.confirmPass
                }
            }
            else{
                var userInfo = {
                Name : this.name,
                DoB : this.dob,
                Gender: 0,
                PassWord: this.pass,
                NRIC: this.nric,
                Phone: this.phone,
                ConfirmPassWord: this.confirmPass
                }
            }
            
            console.log(userInfo)
            this.$store.dispatch('customer/register', userInfo)
        },
        login(){

            var userpass = {
                user: this.phone,
                pass: this.pass
            }
            // this.$store.dispatch('customer/logIn', userpass)
            this.$store.dispatch('customer/getCustomerInfoByUserNamePassword', userpass)
        },
        onDismissed () {
            this.$store.dispatch('share/clearNoti')
        },

        sleep(ms) {
        return new Promise(resolve => setTimeout(resolve, ms));
      },
      // face book menthod
        getUserData() {
          console.log('lấy thông tin người dùng')
          var userinf =null
          this.FB.api('/me', 'GET', {fields: 'id,name,email,gender'},
            userInformation => {
              console.warn("get data from facebook", userInformation)
              //var encryptor = require('simple-encryptor')('qwertyuiopasdfghjklzxcvbnm123456789')
              var gender = 0
              if (userInformation.gender == 'male')
                gender = 1
              var passw = userInformation.id
              userinf ={
                Name: userInformation.name,
                Email: userInformation.email,
                Gender: gender,
                DoB: '2018-04-07',
                PassWord: passw,
                Phone: passw.slice(0, 10),
                NRIC:passw.slice(0,5),
                ConfirmPassWord: passw
              }
            }
          )
           this.sleep(3000).then(() => {
             var customer = {
               info: userinf.Email,
               type: 'email'
             }

             this.$store.dispatch('customer/checkExistAccount', customer)
           })
          this.sleep(4500).then(() => {
            console.log(this.isEmailExist, 'IS email EXITS')
            if (this.isEmailExist == 1) {
              var userpass = {
                user: userinf.Email,
                pass: userinf.PassWord
              }
              this.$store.dispatch('customer/getCustomerInfoByUserNamePassword', userpass)
            } else if (this.isEmailExist == 0)
                this.$store.dispatch('customer/register', userinf)
          })

        },
        checkEmailExist() {
          var payload = {
            info: this.email,
            type: 'email'
          }
          this.$store.dispatch('customer/checkExistAccount', payload)
          this.sleep(2000).then(() => {
            console.log(this.isEmailExist, 'IS EXITS')
            if (this.isEmailExist == 1) {
              this.allow_register = false
              console.log(this.allow_register, 'sau khi kiemm tra')
            }
            else  if(this.isNricExist==0)
              this.allow_register = true
          })
        },
        checkPhoneExist() {
          console.log('thực hiện check phone')
          var payload = {
            info: this.phone,
            type: 'phone'
          }
          this.$store.dispatch('customer/checkExistAccount', payload)
          this.sleep(200).then(() => {
            console.log(this.isPhoneExist, 'IS EXITS')
            if (this.isPhoneExist == 1) {
              this.allow_register = false
              console.log(this.allow_register, 'sau khi kiemm tra phone')
            }
            else if(this.isNricExist==0)
              this.allow_register=true
          })
        },
        checkNricExist() {
          console.log('thực hiện check phone')
          var payload = {
            info: this.phone,
            type: 'nric'
          }
          this.$store.dispatch('customer/checkExistAccount', payload)
          this.sleep(200).then(() => {
            console.log(this.isPhoneExist, 'IS EXITS')
            if (this.isNricExist == 1) {
              this.allow_register = false
              console.log(this.allow_register, 'sau khi kiemm tra phone')
            }
            else  if(this.isPhoneExist==0)
              this.allow_register = true
          })
        },
        sdkLoaded(payload) {
          this.isConnected = payload.isConnected
          this.FB = payload.FB
          console.log('load sdk rồi nha')
        },
        onLogin() {
          this.isConnected = true
          this.getUserData()
          console.log('đăng nhập bằng facebook')

        },
        onLogout() {
          this.isConnected = false;
          console.log('log out thành công')
        },
    },
}
</script>
