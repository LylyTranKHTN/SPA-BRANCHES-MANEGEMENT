<template>
    <v-container fluid grid-list-lg>
        <!-- Customer Name -->

        <v-layout row wrap justify-center>
            <v-flex xs10 sm8 md8>
                <v-card>
                    <v-card-title>
                        <div>
                            <h4 class="pa-1">Customer Name: {{BookingDetails.CustomerName}}</h4>
                            <h4 class="pa-1">Outlet Name: {{BookingDetails.outletName}}</h4>
                            <h4 class="pa-1">{{BookingDetails.dateBooking}} | {{BookingDetails.timeBooking}} | {{BookingDetails.statusBooking}}</h4>
                            <h4 class="pa-1">{{BookingDetails.cost}}</h4>
                            <v-layout pl-1>
                                <!-- Paid text -->
                                <v-flex>
                                    <h4>Paid</h4>
                                </v-flex>


                            </v-layout>
                        </div>

                    </v-card-title>
                </v-card>
            </v-flex>
        </v-layout>

        <!-- Button Cancel + Button Edit + Button View Result -->
        <v-layout justify-center wrap row>
            <!-- Button Cancel -->
            <v-btn
                flat outline
                color = "primary">
                Cancel
            </v-btn>

            <!-- Button Edit -->
            <v-btn
                outline flat
                color = "primary">
                Edit
            </v-btn>

            <!-- Button View more -->
            <v-btn
                outline flat
                color = "primary">
                View more
            </v-btn>
        </v-layout>

        <!-- Timeline -->
        <v-layout wrap justify-center>
            <v-flex xs10 sm8 md8>
                <v-card>
                    <v-flex>
                        <v-timeline dense clipped >
                        <v-layout v-for = "detail in BookingDetails.infoService" :key="detail.index" column>
                            <v-timeline-item
                                class="mb-3"
                                color="white"
                                icon-color="grey lighten-2"
                                small
                                light
                            >
                                <v-flex>
                                <span>{{detail.nameService}}</span>
                                </v-flex>
                                <v-flex>
                                <span>By: {{detail.nameTherapist}}</span>
                                </v-flex>
                            </v-timeline-item>
                            <v-divider inset></v-divider>
                        </v-layout>
                        </v-timeline>
                    </v-flex>
                </v-card>
            </v-flex>
        </v-layout>

        <!-- Extra note -->
        <v-layout wrap justify-center row>
            <v-flex xs10 sm8 md8 >
                <v-card>
                    <span>{{BookingDetails.ExtraNote}}</span>
                </v-card>
            </v-flex>
        </v-layout>
    </v-container>
</template>

<script>
import { mapState, mapActions } from 'vuex'
export default {
    data(){
        return{
        //     BookingDetails: {
        //         CustomerName: "Bao",
        //         Outlet: "Con co huong",
        //         Date: "1/1/2019",
        //         Time: "11.45-14.30",
        //         Book: "Booked",
        //         Total: "$45",
        //         Paid: "True",
        //         ExtraNote: "Extra Note"
        //     },

        //     Services: [
        //         {
        //             Detail: "Chuan bi",
        //             By: "Bao"
        //         },
        //         {
        //             Detail: "Massage",
        //             By: "My"
        //         }
        //     ]
        }
    },
    computed: {
        ...mapState({
            BookingDetails: state => state.booking.booking
        })
    },
}
</script>
