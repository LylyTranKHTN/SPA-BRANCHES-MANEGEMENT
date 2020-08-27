<template>
    <v-container>
        <h2 class="text-xs-center">My bookings</h2>

        <v-layout row wrap>
             <v-flex >
                <review-booking
                    class="right" :outlet="outlet.name" :date="date" >
                </review-booking>
            </v-flex>
        </v-layout>

        <v-layout pb-2>
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
                    @change="chooseOutlet(currentOutlet.id)"
            >
            </v-select>
                <v-btn fab dark color="green" small> <v-icon dark>add</v-icon>
                </v-btn>
        </v-layout>

         <v-layout>
            <v-flex pb-2>
            <v-text-field
                hide-details
				v-model="CustomerSearch"
                label="Customer search"
                outline
                append-icon="search"
                @keyup.enter="searchBooking()"
				@click:append="searchBooking()"
            ></v-text-field>
            </v-flex>
         </v-layout>
        
          <v-layout>
            <v-flex>
                <v-select
                    v-model="DateRange"
                    outline
                    label="Date Range"
                    :items="days"
                    
                >
                </v-select>
            </v-flex>
            <v-divider vertical></v-divider>
            <v-flex>
                 <v-select
                    v-model="Status"
                    outline
                    label="Status"
                    :items="statusList"
                >
                </v-select>
            </v-flex>
        </v-layout>
        
        <v-layout>  
            <v-flex xs2 >Status</v-flex>
             <v-card>
                <div>
                    <span >ID Booking - Service Namejdfjhsdfjabvcahgdhbxn</span><br>
                    <span>Data Performing | Timeslot</span><br>
                    <span>@Tai Seng Outlet</span>
                </div>
            </v-card>

		</v-layout>


    </v-container>
</template>
<script>
import { mapState, mapActions } from 'vuex'

export default {
    data () {
        return{
            CustomerSearch: '',
            //outlets: ['oulet1', 'outlet2'],
            days: ['Yesterday','Last 30 days', 'Last week'],
            statusList: ['Booked', 'Performing', 'Completed', 'No show', 'All Status'],
            outlet: ''
        }
    },
     created(){
        this.loadOutlet()
    },
    mounted(){
    //     this.chooseOutlet(this.currentOutlet.id)
    },
    computed: {
        ...mapState({
            outlets: state => state.outlet.outlets,
            //services: state => state.service.services,
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
        searchBooking(){
			console.log('search booking')
			var name = this.outletName
			//this.$store.dispatch('booking/getBookingByOutletandCustomername', payload)
		},
    },  
    
    }
</script>
