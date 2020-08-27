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
            <v-toolbar color="white">
            <v-btn icon  @click="dialog = false">
                <v-icon>close</v-icon>
            </v-btn>
            <v-spacer></v-spacer>
            <v-toolbar-items>
                <v-btn 
                    flat color="primary" @click="createBooking()"
                    :loading="loading"
                >Submit</v-btn>
            </v-toolbar-items>
            </v-toolbar>
            <v-container grid-list-lg fluid>
                <v-layout row wrap v-if="noti != null">
                    <app-alert
                        :noti="noti"
                        @dismissed="onDismissed"
                        >
                    </app-alert>
                </v-layout>
                <v-layout justify-space-around>
                <h2> Review Booking Before Submit</h2>
                </v-layout>
                <!-- choose outlet + continue -->
                <v-layout row justify-center>
                    <v-flex xs6 md4 sm4>
                        <v-text-field
                            readonly
                            label="Choose Oulet"
                            outline
                            v-model="outlet.name"
                            
                        ></v-text-field>
                    </v-flex>
                    <v-flex xs6 md4 sm4>
                        <v-text-field
                            readonly
                            label="Session"
                            outline
                            v-model="date"
                            hide-details
                        ></v-text-field>
                    </v-flex>
                </v-layout>

                <!-- service list -->
                <v-layout column>
                    <v-flex v-for="service in servicesChoose" :key="service.index">
                        <service-review 
                            :service="service"
                            :currentOutlet="outlet"
                        >
                        </service-review>
                    </v-flex>
                </v-layout>

                <!-- Note -->
                <v-layout row wrap justify-center>
                    <v-flex xs12 md8 sm8>
                        <v-textarea
                            outline
                            label="Extra note for your booking"
                            v-model="note"
                        ></v-textarea>
                    </v-flex>
                </v-layout>

            </v-container>
        </v-card>
        </v-dialog>
    </v-layout>
</template>
<script>
import { mapState, mapActions } from 'vuex'

import axios from 'axios'
export const HTTP_API = axios.create({
// baseURL điền cái localhost:port
baseURL: 'http://localhost:5828/',
headers: {"Access-Control-Allow-Origin": "*"}
})

export default {
    props:{
        currentOutlet: {},
        date: String
    },
    data () {
        return {
            dialog: false,
            note: '',
            outlet: {}
        }
    },
    created(){
        this.loadOutletByID()
    },
    computed: {
        ...mapState({
            servicesChoose: state => state.service.servicesChoose,
            customerID: state => state.customer.userInfo.id,
            loading: state => state.share.loading,
            noti: state => state.share.noti,
        }),
        // noti: {
        //     get() {
        //         return state => state.share.noti
        //     },
        //     set() {
        //         this.$store.dispatch('share/clearNoti')
        //     }
        // },
    },
    methods: {
        loadOutletByID(){
        // get outlet by id
            HTTP_API({
                method: 'get',
                url: `outlet/${this.currentOutlet.id}`,
                headers:{
                    // Authorization:
                },
            })
            .then((result) => {
                console.log('this is current outlet',this.currentOutlet)
                console.log('action getoutlet')
                console.log(result)
                var outlet = result.data.result.data
                this.outlet = outlet
            }).catch((err) => {
                console.log(err)
            })  
        },
        createBooking(){
            
            var n = this.servicesChoose.length
            var servicesChooseFilter=[]
            for (var i = 0; i<n; i++){
                var serviceChoose={
                    serviceID:this.servicesChoose[i].serviceID,
                    therapistID:this.servicesChoose[i].therapistID,
                    timeSlotID:this.servicesChoose[i].timeSlot.id,
                    bookingDate:this.servicesChoose[i].bookingDate,
                }
                console.log('this is body',serviceChoose)
                servicesChooseFilter.push(serviceChoose)
            }
        
            var payload = {
                customerID : this.customerID,
                outletID: this.currentOutlet.id,
                note: this.note,
                createBookingDtos: this.servicesChoose
            }
            this.$store.dispatch('booking/createBooking', payload)
            console.log('method booking')
        },
        onDismissed () {
            this.$store.dispatch('share/clearNoti')
        }

    }
}
</script>
