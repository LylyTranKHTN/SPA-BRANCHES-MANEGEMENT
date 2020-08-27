<template>
    <v-container px-0>
        <v-card>
            <v-img height="100"
                src="https://img.grouponcdn.com/iam/NG2PuHCH332Ax1sL19w4GDDyvyE/NG-1500x900/v1/c700x420.jpg"
                class="red white--text"
            >
                <v-container fill-height fluid>
                    <v-layout fill-height>
                        <v-flex xs12 align-end flexbox>
                            <span class="headline" >{{service.serviceName}}</span>
                        </v-flex>
                        <v-btn
                            dark
                            icon
                            color="success"
                            v-if="choose"
                            @click="chooseService()">
                            <v-icon>add</v-icon>
                        </v-btn>
                        <v-btn
                            dark
                            icon
                            color="error"
                            v-if="!choose"
                            @click="removeService()">
                            <v-icon>remove</v-icon>
                        </v-btn>
                    </v-layout>
                </v-container>
            </v-img>
            <v-card-title primary-title>
                <v-layout row wrap>
                    <v-flex xs2>
                        <v-avatar size="60" >
                            <img
                                src="this.service.therapist.therapistAvatar"
                            >
                        </v-avatar>
                    </v-flex>
                    <v-flex xs5>
                        <v-select
                            label="Choose Therapist"
                            :items="therapists"
                            v-model="therapist"
                            item-text="therapistName"
                            item-value="therapistID"
                            hide-details
                            @mousedown="loadTherapist(currentOutlet.id, service.serviceID, timeslot.id, currentDate)"
                            @change="loadTimeSlot(currentOutlet.id, service.serviceID, therapist, currentDate)"
                        >
                        </v-select>
                    </v-flex>
                    <v-flex xs5>
                        <v-select
                            label="Choose Timeslot"
                            :items="timeslots"
                            v-model="timeslot"
                            item-value="id"
                            return-object
                            hide-details
                            @mousedown="loadTimeSlot(currentOutlet.id, service.serviceID, therapist, currentDate)"
                            @change="loadTherapist(currentOutlet.id, service.serviceID, timeslot.id, currentDate)" 
                        >
                            <template slot="selection" slot-scope="data">
                                {{ data.item.from }} - {{ data.item.to }}
                            </template>
                            <template slot="item" slot-scope="data">
                                {{ data.item.from }} - {{ data.item.to }}
                            </template>
                        </v-select>
                    </v-flex>
                </v-layout>
            </v-card-title>
        </v-card>
    </v-container>
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
        service: {},
        currentOutlet: {},
        currentDate : String,
    },
    data() {
        return{
            therapist : this.service.therapist.therapistID,
            timeslot : {},
            therapists: [],
            timeslots: [],
            choose: true
        }
    },
    created(){
        this.therapists[0] = this.service.therapist
        this.timeslots[0] = this.service.timeSlot
        this.timeslot=this.service.timeSlot
    },
    computed: {
        ...mapState({
            // timeslots: state => state.booking.timeslots,
            // therapists: state => state.booking.therapists,
        }),
    },
    methods: {
        chooseService(){
            this.choose = ! this.choose
            console.log(this.service)
            var service = {
                serviceID: this.service.serviceID,
                serviceName:this.service.serviceName,
                therapistID: this.therapist,	
                timeSlot: this.timeslot,
                bookingDate: this.formatDate(this.currentDate),
            }
            this.$store.dispatch('service/chooseService', service)
        },
        removeService(){
            this.choose = ! this.choose
            this.$store.dispatch('service/removeService', this.service)
        },
        loadTherapist(outletId, serviceId, timeslotId, date){
            var payload = {
                outletId: outletId,
                serviceId: serviceId,
                timeslot: timeslotId,
                date: date
            }
            // this.$store.dispatch('booking/getTherapistBookingAvailable', outlet_service_timeslot_date)
            HTTP_API({
            method: 'get',
            url: `booking/booking-available/therapist/${payload.outletId}/${payload.serviceId}/${payload.timeslot}/${payload.date}`,
            header: {
                // 
            }
            }).then((result) => {
            console.log('get therapist of booking available')
            console.log(result)
            if (result.data.hasOwnProperty('error')){
                alert(result.data.error.errorMessage)
                alert(result.data.error.validation.errorMessage)
            }
            var therapists = result.data.result.data.result
            this.therapists = therapists
            }).catch((err) => {
            console.log(err)
            })
        },
        loadTimeSlot(outletId, serviceId, therapistId, date){
            //
            var payload = {
                outletId: outletId,
                serviceId: serviceId,
                therapistId: therapistId,
                date: date
            }
            // this.$store.dispatch('booking/getTimeslotBookingAvailable', outlet_servie_therapist_date)
            HTTP_API({
            method: 'get',
            url: `booking/booking-available/timeslot/${payload.outletId}/${payload.serviceId}/${payload.therapistId}/${payload.date}`,
            header: {
                // 
            }
            }).then((result) => {
            console.log('get timeslots of booking available')
            console.log(result)
            if (result.data.hasOwnProperty('error')){
                alert(result.data.error.errorMessage)
                alert(result.data.error.validation.errorMessage)
            }
            var timeslots = result.data.result.data
            this.timeslots = timeslots
            }).catch((err) => {
            console.log(err)
            })
        },
        formatDate (date) {
            if (!date) return null
                const [year, month, day] = date.split('-')
                return `${month}/${day}/${year}`
        },
        
    }
}
</script>
