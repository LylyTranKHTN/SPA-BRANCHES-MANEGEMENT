<template>
  <v-container grid-list-xs fluid class="scroll-y">
    <v-layout row wrap justify-center>
      <!-- Profile -->
      <v-flex xs10 sm8 md8>
        <v-card>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn
              v-if="edit"
              icon
              flat
              @click="edit = false"
            >
              <v-icon>edit</v-icon>
            </v-btn>
            <v-btn
              v-if="!edit"
              color="success"
              @click="saveUserInfo()"
            >
              Save
            </v-btn>
            
          </v-card-actions>
          <v-layout wrap justify-center>
            <v-flex  class="text-xs-center">
              <v-avatar
                  size="80"
                  color="red"
              >
                <img :src="user.avatar" alt="alt">
              </v-avatar>
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
                :readonly="edit"
                v-model="user.name"
              ></v-text-field>
            </v-flex>
          </v-layout>

          <!-- Gender  -->
           <v-layout row justify-center align-center>
            <v-flex xs4>
              <v-subheader>Gender</v-subheader>
            </v-flex>
            <v-flex xs6>
              <v-select
                :readonly="edit"
                v-model="user.gender"
                :items="genders"
                item-text="name"
                item-value="gender"
              ></v-select>
            </v-flex>
          </v-layout>

            <!-- BOD  -->
          <v-layout row justify-center align-center>
            <v-flex xs4>
              <v-subheader>BOD</v-subheader>
            </v-flex>
            <v-flex xs6>
              <v-menu
                :disabled="edit"
                ref="menu_dob"
                :close-on-content-click="false"
                v-model="menu_dob"
                :nudge-right="20"
                lazy
                transition="scale-transition" origin="center center"
                offset-y
                full-width
                max-width="290px"
                >
                <v-text-field
                  slot="activator"
                  v-model="date"
                  label="Birthday date"
                  readonly
                ></v-text-field>
                <v-date-picker
                  ref="picker"
                  v-model="date"
                  :max="new Date().toISOString().substr(0, 10)"
                  min="1950-01-01"
                  @change="save"
                ></v-date-picker>
              </v-menu>
              
            </v-flex>
          </v-layout>

          <!-- age -->
          <!-- <v-layout row justify-center align-center>
            <v-flex xs4>
              <v-subheader>Age</v-subheader>
            </v-flex>
            <v-flex xs6>
              <v-text-field
                readonly
                v-model="user.age"
              ></v-text-field>
            </v-flex>
          </v-layout> -->

          <!-- Profession -->
          <v-layout row justify-center align-center>
            <v-flex xs4>
              <v-subheader>Profession</v-subheader>
            </v-flex>
            <v-flex xs6>
              <v-select
                :readonly="edit"
                v-model="user.profession"
                :items="profressional"
                item-text="name"
                item-value="id"
              ></v-select>
            </v-flex>
          </v-layout>

          <!-- password -->
          <v-layout row justify-center align-center>
            <v-flex xs4>
              <v-subheader>Password</v-subheader>
            </v-flex>
            <v-flex xs6>
              <v-text-field
                :readonly="edit"
                type="password"
                value="12345678910"
              ></v-text-field>
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
                :readonly="edit"
                v-model="user.phone"
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
                :readonly="edit"
                v-model="user.email"
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
                :readonly="edit"
                v-model="user.nric"
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
                <h3>{{bodyMeasurement.height}}</h3>
              </div>
            </v-flex>
            <v-flex xs3 class="text-xs-center">
              <div>
                <span>Weight</span>
                <h3>{{bodyMeasurement.weight}}</h3>
              </div>
            </v-flex>
            <v-flex xs3 class="text-xs-center">
              <div>
                <span>BMI</span>
                <h3>{{bodyMeasurement.bmi}}</h3>
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
                v-model="bodyMeasurement.bodyMass"
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
                v-model="bodyMeasurement.fatContent"
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
                v-model="bodyMeasurement.muscleContent"
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
import { mapState, mapActions } from 'vuex'
  export default {
    data () {
      return {
        edit: true,
        menu_dob: false,
        genders:[
          {name: 'Female', gender: 0},
          {name: 'Male', gender: 1}
        ],
        date: '',
        profressional:[
        {name: 'HomeCarry', id: 1},
        {name: 'Student', id: 2},
        {name: 'Technical', id: 3},
        {name: 'Economic', id: 4},
        {name: 'Officer', id: 5}]
      }
    },
    created(){
      this.searchMeasurement()
      this.date = this.formatDate(this.user.dob)
    },
    watch: {
      menu_dob (val) {
        val && this.$nextTick(() => (this.$refs.picker.activePicker = 'YEAR'))
      }
    },
    computed: {
        ...mapState({
            bodyMeasurement: state => state.customer.measurement,
            user: state => state.customer.userInfo
        }),
        gender(){
          return {id: this.user.gender}
        }
    },
    methods:{
      searchMeasurement(){
        this.$store.dispatch('customer/getCustomerMeasurement', this.user.id)
      },
      formatDate(date){
         if (!date) return null
          const [day, time] = date.split('T')
          return day
      },
      save (date) {
        this.$refs.menu_dob.save(date)
        this.user.dob=date
      },
      saveUserInfo(){
        var newInfo = {
          Id: this.user.id,
          Name: this.user.name,
          DoB: this.user.dob,
          Gender: this.user.gender,
          Email: this.user.email,
          //PassWord: this.user.PassWord,
          NRIC : this.user.nric,
          Phone: this.user.phone,
          Profession: this.user.profession
        }
        console.log(newInfo)
        this.$store.dispatch('customer/editCustomer', newInfo)
        this.edit = true
      }
    }
  }
</script>
