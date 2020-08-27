<template>
    <v-container grid-list-lg fluid>
        <!-- Choose Oulet -->
        <v-layout justify-center>
            <v-flex xs10 sm8 md8>
                <v-select
                    size="5"
                    label="Choose Outlet"
                    outline
                    :items="outlets"
                    v-model="currentOutlet"
                    item-text="name"
                    item-value="id"
                    hide-details
                    @change="searchTherapist()"
                    >
                </v-select>
            </v-flex>
        </v-layout>

        <v-layout justify-center pa->
			<v-flex xs10 sm8 dm8>
				<v-text-field
                    hide-details
					v-model="therapistName"
					label="Search therapist by name"
					outline
					append-icon="search"
                    @keyup.enter="searchTherapist()"
					@click:append="searchTherapist()"
				></v-text-field>
			</v-flex>
		</v-layout>


        <!-- Show List View -->
       <v-layout row wrap justify-center>
           <v-flex xs10 sm8 md8 >
               <v-card>
                    <v-layout row wrap align-center>
                        <v-flex class="text-xs-center"  xs6 sm6 v-for = "therapist in therapists" :key="therapist.index" >
                            <v-avatar
                            :size="80"
                            color="red"
                            @mousedown="viewTherapistDetails(therapist.id)"
                            >
                                <v-img
                                    :src="therapist.avatar"
                                    :alt="therapist.name"
                                >
                                </v-img>
                            </v-avatar>
                            <br>
                            <span>{{therapist.name}}</span>
                        </v-flex>

                    </v-layout>
               </v-card>
           </v-flex>
       </v-layout>
    </v-container>
</template>

<script>
import { mapState, mapActions } from 'vuex'
import axios from 'axios'

export default {
    // for data from other layout
    props: {

    },
    // for data in this layout
    data(){
        return {
            // Choosed Value
            outlet: null,
            outletName: null,
            therapistName: null,
            currentOutlet: 1,
        }
    },
    computed: {
        ...mapState({
            outlets: state => state.outlet.outlets,
            therapists: state => state.therapist.therapists,
        }),
    },
    created(){
        this.InitOutlet()
        this.loadOutlet()
    },
    mounted(){
        this.searchTherapist()
    },
    // for function for event
    methods: {
        viewTherapistDetails(therapistID){
            this.$store.dispatch('therapist/getTherapistsByID', therapistID)
            this.$router.push('/therapist')
        },
        loadOutlet(){
            this.$store.dispatch('outlet/getAllOutlet')
        },
        // chooseOutlet(outletID){
        //     var o_therapist = {
		// 		outletID: outletID,
		// 		pageSize: 6,
		// 		pageNumber: 1
		// 	}
        //     this.$store.dispatch('therapist/getTherapistsByOutlet', o_therapist)
        // },
        searchTherapist(){
            if (this.therapistName == null){
                var payload = {
                outletID: this.currentOutlet,
                pageSize: 6,
                pageNumber: 1
                }
                this.$store.dispatch('therapist/getTherapistsByOutlet', payload)
            }
            else{
                var o = {
                    outletID: this.currentOutlet,
                    therapistName: this.therapistName
                }
                this.$store.dispatch('therapist/getTherapistsByNameandOutlet', o)
            }
        },
        InitOutlet(){
            this.currentOutlet = this.$store.state.outlet.outlet
        }
    }
}
</script>
