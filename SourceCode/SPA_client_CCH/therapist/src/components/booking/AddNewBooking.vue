<template>
    <v-container grid-list-lg fluid>
        <v-layout justify-space-around>
            <h2> Book New Appointment</h2>
        </v-layout>
        <v-layout row wrap>
             <v-flex>
                 <!-- Continue Button -->
                <review-booking
                    class="right" :outlet="outlets.name" :date="date">
                </review-booking>
            </v-flex>
        </v-layout>
         <!-- Choose Outlet -->
        <v-layout row>
            <v-flex xs6>
                <v-select
                    size="5"
                    label="Choose Oulet"
                    outline
                    :items="outlets"
                    item-text="name"
                    item-value="id"
                    v-model="outlet"
                    return-object
                    hide-details
                    @mousedown="loadOutlets()"
                    @change="loadServiceByOutletID(outlet.id)"
                >
                </v-select>
            </v-flex>
            <!-- Choose Session Date -->
            <v-flex xs6>
                <v-dialog
                    ref="dialog_date"
                    v-model="dialog_date"
                    :return-value.sync="date"
                    persistent
                    lazy
                    full-width
                    width="290px"
                >
                    <v-text-field
                    slot="activator"
                    v-model="date"
                    label="Session Date"
                    readonly
                    outline
                    hide-details
                    ></v-text-field>
                    <v-date-picker v-model="date" scrollable>
                    <v-spacer></v-spacer>
                    <v-btn flat color="primary" @click="dialog_date = false">Cancel</v-btn>
                    <v-btn flat color="primary" @click="$refs.dialog_date.save(date)">OK</v-btn>
                    </v-date-picker>
                </v-dialog>
            </v-flex>

        </v-layout>

        <!-- Service List -->
        <v-layout column>
            <v-flex v-for="service in services" :key="service.index">
                <service 
                    :service="service"
                    :outlet="outlet">
                </service>
            </v-flex>
        </v-layout>
    </v-container>
</template>

<script>

import { mapState, mapActions } from 'vuex'
//import API from '@/api'
export default {
    // store data of this layout
    data() {
        return{
            outlet:null,
            date: new Date().toISOString().substr(0, 10),
            dialog_date: false,
           
        }
    },

    // contain functions that start from beginning
    created(){
        //this.loadOutlets()
        //this.loadServiceByOutletID(outlet.id)
        //this.checkpoint()
    },

    // get state data
    computed: {
        ...mapState({
            outlets: state => state.outlet.outlets,
            services:state => state.service.services,
        }),
    },

    // contain function define (for calling API)
    methods: {
        // ...mapActions('outlet', ['loadOutlet'])
        loadOutlets (){
            console.log('loadOutlets (UI)')
            this.$store.dispatch('outlet/getAllOutlet')
            console.log('loadOutlets success')
        },
        
        chooseOutlet(){
            console.log('choose outlet')
            console.log(this.outlet)
        },

        checkpoint(){
            console.log('OKOKOKOK')
            //this.$store.dispatch('outlet/getAllOutlet')
            //this.$store.dispatch('service/getServicesByOutletID','1')
            console.log('this is checkpoint')
            console.log(outlets)
            console.log('end of checkpoint')
        },
        loadServiceByOutletID(outletID){
            console.log('loadServiceByOutletID (UI)')
            this.$store.dispatch('service/getServicesByOutletID',outletID)
            console.log('loadServiceByOutletID success')
        }
    },

}
</script>
