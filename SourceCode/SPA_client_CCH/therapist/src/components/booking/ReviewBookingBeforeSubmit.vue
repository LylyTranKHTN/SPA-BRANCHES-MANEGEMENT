<template>
  <v-layout row justify-center>
    <v-dialog v-model="dialog" fullscreen hide-overlay transition="dialog-bottom-transition">
      <v-btn
        slot="activator" color="primary" dark
        flat
        class="right"
        > Continue
           <v-icon right>arrow_forward</v-icon>
      </v-btn>
      <v-card>
        <!-- Tool Bar -->
        <v-toolbar color="white">
          <v-btn icon  @click="dialog = false">
            <v-icon>close</v-icon>
          </v-btn>
          <v-spacer></v-spacer>
          <v-toolbar-items>
            <v-btn  flat color="primary" @click="dialog = false">Submit</v-btn>
          </v-toolbar-items>
        </v-toolbar>
        <!-- Content -->
        <v-container grid-list-lg fluid>
            <v-layout justify-space-around>
            <h2> Review Booking Before Submit</h2>
            </v-layout>
            <!-- Customer Profile Abtract -->
            <v-layout row wrap>
                <v-flex xs2 offset-xs3>
                    <v-avatar
                    size="50px"
                    color="red"
                    >
        
                    </v-avatar>
                </v-flex>
                <!-- Infor -->
                <v-flex xs5>
                    <v-layout column wrap>
                        <span>{{customer1.name}}</span>
                        <span>{{customer1.email}}</span>
                        <span>{{customer1.phone}}</span>
                    </v-layout>
                </v-flex>
            </v-layout>
            <v-divider></v-divider>
            <!-- Choose Outlet -->
            <v-layout row>
                <v-flex xs6>
                    <v-text-field
                         readonly
                        label="Choose Oulet"
                        outline
                        v-model="outlet"
                        hide-details
                    ></v-text-field>
                </v-flex>
                <v-flex xs6>
                    <v-text-field
                         readonly
                        label="Choose Date"
                        outline
                        v-model="date"
                        hide-details
                    ></v-text-field>
                </v-flex>
            </v-layout>
            <!-- Service List -->
            <!-- <v-layout column>
                <v-flex v-for="i in servicesChoose" :key="i">
                    <service :disabled="true" :service="i"></service>
                </v-flex>
            </v-layout> -->
        </v-container>
      </v-card>
    </v-dialog>
  </v-layout>
</template>
<script>
import { mapState, mapActions } from 'vuex'
export default {
    props:{
        outlet: String,
        date: String
    },
    data () {
        return {
        dialog: false,
        customer1: {
            name: "Nguyen Thanh Huy",
            email:"abc@gmail.com",
            phone:"070707070"
        }
        }
    },
    computed: {
        ...mapState({
            //servicesChoose: state => state.service.servicesChoose
        })
    }
}
</script>
