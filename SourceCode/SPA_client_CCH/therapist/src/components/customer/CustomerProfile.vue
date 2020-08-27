<template>
  <v-container grid-list-xs fluid class="scroll-y">
    <v-layout row wrap justify-center>
      <!-- Profile -->
      <v-flex xs10 sm8 md8>
        <v-card>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn
              color="primary"
              @click="update()"
            >
              Update
            </v-btn>
          </v-card-actions>
          <v-layout wrap justify-center column>
            <v-flex class="text-xs-center">
              <v-avatar
                size="80"
                color="red"
              >
                <img :src="customer.image" alt="alt">
              </v-avatar>
            </v-flex>
            <v-flex class="text-xs-center">
              <v-icon
                :color="customer.blackList>=0 ? 'primary':'black'"
              >
                info
              </v-icon>
              <v-icon
                :color="customer.blackList>1 ? 'primary':'black'"
              >
                info
              </v-icon>
              <v-icon
                :color="customer.blackList>2 ? 'primary':'black'">
                info
              </v-icon>
            </v-flex>
          </v-layout>

          <v-layout row wrap>
            <v-flex offset-xs1>
              <h3>Personal details</h3>
            </v-flex>
          </v-layout>

          <!-- Fullname -->
          <v-layout row justify-center align-center>
            <v-flex xs4>
              <v-subheader>Full name</v-subheader>
            </v-flex>
            <v-flex xs6>
              <v-text-field
                readonly
                v-model="customer.name"
              ></v-text-field>
            </v-flex>
          </v-layout>

          <!-- Gender  -->
          <v-layout row justify-center align-center>
            <v-flex xs4>
              <v-subheader>Gender</v-subheader>
            </v-flex>
            <v-flex xs6>
              <v-text-field
                readonly
                v-model="customer.gender"
              ></v-text-field>
            </v-flex>
          </v-layout>

          <!-- BOD  -->
          <v-layout row justify-center align-center>
            <v-flex xs4>
              <v-subheader>BOD</v-subheader>
            </v-flex>
            <v-flex xs6>
              <v-text-field
                readonly
                v-model="customer.doB"
              ></v-text-field>
            </v-flex>
          </v-layout>

          <!-- age -->
          <v-layout row justify-center align-center>
            <v-flex xs4>
              <v-subheader>Age</v-subheader>
            </v-flex>
            <v-flex xs6>
              <v-text-field
                readonly
                v-model="customer.age"
              ></v-text-field>
            </v-flex>
          </v-layout>

          <!-- Profession -->
          <v-layout row justify-center align-center>
            <v-flex xs4>
              <v-subheader>Profession</v-subheader>
            </v-flex>
            <v-flex xs6>
              <v-text-field
                readonly
                v-model="customer.profession"
              ></v-text-field>
            </v-flex>
          </v-layout>

          <!-- password -->
          <v-layout row justify-center align-center>
            <v-flex xs4>
              <v-subheader>Password</v-subheader>
            </v-flex>
            <v-flex xs6>
              <v-layout row>
                <v-text-field
                  :append-icon="show1 ? 'visibility_off' : 'visibility'"
                  :type="show1 ? 'text' : 'password'"
                  label="edit"
                  :rules="[rules.required,rules.min]"
                  v-model="password_hask"

                  @click:append="show1 = !show1"
                  :disabled="edit"
                ></v-text-field>
                <v-flex pl-4 xs2>
                  <v-btn fab dark small color="cyan" v-on:click="edit=!edit,isUpdate=true">
                    <v-icon dark>edit</v-icon>
                  </v-btn>
                </v-flex>
              </v-layout>
            </v-flex>
          </v-layout>

          <!-- Contact details -->
          <v-layout row wrap>
            <v-flex offset-xs1>
              <h3>Contact details</h3>
            </v-flex>
          </v-layout>

          <!-- Mobile -->
          <v-layout row justify-center align-center>
            <v-flex xs4>
              <v-subheader>Mobile</v-subheader>
            </v-flex>
            <v-flex xs6>
              <v-text-field
                readonly
                v-model="customer.phone"
              ></v-text-field>
            </v-flex>
          </v-layout>

          <!-- Email -->
          <v-layout row justify-center align-center>
            <v-flex xs4>
              <v-subheader>Email</v-subheader>
            </v-flex>
            <v-flex xs6>
              <v-text-field
                readonly
                v-model="customer.email"
              ></v-text-field>
            </v-flex>
          </v-layout>

          <!-- NRIC -->
          <v-layout row justify-center align-center>
            <v-flex xs4>
              <v-subheader>NRIC</v-subheader>
            </v-flex>
            <v-flex xs6>
              <v-text-field
                readonly
                v-model="customer.nric"
              ></v-text-field>
            </v-flex>
          </v-layout>
        </v-card>
      </v-flex>

      <!-- Body measurement -->
      <v-flex xs10 sm8 md8 pt-2>
        <v-card>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn
              class="primary"
              flat

            >
              View history
            </v-btn>
          </v-card-actions>

          <v-layout row wrap>
            <v-flex offset-xs1>
              <h3>Body Measurement</h3>
            </v-flex>
          </v-layout>

          <v-layout row wrap justify-center align-center>
            <v-flex xs3 class="text-xs-center">
              <div>
                <span>Height</span>
                <h3>{{bodymeasurement.height}}</h3>
              </div>
            </v-flex>
            <v-flex xs3 class="text-xs-center">
              <div>
                <span>Weight</span>
                <h3>{{bodymeasurement.weight}}</h3>
              </div>
            </v-flex>
            <v-flex xs3 class="text-xs-center">
              <div>
                <span>BMI</span>
                <h3>{{bodymeasurement.bmi}}</h3>
              </div>
            </v-flex>
          </v-layout>

          <!-- Body mass -->
          <v-layout row justify-center align-center>
            <v-flex xs4>
              <v-subheader>Body mass</v-subheader>
            </v-flex>
            <v-flex xs6>
              <v-text-field
                v-model="bodymeasurement.bodyMass"
                readonly
              ></v-text-field>
            </v-flex>
          </v-layout>

          <!-- Fat content -->
          <v-layout row justify-center align-center>
            <v-flex xs4>
              <v-subheader>Fat content</v-subheader>
            </v-flex>
            <v-flex xs6>
              <v-text-field
                v-model="bodymeasurement.fatContent"
                readonly
              ></v-text-field>
            </v-flex>
          </v-layout>

          <!-- Muscle content -->
          <v-layout row justify-center align-center>
            <v-flex xs4>
              <v-subheader>Muscle content</v-subheader>
            </v-flex>
            <v-flex xs6>
              <v-text-field
                v-model="bodymeasurement.muscleContent"
                readonly
              ></v-text-field>
            </v-flex>
          </v-layout>

          <!-- Stomach fat -->
          <v-layout row justify-center align-center>
            <v-flex xs4>
              <v-subheader>Stomach fat</v-subheader>
            </v-flex>
            <v-flex xs6>
              <v-text-field
                v-model="bodymeasurement.muscleContent"
                readonly
              ></v-text-field>
            </v-flex>
          </v-layout>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
  import {mapState} from 'vuex'

  export default {
    data() {
      return {
        show1: false,
        edit: true,
        color1: 'black',
        color2: 'primary',
        password_hask: '',
        rules: {
          required: value => !!value || 'Required.',
          min: v => v.length >= 8 || 'Min 8 characters',
        },
        isUpdate:false
      }
    },
    computed: {
      ...mapState({
        customer: state => state.customer.customer,
        bodymeasurement: state => state.customer.bodymeasurement,
      }),

    },
    created:function(){
      this.loadcustomer(this.idcustomer)
    },

    props: {
      idcustomer: {
        type:Number,
        required: false,
        default:5
  }
    },
    methods: {

      update() {

        var customer_profile = this.customer
        customer_profile.passWord = this.password_hask
        if (this.isUpdate==false) {
          alert('not thing to update')
          return null
        }
        this.$store.dispatch('customer/updatePassword',customer_profile)
        console.log(customer_profile)
        console.log(this.customer.passWord, " pass word moi ne")
        this.isUpdate=false
      },
      viewmore(customerid) {

      },
      loadcustomer(customerid) {
        console.log('load customer')
        this.$store.dispatch('customer/get_info_customer_by_id', customerid)
        this.$store.dispatch('customer/get_bodymeasurement_customer_by_id', customerid)


      },
    }
  }
</script>
