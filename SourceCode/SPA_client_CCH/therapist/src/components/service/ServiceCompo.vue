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
                            <span class="headline">{{service.name}}</span>
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
                                src="data:image/jpeg;base64,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"
                            >
                        </v-avatar>
                    </v-flex>
                    <v-flex xs5>
                        <!-- Select Therapist -->
                        <v-select
                            :items="therapists"
                            item-text="therapistName"
                            
                            hide-details
                            label="Choose Therapist"
                            @mousedown="loadTherapist(serviceOutletID)"
                        >
                        </v-select>
                    </v-flex>
                    <v-flex xs5>
                        <v-select
                            :items="therapists"
                            
 
                            hide-details
                            label="Choose TimeSlot"
                            
                        >
                        </v-select>
                    </v-flex>
                </v-layout>
            </v-card-title>

        </v-card>
    </v-container>
</template>
<script>
import { mapState, mapActions } from 'vuex'

export default {
    props:{
        service: {},
        outlet:{},
    },
    data() {
        return{
            choose: true,
            serviceOutletID:{
                serviceID: this.service.id,
                outletID: this.outlet.id,
            }
            
            // payload1: outletID + serviceID
        
        }
    },
    created(){
        // this.loadTherapist();
        // this.loadTimeSlot();
    },
    computed: {
        ...mapState({
            therapists:state=>state.therapist.therapists,
        })
    },
    methods: {
        chooseService(){
            this.choose = ! this.choose
            this.$store.dispatch('service/chooseService', this.service)
        },
        removeService(){
            this.choose = ! this.choose
            this.$store.dispatch('service/removeService', this.service)
        },
        loadTherapist(serviceOutletID){
            console.log('loadTherapist (UI)')
            console.log(this.outlet)
            console.log(this.service)
            this.$store.dispatch('therapist/getTherapistsByServiceOutlet',serviceOutletID)
            console.log('loadTherapist (UI) success')
        },
        loadTimeSlot(){},
        // loadTherapist(){

        // },
        // loadTimeSlot(){

        // },

    }
}
</script>
