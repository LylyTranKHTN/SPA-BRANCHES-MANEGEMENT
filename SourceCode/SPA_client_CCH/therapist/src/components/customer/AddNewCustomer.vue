<template>
  <v-container grid-list-xs fluid class="scroll-y">
    <v-layout column wrap justify-center>
      <!-- Main Title -->
      <v-flex  class="text-xs-center">
        <h1>Add New Customer</h1>
      </v-flex>
      
      <!-- Profile -->
      <v-flex xs12 sm8 md8 pt-3>
        <v-card>
          <v-layout column wrap>     
            <!-- Title Personal Detail -->
            <v-flex offset-xs1 pt-2>
                <h3>Personal details</h3>
            </v-flex>

            <!-- Fullname -->
            <v-flex xs12>
              <v-layout row justify-center align-center>
                <v-flex xs4>
                  <v-subheader>Full name</v-subheader>
                </v-flex>
                <v-flex xs6>
                  <v-text-field
                    readonly
                    v-model="user.fullName"
                  ></v-text-field>
                </v-flex>
              </v-layout>
            </v-flex>

            <!-- Gender  -->
            <v-flex xs12>
              <v-layout row justify-center align-center>
                <v-flex xs4>
                  <v-subheader>Gender</v-subheader>
                </v-flex>
                <v-flex xs6>
                  <v-text-field
                    readonly
                    v-model="user.gender"
                  ></v-text-field>
                </v-flex>
              </v-layout>
            </v-flex>

              <!-- BOD  -->
            <v-flex xs12>
              <v-layout row justify-center align-center>
                <v-flex xs4>
                  <v-subheader>BOD</v-subheader>
                </v-flex>
                <v-flex xs6>
                  <v-text-field
                    readonly
                    v-model="user.dob"
                  ></v-text-field>
                </v-flex>
              </v-layout>
            </v-flex>

            <!-- Age -->
            <v-flex xs12>
              <v-layout row justify-center align-center>
                <v-flex xs4>
                  <v-subheader>Age</v-subheader>
                </v-flex>
                <v-flex xs6>
                  <v-text-field
                    readonly
                    v-model="user.age"
                  ></v-text-field>
                </v-flex>
              </v-layout>  
            </v-flex>
            
            <!-- Profession -->
            <v-flex xs12>
              <v-layout row justify-center align-center>
                <v-flex xs4>
                  <v-subheader>Profession</v-subheader>
                </v-flex>
                <v-flex xs6>
                  <v-text-field
                    readonly
                    v-model="user.profession"
                  ></v-text-field>
                </v-flex>
              </v-layout>  
            </v-flex>

            <!-- Password -->
            <v-flex xs12>
              <v-layout row justify-center align-center>
              <v-flex xs4>
                <v-subheader>Password</v-subheader>
              </v-flex>
              <v-flex xs6>
                <v-text-field
                  readonly
                  type="password"
                  v-model="user.password"
                ></v-text-field>
              </v-flex>
            </v-layout>  
            </v-flex>
          </v-layout>
        </v-card>
      </v-flex>

      <!-- Contact -->
      <v-flex xs12 sm8 md8 pt-3>
        <v-card>
          <v-layout column>
            <!-- Title -->
            <v-flex xs12 pt-3>
              <v-layout row wrap>
                <v-flex offset-xs1>
                  <h3>Contact details</h3>
                </v-flex>
              </v-layout>
            </v-flex>
            
            <!-- Mobile -->
            <v-flex xs12>
              <v-layout row justify-center align-center>
                <v-flex xs4>
                  <v-subheader>Mobile</v-subheader>
                </v-flex>
                <v-flex xs6>
                  <v-text-field
                    readonly
                    v-model="user.mobile"
                  ></v-text-field>
                </v-flex>
              </v-layout>
            </v-flex>
            

            <!-- Email -->
            <v-flex xs12>
              <v-layout row justify-center align-center>
                <v-flex xs4>
                  <v-subheader>Email</v-subheader>
                </v-flex>
                <v-flex xs6>
                  <v-text-field
                    readonly
                    v-model="user.email"
                  ></v-text-field>
                </v-flex>
              </v-layout>
            </v-flex>
            

            <!-- NRIC -->
            <v-flex xs12>
              <v-layout row justify-center align-center>
                <v-flex xs4>
                  <v-subheader>NRIC</v-subheader>
                </v-flex>
                <v-flex xs6>
                  <v-text-field
                    readonly
                    v-model="user.nric"
                  ></v-text-field>
                </v-flex>
              </v-layout>
            </v-flex>

          </v-layout>
        </v-card>
      </v-flex>  
            
      <!-- Body measurement -->
      <v-flex xs12 sm8 md8 pt-3>
        <v-card>
          <v-layout column wrap>
            <!-- Title -->
            <v-flex xs12>
              <v-layout row wrap>
                <v-flex offset-xs1 pt-2>
                  <h3>Body Measurement</h3>
                </v-flex>
              </v-layout>
            </v-flex>
            
            <!-- Basic Measurement -->
            <v-flex xs12>
              <v-layout row wrap justify-center align-center pt-2>
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
            </v-flex>
            
            <!-- Body mass -->
            <v-flex xs12>
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
            </v-flex>
            
            <!-- Fat content -->
            <v-flex xs12>
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
            </v-flex>
            
            <!-- Muscle content -->
            <v-flex xs12>
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
            </v-flex>
            
            
            <!-- Stomach fat -->
            <v-flex xs1>
              <v-layout row justify-center align-center>
                <v-flex xs4>
                  <v-subheader>Stomach fat</v-subheader>
                </v-flex>
                <v-flex xs6>
                  <v-text-field
                    v-model="bodyMeasurement.stomachFat"
                    readonly
                  ></v-text-field>
                </v-flex>
              </v-layout>
            </v-flex>
          </v-layout>
        </v-card>
      </v-flex>

    </v-layout>
  </v-container>
</template>

<script>
  export default {
    data () {
      return {
        user: {
          image: '../../static/outlet/outlet.jpg',
          fullName: 'Con cò hương',
          gender: 'Chưa xác định',
          dob: '11/01/2019',
          age: '1',
          profession: 'Student',
          password: '12345678910',
          mobile: '+8412345678',
          email: 'cch@gitlab.com',
          nric: '123456789'
        },
        bodyMeasurement: {
          height: '150',
          weight: '30',
          bmi: '10',
          bodyMass: '45',
          fatContent: '22',
          muscleContent: '33',
          stomachFat: '332'
        }
      }
    }
  }
</script>
