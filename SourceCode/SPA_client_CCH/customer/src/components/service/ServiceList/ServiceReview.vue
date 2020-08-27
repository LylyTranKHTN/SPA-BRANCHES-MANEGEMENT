<template>
    <v-container px-0>
        <v-layout row wrap justify-center>
            <v-flex xs12 sm8 md8>
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
                            </v-layout>
                        </v-container>
                    </v-img>
                    <v-card-title primary-title>
                        <v-layout row wrap>
                            <v-flex xs2>
                                <v-avatar size="60" color="red">
                                    <v-img
                                        :src="therapist.avatar"
                                    ></v-img>
                                </v-avatar>
                            </v-flex>
                            <v-flex xs5>
                                <v-text-field
                                    label="Choosed Therapist"
                                    v-model="therapist.name"
                                    readonly
                                    
                                >
                                </v-text-field>
                            </v-flex>
                            <v-flex xs5>
                                <v-text-field
                                    
                                    label="Choosed Timeslot"
                                    
                                    v-model="timeslot"
                                    readonly
                                >
                                
                                </v-text-field>
                    </v-flex>
                </v-layout>
            </v-card-title>
        </v-card>
            </v-flex>
        </v-layout>
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
        currentOutlet: {}
    },
    data() {
        return{
           therapist: {},
           outlet:{},
           timeslot:this.service.timeSlot.from + ' -- ' + this.service.timeSlot.to
        }
    },
    created(){
        this.loadTherapistTimeSlotByOutlet()
    },
    computed: {
        ...mapState({
            // timeslots: state => state.booking.timeslots,
            // therapists: state => state.booking.therapists,
        }),
    },
    methods:{
        loadTherapistTimeSlotByOutlet(){
            HTTP_API({
                method: 'get',
                url: `therapist/${this.service.therapistID}`,
                header: {
                    //
                }
            }).then((result) => {
                console.log('get therapist by id')
                console.log(result)
                console.log(this.service.timeSlot)
                var therapist = result.data.result.data
                
                this.therapist = therapist
                })
            .catch((err) => {
                console.log(err)
            })
        }

        //
        
    }
}
</script>
