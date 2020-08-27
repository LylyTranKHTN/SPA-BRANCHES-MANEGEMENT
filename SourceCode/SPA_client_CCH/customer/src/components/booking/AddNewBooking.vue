<template>
    <v-container grid-list-lg fluid>
        <v-layout justify-space-around>
            <h2> Book New Appointment</h2>
        </v-layout>
        <v-layout row wrap>
             <v-flex>
                <review-booking
                    class="right" :currentOutlet="currentOutlet" :date="date">
                </review-booking>
            </v-flex>
        </v-layout>
         <!-- choose outlet + continue -->
        <v-layout row>
            <v-flex xs6>
                <v-select
                    size="5"
                    label="Choose Oulet"
                    outline
                    :items="outlets"
                    v-model="currentOutlet"
                    item-text="name"
                    item-value="id"
                    hide-details
                    @mousedown="loadOutlet"
                    @change="chooseOutlet(currentOutlet.id, date)"
                >
                </v-select>
            </v-flex>
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
                    <v-date-picker 
                        v-model="date" 
                        scrollable
                        :min='curDate'
                    >
                        <v-spacer></v-spacer>
                        <v-btn flat color="primary" @click="dialog_date = false">Cancel</v-btn>
                        <v-btn flat color="primary" @click="$refs.dialog_date.save(date); chooseOutlet(currentOutlet.id, date);">OK</v-btn>
                    </v-date-picker>
                </v-dialog>
            </v-flex>
        </v-layout>

        <!-- service list -->
        <v-layout column>
            <v-flex v-for="service in bookings" :key="service.index">
                <service
                    :service="service"
                    :currentOutlet="currentOutlet"
                    :currentDate="date"
                ></service>
            </v-flex>
        </v-layout>
    </v-container>
</template>
<script>

import { mapState, mapActions } from 'vuex'
import API from '@/api';
export default {
    data() {
        return{
            outlet: '',
            date: new Date().toISOString().substr(0, 10),
            dialog_date: false,
            curDate: new Date().toISOString().substr(0, 10),
        }
    },
    created(){
        this.loadOutlet()
        this.chooseOutlet(this.currentOutlet.id, this.date)
    },
    // mounted(){
    //     this.chooseOutlet(this.currentOutlet.id)
    // },
    computed: {
        ...mapState({
            outlets: state => state.outlet.outlets,
            bookings: state => state.booking.bookingAvailables,
            //servicesChoose: state => state.service.servicesChoose
        }),
        currentOutlet: {
            get() {
                return this.$store.state.outlet.outlet
            },
            set(newID) {
                this.$store.dispatch('outlet/changeIDCurrentOutLet', newID)
            }
        },
    },
    methods: {
        // ...mapActions('outlet', ['loadOutlet'])
        loadOutlet (){
            this.$store.dispatch('outlet/getAllOutlet')
        },
        chooseOutlet(outletID, date){
            console.log('choose outlet',this.currentOutlet.name)
            this.$store.dispatch('booking/resetAllBookings')
            var outlet_date_pagenum_pagesize = {
                outletId: outletID,
                date: date,
                pagesize: 4,
                pageNumber: 1
            }
            this.$store.dispatch('booking/getBookingAvailable', outlet_date_pagenum_pagesize)
            this.$store.dispatch('service/removeAllServiceChoose')
            this.isChange=true
        }
    },

}
</script>
