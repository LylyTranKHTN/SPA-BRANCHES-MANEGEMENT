<template>
    <v-container fluid grid-list-lg>
        <!-- Choose Time & Status Select  -->
        <v-layout wrap row justify-center>
            <v-flex xs5 sm4 md4>
                <v-select
                    v-model="time"
                    :items="days"
                    item-text="text"
                    item-value="value"
                    label="Time"
                    outline
                    hide-details
                    @change="chooseTime()"
                ></v-select>
            </v-flex>

            <v-flex xs5 sm4 md4 >
                <v-select
                :items="status"
                label="Status"
                outline
                hide-details
                @change="choosetus()"
                disabled=""
                ></v-select>
            </v-flex>
        </v-layout>

        <!-- Booking History list -->

        <v-layout
            v-for="booking in bookings" :key="booking.index"
            row wrap
            justify-center
            pb-2
            @mousedown="viewBookingDetails(booking.appointmentId)"
        >
            <v-flex xs10 sm8 md8
                wrap>
                <v-card>
                   <v-layout row wrap>
                        <v-flex xs3 md3 pa-0
                            class="text-xs-center white--text"
                        >
                            <v-avatar
                                size="70"
                                color="red"
                                tile
                            >
                                <span>{{booking.status}}</span>
                            </v-avatar>
                        </v-flex>

                        <v-flex xs9 md9>
                            <v-layout column>
                                <v-flex pa-0> #{{booking.appointmentId}} </v-flex>
                                <v-flex align-content-center>
                                    <v-layout row align-center>
                                        <v-flex pa-0>{{booking.dateServe}} || {{booking.timeServe}}</v-flex>
                                    </v-layout>
                                </v-flex>
                                <!-- <v-flex pa-0> @ {{booking.nameOutlet}} </v-flex>
                                <v-flex pa-0> {{booking.nameOutlet}} </v-flex>
                                <v-flex pa-0> {{booking.dateServe}} - {{booking.timeServe}} </v-flex>
                                <v-flex pa-0> {{booking.listnameService[0]}} </v-flex> -->
                            </v-layout>
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
    data(){
        return {
            days:[
                {
                    text:"Last 7 days",
                    value: 7
                },
                {
                    text:"Last 15 days",
                    value: 15
                },
                {
                    text:"Last 30 days",
                    value: 30
                },
            ],
            status:["All status","Booked","Performed","Complete"],
            time: 7 ,
            outlet:null,
        }
    },

    created(){
        this.chooseTime()
    },
        
    computed: {
        ...mapState({
            bookings: state => state.booking.bookingHistory,
            user: state => state.customer.userInfo
        })
    },

    methods: {
        viewBookingDetails(bookingID) {
            this.$store.dispatch('booking/getBookingDetail', bookingID)
            this.$router.push('/booking_detail')
        },
        chooseTime(){
            var payload= {
                    cusID: this.user.id,
                    days: this.time
                }
            this.$store.dispatch('booking/getBookingHistoryByCustomer', payload)
           
        },
        choosetus(){
        },
        loadBookings(){
            console.log('this is booking list')
            console.log(this.bookings)
        }
    }
}
</script>
